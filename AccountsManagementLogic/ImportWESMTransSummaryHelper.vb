Imports AccountsManagementObjects
Imports AccountsManagementDataAccess
Imports System.IO
Imports System.Threading.Tasks
Imports CsvHelper
Imports System.Globalization
Imports System.Threading

Public Class ImportWESMTransSummaryHelper
    Public Sub New()
        'Get the current instance of the dal
        Me._WBillHelper = WESMBillHelper.GetInstance
        Me._DataAccess = DAL.GetInstance()
        Me._NewListWESMBill = New List(Of WESMBill)
        Me._NewListWBAllocCoverSummary = New List(Of WESMBillAllocCoverSummary)
    End Sub

#Region "DAL"
    Private _DataAccess As DAL
    Public ReadOnly Property DataAccess() As DAL
        Get
            Return Me._DataAccess
        End Get
    End Property
#End Region

#Region "WESMBillHelper"
    Private _WBillHelper As WESMBillHelper
    Public ReadOnly Property WBillHelper() As WESMBillHelper
        Get
            Return _WBillHelper
        End Get
    End Property
#End Region

    Private _NewListWESMBill As List(Of WESMBill)
    Public Property NewListWESMBill() As List(Of WESMBill)
        Get
            Return _NewListWESMBill
        End Get
        Set(ByVal value As List(Of WESMBill))
            _NewListWESMBill = value
        End Set
    End Property

    Private _NewListWBAllocCoverSummary As List(Of WESMBillAllocCoverSummary)
    Public Property NewListWBAllocCoverSummary() As List(Of WESMBillAllocCoverSummary)
        Get
            Return _NewListWBAllocCoverSummary
        End Get
        Set(ByVal value As List(Of WESMBillAllocCoverSummary))
            _NewListWBAllocCoverSummary = value
        End Set
    End Property


#Region "Read TextFile"
    Private Function CsvToDataTable(ByVal strFileName As String) As DataTable
        Using reader As StreamReader = New StreamReader(strFileName)
            Using csv As CsvReader = New CsvReader(reader, CultureInfo.InvariantCulture)
                Using dr = New CsvDataReader(csv)
                    Dim dt As DataTable = New DataTable()
                    dt.Load(dr)
                    Return dt
                End Using
            End Using
        End Using
    End Function

    Public Sub ReadFlatFileNew(ByVal bpNo As Integer, ByVal stlRun As String, ByVal wesmBilltransType As String, ByVal fileType As EnumFileType, ByVal pathFile As String,
                            ByVal fileName As String, ByVal remarks As String, ByVal transDate As Date, ByVal dueDate As Date, ByVal progress As IProgress(Of ProgressClass))
        Dim dtDataTable As New DataTable
        Dim listWBFileOutputItem As New List(Of WESMBillFileOutput)
        Try
            pathFile = pathFile & "\" & fileName
            dtDataTable = CsvToDataTable(pathFile)
            Dim columnHeaders As String() = {"STL_ID", "BILLING_ID", "FACILITY_TYPE", "WHT_TAG", "ITH_TAG", "NON_VATABLE_TAG",
                                              "ZERO_RATED_TAG", "NET_SELLERBUYER_TAG", "VATABLE_SALES", "ZERO_RATED_SALES", "ZERO_RATED_ECOZONES_SALES",
                                              "VAT_ON_SALES", "VATABLE_PURCHASES ", "ZERO_RATED_PURCHASES", "ZERO_RATED_ECOZONES_PURCHASES", "VAT_ON_PURCHASES",
                                              "EWT", "NSS", "GMR", "MF_RATE", "SELLER/BUYER", "SPOT_QTY", "GENX_AMT", "INVOICE_ID"}

            Dim colIndx As Integer = 0
            For Each column As DataColumn In dtDataTable.Columns
                If Not column.ColumnName.Trim.ToUpper.Equals(CStr(columnHeaders(colIndx).Trim.ToUpper)) Then
                    Throw New Exception("Invalid row header for " & column.ColumnName)
                End If
                colIndx += 1
            Next

            Dim rowIndx As Integer = 0
            Dim rowCount = dtDataTable.Rows.Count()
            For Each row As DataRow In dtDataTable.Rows
                Dim wbFileOutputItem As New WESMBillFileOutput

                If IsDBNull(row.Item("STL_ID")) Then
                    Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid STL_ID found in File")
                End If
                wbFileOutputItem.StlID = CStr(row.Item("STL_ID"))

                If IsDBNull(row.Item("BILLING_ID")) Then
                    Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid BILLING_ID found in File")
                End If
                wbFileOutputItem.BillingId = CStr(row.Item("BILLING_ID"))

                If IsDBNull(row.Item("FACILITY_TYPE")) Then
                    Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid FACILITY_TYPE found in File")
                ElseIf Not CStr(row.Item("FACILITY_TYPE").ToUpper.ToString).Equals("GENERATOR") And Not CStr(row.Item("FACILITY_TYPE").ToUpper.ToString).Equals("LOAD") Then
                    Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid FACILITY_TYPE found in File")
                End If
                wbFileOutputItem.FacilityType = CStr(row.Item("FACILITY_TYPE"))

                If IsDBNull(row.Item("WHT_TAG")) Then
                    Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid WHT_TAG found in File. Shall be Y/N only.")
                ElseIf Not CStr(row.Item("WHT_TAG").ToUpper.ToString).Equals("Y") And Not CStr(row.Item("WHT_TAG").ToUpper.ToString).Equals("N") Then
                    Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid WHT_TAG found in File. Shall be Y/N only.")
                End If
                wbFileOutputItem.WHTTag = CStr(row.Item("WHT_TAG"))

                If IsDBNull(row.Item("ITH_TAG")) Then
                    Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid ITH_TAG found in File. Shall be Y/N only.")
                ElseIf Not CStr(row.Item("ITH_TAG").ToUpper.ToString).Equals("Y") And Not CStr(row.Item("ITH_TAG").ToUpper.ToString).Equals("N") Then
                    Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid ITH_TAG found in File. Shall be Y/N only.")
                End If
                wbFileOutputItem.ITHTag = CStr(row.Item("ITH_TAG"))

                If IsDBNull(row.Item("NON_VATABLE_TAG")) Then
                    Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid NON_VATABLE_TAG found in File. Shall be Y/N only.")
                ElseIf Not CStr(row.Item("NON_VATABLE_TAG").ToUpper.ToString).Equals("Y") And Not CStr(row.Item("NON_VATABLE_TAG").ToUpper.ToString).Equals("N") Then
                    Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid NON_VATABLE_TAG found in File. Shall be Y/N only.")
                End If
                wbFileOutputItem.NonVatableTag = CStr(row.Item("NON_VATABLE_TAG"))

                If IsDBNull(row.Item("ZERO_RATED_TAG")) Then
                    Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid ZERO_RATED_TAG found in File. Shall be Y/N only.")
                ElseIf Not CStr(row.Item("ZERO_RATED_TAG").ToUpper.ToString).Equals("Y") And Not CStr(row.Item("ZERO_RATED_TAG").ToUpper.ToString).Equals("N") Then
                    Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid ZERO_RATED_TAG found in File. Shall be Y/N only.")
                End If
                wbFileOutputItem.ZeroRatedTag = CStr(row.Item("ZERO_RATED_TAG"))

                If IsDBNull(row.Item("NET_SELLERBUYER_TAG")) Then
                    Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid NET_SELLERBUYER_TAG found in File. Shall be SELLER/BUYER only.")
                ElseIf Not CStr(row.Item("NET_SELLERBUYER_TAG").ToUpper.ToString).Equals("SELLER") And Not CStr(row.Item("NET_SELLERBUYER_TAG").ToUpper.ToString).Equals("BUYER") Then
                    Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid NET_SELLERBUYER_TAG found in File. Shall be SELLER/BUYER only.")
                End If
                wbFileOutputItem.NetSellerBuyerTag = CStr(row.Item("NET_SELLERBUYER_TAG"))

                If Not IsDBNull(row.Item("VATABLE_SALES")) And Len(row.Item("VATABLE_SALES")) <> 0 Then
                    If Not IsNumeric(row.Item("VATABLE_SALES")) Then
                        Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid VATABLE_SALES found in File")
                    ElseIf CDec(row.Item("VATABLE_SALES")) < 0 Then
                        Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid VATABLE_SALES found with negative amount in File")
                    End If
                    wbFileOutputItem.VatableSales = CDec(row.Item("VATABLE_SALES"))
                Else
                    wbFileOutputItem.VatableSales = 0
                End If

                If Not IsDBNull(row.Item("ZERO_RATED_SALES")) And Len(row.Item("ZERO_RATED_SALES")) <> 0 Then
                    If Not IsNumeric(row.Item("ZERO_RATED_SALES")) Then
                        Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid ZERO_RATED_SALES found in File")
                    ElseIf CDec(row.Item("ZERO_RATED_SALES")) < 0 Then
                        Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid ZERO_RATED_SALES found with negative amount in File")
                    End If
                    wbFileOutputItem.ZeroRatedSales = CDec(row.Item("ZERO_RATED_SALES"))
                Else
                    wbFileOutputItem.ZeroRatedSales = 0
                End If

                If Not IsDBNull(row.Item("ZERO_RATED_ECOZONES_SALES")) And Len(row.Item("ZERO_RATED_ECOZONES_SALES")) <> 0 Then
                    If Not IsNumeric(row.Item("ZERO_RATED_ECOZONES_SALES")) Then
                        Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid ZERO_RATED_ECOZONES_SALES found in File")
                    ElseIf CDec(row.Item("ZERO_RATED_ECOZONES_SALES")) < 0 Then
                        Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid ZERO_RATED_ECOZONES_SALES found with negative amount in File")
                    End If
                    wbFileOutputItem.ZeroRatedEcoZoneSales = CDec(row.Item("ZERO_RATED_ECOZONES_SALES"))
                Else
                    wbFileOutputItem.ZeroRatedEcoZoneSales = 0
                End If

                If Not IsDBNull(row.Item("VAT_ON_SALES")) And Len(row.Item("VAT_ON_SALES")) <> 0 Then
                    If Not IsNumeric(row.Item("VAT_ON_SALES")) Then
                        Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid VAT_ON_SALES found in File")
                    ElseIf CDec(row.Item("VAT_ON_SALES")) < 0 Then
                        Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid VAT_ON_SALES found with negative amount in File")
                    End If
                    wbFileOutputItem.VatOnSales = CDec(row.Item("VAT_ON_SALES"))
                Else
                    wbFileOutputItem.VatOnSales = 0
                End If

                If Not IsDBNull(row.Item("VATABLE_PURCHASES")) And Len(row.Item("VATABLE_PURCHASES")) <> 0 Then
                    If Not IsNumeric(row.Item("VATABLE_PURCHASES")) Then
                        Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid VATABLE_PURCHASES found in File")
                    ElseIf CDec(row.Item("VATABLE_PURCHASES")) > 0 Then
                        Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid VATABLE_PURCHASES found with positive amount in File")
                    End If
                    wbFileOutputItem.VatablePurchases = CDec(row.Item("VATABLE_PURCHASES"))
                Else
                    wbFileOutputItem.VatablePurchases = 0
                End If

                If Not IsDBNull(row.Item("ZERO_RATED_PURCHASES")) And Len(row.Item("ZERO_RATED_PURCHASES")) <> 0 Then
                    If Not IsNumeric(row.Item("ZERO_RATED_PURCHASES")) Then
                        Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid ZERO_RATED_PURCHASES found in File")
                    ElseIf CDec(row.Item("ZERO_RATED_PURCHASES")) > 0 Then
                        Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid ZERO_RATED_PURCHASES found with positive amount in File")
                    End If
                    wbFileOutputItem.ZeroRatedPurchases = CDec(row.Item("ZERO_RATED_PURCHASES"))
                Else
                    wbFileOutputItem.ZeroRatedPurchases = 0
                End If

                If Not IsDBNull(row.Item("ZERO_RATED_ECOZONES_PURCHASES")) And Len(row.Item("ZERO_RATED_ECOZONES_PURCHASES")) <> 0 Then
                    If Not IsNumeric(row.Item("ZERO_RATED_ECOZONES_PURCHASES")) Then
                        Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid ZERO_RATED_ECOZONES_PURCHASES found in File")
                    ElseIf CDec(row.Item("ZERO_RATED_PURCHASES")) > 0 Then
                        Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid ZERO_RATED_ECOZONES_PURCHASES found with positive amount in File")
                    End If
                    wbFileOutputItem.ZeroRatedEcoZonePurchases = CDec(row.Item("ZERO_RATED_ECOZONES_PURCHASES"))
                Else
                    wbFileOutputItem.ZeroRatedEcoZonePurchases = 0
                End If

                If Not IsDBNull(row.Item("VAT_ON_PURCHASES")) And Len(row.Item("VAT_ON_PURCHASES")) <> 0 Then
                    If Not IsNumeric(row.Item("VAT_ON_PURCHASES")) Then
                        Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid VAT_ON_PURCHASES found in File")
                    ElseIf CDec(row.Item("VAT_ON_PURCHASES")) > 0 Then
                        Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid VAT_ON_PURCHASES found with positive amount in File")
                    End If
                    wbFileOutputItem.VatOnPurchases = CDec(row.Item("VAT_ON_PURCHASES"))
                Else
                    wbFileOutputItem.VatOnPurchases = 0
                End If

                If Not IsDBNull(row.Item("EWT")) And Len(row.Item("EWT")) <> 0 Then
                    If Not IsNumeric(row.Item("EWT")) Then
                        Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid EWT found in File")
                    End If
                    wbFileOutputItem.EWT = CDec(row.Item("EWT"))
                Else
                    wbFileOutputItem.EWT = 0
                End If

                If Not IsDBNull(row.Item("NSS")) And Len(row.Item("NSS")) <> 0 Then
                    If Not IsNumeric(row.Item("NSS")) Then
                        Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid NSS found in File")
                    End If
                    wbFileOutputItem.NSS = CDec(row.Item("NSS"))
                Else
                    wbFileOutputItem.NSS = 0
                End If

                If Not IsDBNull(row.Item("GMR")) And Len(row.Item("GMR")) <> 0 Then
                    If Not IsNumeric(row.Item("GMR")) Then
                        Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid GMR found in File")
                    End If
                    wbFileOutputItem.GMR = CDec(row.Item("GMR"))
                Else
                    wbFileOutputItem.GMR = 0
                End If

                If Not IsDBNull(row.Item("MF_RATE")) And Len(row.Item("MF_RATE")) <> 0 Then
                    If Not IsNumeric(row.Item("MF_RATE")) Then
                        Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid MF_RATE found in File")
                    ElseIf CDec(row.Item("MF_RATE")) < 0 Then
                        Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid MF_RATE found with negative amount in File")
                    End If
                    wbFileOutputItem.MarketFeesRate = CDec(row.Item("MF_RATE"))
                Else
                    wbFileOutputItem.MarketFeesRate = 0
                End If

                If IsDBNull(row.Item("SELLER/BUYER")) Then
                    Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid SELLER/BUYER found in File")
                End If
                wbFileOutputItem.SellerOrBuyer = CStr(row.Item("SELLER/BUYER"))

                If Not IsDBNull(row.Item("SPOT_QTY")) And Len(row.Item("SPOT_QTY")) <> 0 Then
                    If Not IsNumeric(row.Item("SPOT_QTY")) Then
                        Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid SPOT_QTY found in File")
                    End If
                    wbFileOutputItem.SpotQty = CDec(row.Item("SPOT_QTY"))
                Else
                    wbFileOutputItem.SpotQty = 0
                End If

                If Not IsDBNull(row.Item("GENX_AMT")) And Len(row.Item("GENX_AMT")) <> 0 Then
                    If Not IsNumeric(row.Item("GENX_AMT")) Then
                        Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid GENX_AMT found in File")
                    End If
                    wbFileOutputItem.GenXAmount = CDec(row.Item("GENX_AMT"))
                Else
                    wbFileOutputItem.GenXAmount = 0
                End If

                If IsDBNull(row.Item("INVOICE_ID")) Then
                    Throw New ApplicationException("Error in row " & rowIndx & "." & "Invalid INVOICE_ID found in File")
                End If
                wbFileOutputItem.InvoiceID = CStr(row.Item("INVOICE_ID"))

                'Add the WESM Bil output
                listWBFileOutputItem.Add(wbFileOutputItem)
                listWBFileOutputItem.TrimExcess()

                'Increment the Row Index
                rowIndx += 1
                Dim newProgress As ProgressClass = New ProgressClass
                newProgress.ProgressIndicator = (rowIndx * 100 / rowCount)
                newProgress.ProgressMsg = "Reading data " & rowIndx & " out of " & rowCount
                progress.Report(newProgress)
            Next

            Me.GenerateWESMBills(bpNo, stlRun, wesmBilltransType, fileType, transDate, dueDate, remarks, listWBFileOutputItem)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub

    Public Async Sub ReadFlatFile(ByVal bpNo As Integer, ByVal stlRun As String, ByVal wesmBilltransType As String, ByVal fileType As EnumFileType, ByVal pathFile As String,
                            ByVal fileName As String, ByVal remarks As String, ByVal transDate As Date, ByVal dueDate As Date, ByVal progress As IProgress(Of ProgressClass))
        Dim ds As New DataSet
        Dim listWBFileOutputItem As New List(Of WESMBillFileOutput)
        pathFile = pathFile & "\"

        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(pathFile & fileName)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")

            Dim currentRow As String()
            Dim indexRow As Integer, indexColumn As Integer

            Dim lineCount = File.ReadAllLines(pathFile & fileName).Length

            While Not MyReader.EndOfData
                Dim wbFileOutputItem As New WESMBillFileOutput

                indexColumn = 0
                'Read the current row
                currentRow = MyReader.ReadFields()
                Try
                    Dim currentField As String
                    Dim chargeIdLib As String = ""
                    For Each currentField In currentRow
                        Select Case indexColumn
                            Case 0
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow.ToString() & "." & "Invalid STL_ID found in File")
                                    End If
                                    wbFileOutputItem.StlID = CStr(currentField)
                                Else
                                    If currentField.Trim().ToUpper() <> "STL_ID" Then
                                        Throw New ApplicationException("Invalid row header for STL_ID")
                                    End If
                                End If
                            Case 1
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow.ToString() & "." & "Invalid BILLING_ID found in File")
                                    End If
                                    wbFileOutputItem.BillingId = CStr(currentField)
                                Else
                                    If currentField.Trim().ToUpper() <> "BILLING_ID" Then
                                        Throw New ApplicationException("Invalid row header for BILLING_ID")
                                    End If
                                End If
                            Case 2
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow.ToString() & "." & "Invalid FACILITY_TYPE found in File")
                                    ElseIf Not CStr(currentField.ToUpper.ToString).Equals("GENERATOR") And Not CStr(currentField.ToUpper.ToString).Equals("LOAD") Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid FACILITY_TYPE found in File")
                                    End If
                                    wbFileOutputItem.FacilityType = CStr(currentField)
                                Else
                                    If currentField.Trim().ToUpper() <> "FACILITY_TYPE" Then
                                        Throw New ApplicationException("Invalid row header for INVOICE_DATE")
                                    End If
                                End If
                            Case 3
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid WHT_TAG found in File")
                                    ElseIf Not CStr(currentField.ToUpper.ToString).Equals("Y") And Not CStr(currentField.ToUpper.ToString).Equals("N") Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid WHT_TAG found in File")
                                    End If
                                    wbFileOutputItem.WHTTag = CStr(currentField)
                                Else
                                    If currentField.Trim().ToUpper() <> "WHT_TAG" Then
                                        Throw New ApplicationException("Invalid row header WHT_TAG")
                                    End If
                                End If
                            Case 4
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid ITH_TAG found in File")
                                    ElseIf Not CStr(currentField.ToUpper.ToString).Equals("Y") And Not CStr(currentField.ToUpper.ToString).Equals("N") Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid ITH_TAG found in File")
                                    End If
                                    wbFileOutputItem.ITHTag = CStr(currentField)
                                Else
                                    If currentField.Trim().ToUpper() <> "ITH_TAG" Then
                                        Throw New ApplicationException("Invalid row header ITH_TAG")
                                    End If
                                End If

                            Case 5
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid NON_VATABLE_TAG found in File")
                                    ElseIf Not CStr(currentField.ToUpper.ToString).Equals("Y") And Not CStr(currentField.ToUpper.ToString).Equals("N") Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid NON_VATABLE_TAG found in File")
                                    End If
                                    wbFileOutputItem.NonVatableTag = CStr(currentField)
                                Else
                                    If currentField.Trim().ToUpper() <> "NON_VATABLE_TAG" Then
                                        Throw New ApplicationException("Invalid row header NON_VATABLE_TAG")
                                    End If
                                End If
                            Case 6
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid ZERO_RATED_TAG found in File")
                                    ElseIf Not CStr(currentField.ToUpper.ToString).Equals("Y") And Not CStr(currentField.ToUpper.ToString).Equals("N") Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid ZERO_RATED_TAG found in File")
                                    End If
                                    wbFileOutputItem.ZeroRatedTag = CStr(currentField)
                                Else
                                    If currentField.Trim().ToUpper() <> "ZERO_RATED_TAG" Then
                                        Throw New ApplicationException("Invalid row header ZERO_RATED_TAG")
                                    End If
                                End If
                            Case 7
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid NET_SELLERBUYER_TAG found in File")
                                    ElseIf Not CStr(currentField.ToUpper.ToString).Equals("SELLER") And Not CStr(currentField.ToUpper.ToString).Equals("BUYER") Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid NET_SELLERBUYER_TAG found in File")
                                    End If
                                    wbFileOutputItem.NetSellerBuyerTag = CStr(currentField)
                                Else
                                    If currentField.Trim().ToUpper() <> "NET_SELLERBUYER_TAG" Then
                                        Throw New ApplicationException("Invalid row header ZERO_RATED_TAG")
                                    End If
                                End If
                            Case 8
                                If indexRow <> 0 Then
                                    If Not IsDBNull(currentField) And Len(currentField) <> 0 Then
                                        If Not IsNumeric(currentField) Then
                                            Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid VATABLE_SALES found in File")
                                        ElseIf CDec(currentField) < 0 Then
                                            Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid VATABLE_SALES found with negative amount in File")
                                        End If
                                    End If
                                    wbFileOutputItem.VatableSales = If(IsDBNull(CDec(currentField)), 0, CDec(currentField))
                                Else
                                    If currentField.Trim().ToUpper() <> "VATABLE_SALES" Then
                                        Throw New ApplicationException("Invalid row header VATABLE_SALES")
                                    End If
                                End If
                            Case 9
                                If indexRow <> 0 Then
                                    If Not IsDBNull(currentField) And Len(currentField) <> 0 Then
                                        If Not IsNumeric(currentField) Then
                                            Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid ZERO_RATED_SALES found in File")
                                        ElseIf CDec(currentField) < 0 Then
                                            Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid ZERO_RATED_SALES found with negative amount in File")
                                        End If
                                    End If
                                    wbFileOutputItem.ZeroRatedSales = CDec(currentField)
                                Else
                                    If currentField.Trim().ToUpper() <> "ZERO_RATED_SALES" Then
                                        Throw New ApplicationException("Invalid row header ZERO_RATED_SALES")
                                    End If
                                End If

                            Case 10
                                If indexRow <> 0 Then
                                    If Not IsDBNull(currentField) And Len(currentField) <> 0 Then
                                        If Not IsNumeric(currentField) Then
                                            Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid ZERO_RATED_ECOZONES_SALES found in File")
                                        ElseIf CDec(currentField) < 0 Then
                                            Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid ZERO_RATED_ECOZONES_SALES found with negative amount in File")
                                        End If
                                    End If
                                    wbFileOutputItem.ZeroRatedEcoZoneSales = If(IsDBNull(CDec(currentField)), 0, CDec(currentField))
                                Else
                                    If currentField.Trim().ToUpper() <> "ZERO_RATED_ECOZONES_SALES" Then
                                        Throw New ApplicationException("Invalid row header ZERO_RATED_ECOZONES_SALES")
                                    End If
                                End If

                            Case 11
                                If indexRow <> 0 Then
                                    If Not IsDBNull(currentField) And Len(currentField) <> 0 Then
                                        If Not IsNumeric(currentField) Then
                                            Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid VAT_ON_SALES found in File")
                                        ElseIf CDec(currentField) < 0 Then
                                            Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid VAT_ON_SALES found with negative amount in File")
                                        End If
                                    End If
                                    wbFileOutputItem.VatOnSales = If(IsDBNull(CDec(currentField)), 0, CDec(currentField))
                                Else
                                    If currentField.Trim().ToUpper() <> "VAT_ON_SALES" Then
                                        Throw New ApplicationException("Invalid row header VAT_ON_SALES")
                                    End If
                                End If

                            Case 12
                                If indexRow <> 0 Then
                                    If Not IsDBNull(currentField) And Len(currentField) <> 0 Then
                                        If Not IsNumeric(currentField) Then
                                            Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid VATABLE_PURCHASES found in File")
                                        ElseIf CDec(currentField) > 0 Then
                                            Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid VATABLE_PURCHASES found with positive amount in File")
                                        End If
                                    End If
                                    wbFileOutputItem.VatablePurchases = If(IsDBNull(CDec(currentField)), 0, CDec(currentField))
                                Else
                                    If currentField.Trim().ToUpper() <> "VATABLE_PURCHASES" Then
                                        Throw New ApplicationException("Invalid row header VATABLE_PURCHASES")
                                    End If
                                End If

                            Case 13
                                If indexRow <> 0 Then
                                    If Not IsDBNull(currentField) And Len(currentField) <> 0 Then
                                        If Not IsNumeric(currentField) Then
                                            Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid ZERO_RATED_PURCHASES found in File")
                                        ElseIf CDec(currentField) > 0 Then
                                            Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid ZERO_RATED_PURCHASES found with positive amount in File")
                                        End If
                                    End If
                                    wbFileOutputItem.ZeroRatedPurchases = If(IsDBNull(CDec(currentField)), 0, CDec(currentField))
                                Else
                                    If currentField.Trim().ToUpper() <> "ZERO_RATED_PURCHASES" Then
                                        Throw New ApplicationException("Invalid row header ZERO_RATED_PURCHASES")
                                    End If
                                End If

                            Case 14
                                If indexRow <> 0 Then
                                    If Not IsDBNull(currentField) And Len(currentField) <> 0 Then
                                        If Not IsNumeric(currentField) Then
                                            Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid ZERO_RATED_ECOZONES_PURCHASES found in File")
                                        ElseIf CDec(currentField) > 0 Then
                                            Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid ZERO_RATED_ECOZONES_PURCHASES found with positive amount in File")
                                        End If
                                    End If
                                    wbFileOutputItem.ZeroRatedEcoZonePurchases = If(IsDBNull(CDec(currentField)), 0, CDec(currentField))
                                Else
                                    If currentField.Trim().ToUpper() <> "ZERO_RATED_ECOZONES_PURCHASES" Then
                                        Throw New ApplicationException("Invalid row header ZERO_RATED_ECOZONES_PURCHASES")
                                    End If
                                End If

                            Case 15
                                If indexRow <> 0 Then
                                    If Not IsDBNull(currentField) And Len(currentField) <> 0 Then
                                        If Not IsNumeric(currentField) Then
                                            Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid VAT_ON_PURCHASES found in File")
                                        ElseIf CDec(currentField) > 0 Then
                                            Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid VAT_ON_PURCHASES found with positive amount in File")
                                        End If
                                    End If
                                    wbFileOutputItem.VatOnPurchases = If(IsDBNull(CDec(currentField)), 0, CDec(currentField))
                                Else
                                    If currentField.Trim().ToUpper() <> "VAT_ON_PURCHASES" Then
                                        Throw New ApplicationException("Invalid row header VAT_ON_PURCHASES")
                                    End If
                                End If

                            Case 16
                                If indexRow <> 0 Then
                                    If Not IsDBNull(currentField) And Len(currentField) <> 0 Then
                                        If Not IsNumeric(currentField) Then
                                            Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid EWT found in File")
                                        End If
                                    End If
                                    wbFileOutputItem.EWT = If(IsDBNull(CDec(currentField)), 0, CDec(currentField))
                                Else
                                    If currentField.Trim().ToUpper() <> "EWT" Then
                                        Throw New ApplicationException("Invalid row header EWT")
                                    End If
                                End If

                            Case 17
                                If indexRow <> 0 Then
                                    If Not IsDBNull(currentField) And Len(currentField) <> 0 Then
                                        If Not IsNumeric(currentField) Then
                                            Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid NSS found in File")
                                        End If
                                    End If
                                    wbFileOutputItem.NSS = If(IsDBNull(CDec(currentField)), 0, CDec(currentField))
                                Else
                                    If currentField.Trim().ToUpper() <> "NSS" Then
                                        Throw New ApplicationException("Invalid row header NSS")
                                    End If
                                End If

                            Case 18
                                If indexRow <> 0 Then
                                    If Not IsDBNull(currentField) And Len(currentField) <> 0 Then
                                        If Not IsNumeric(currentField) Then
                                            Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid GMR found in File")
                                        End If
                                    End If
                                    wbFileOutputItem.GMR = If(IsDBNull(CDec(currentField)), 0, CDec(currentField))
                                Else
                                    If currentField.Trim().ToUpper() <> "GMR" Then
                                        Throw New ApplicationException("Invalid row header GMR")
                                    End If
                                End If

                            Case 19
                                If indexRow <> 0 Then
                                    If Not IsDBNull(currentField) And Len(currentField) <> 0 Then
                                        If Not IsNumeric(currentField) Then
                                            Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid MF_RATE found in File")
                                        End If
                                    End If
                                    wbFileOutputItem.MarketFeesRate = If(IsDBNull(CDec(currentField)), 0, CDec(currentField))
                                Else
                                    If currentField.Trim().ToUpper() <> "MF_RATE" Then
                                        Throw New ApplicationException("Invalid row header MF_RATE")
                                    End If
                                End If

                            Case 20
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow.ToString() & "." & "Invalid SELLER/BUYER found in File")
                                    End If
                                    wbFileOutputItem.SellerOrBuyer = CStr(currentField)
                                Else
                                    If currentField.Trim().ToUpper() <> "SELLER/BUYER" Then
                                        Throw New ApplicationException("Invalid row header for SELLER")
                                    End If
                                End If

                            Case 21
                                If indexRow <> 0 Then
                                    If Not IsDBNull(currentField) And Len(currentField) <> 0 Then
                                        If Not IsNumeric(currentField) Then
                                            Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid SPOT_QTY found in File")
                                        End If
                                    End If
                                    wbFileOutputItem.SpotQty = If(IsDBNull(CDec(currentField)), 0, CDec(currentField))
                                Else
                                    If currentField.Trim().ToUpper() <> "SPOT_QTY" Then
                                        Throw New ApplicationException("Invalid row header SPOT_QTY")
                                    End If
                                End If

                            Case 22
                                If indexRow <> 0 Then
                                    If Not IsDBNull(currentField) And Len(currentField) <> 0 Then
                                        If Not IsNumeric(currentField) Then
                                            Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid GENX_AMT found in File")
                                        End If
                                    End If
                                    wbFileOutputItem.GenXAmount = If(IsDBNull(CDec(currentField)), 0, CDec(currentField))
                                Else
                                    If currentField.Trim().ToUpper() <> "GENX_AMT" Then
                                        Throw New ApplicationException("Invalid row header GENX_AMT")
                                    End If
                                End If

                            Case 23
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow.ToString() & "." & "Invalid INVOICE_ID found in File")
                                    End If
                                    wbFileOutputItem.InvoiceID = CStr(currentField)
                                Else
                                    If currentField.Trim().ToUpper() <> "INVOICE_ID" Then
                                        Throw New ApplicationException("Invalid row header for INVOICE_ID")
                                    End If
                                End If
                        End Select
                        indexColumn += 1
                    Next
                    'Add the WESM Bil output
                    If indexRow <> 0 Then
                        listWBFileOutputItem.Add(wbFileOutputItem)
                    End If
                    listWBFileOutputItem.TrimExcess()

                    'Increment the Row Index
                    indexRow += 1
                    Dim newProgress As ProgressClass = New ProgressClass
                    newProgress.ProgressIndicator = (indexRow * 100 / lineCount)
                    newProgress.ProgressMsg = "Reading data " & indexRow & " out of " & lineCount
                    progress.Report(newProgress)
                Catch ex1 As Microsoft.VisualBasic.
                                               FileIO.MalformedLineException
                Catch ex As Exception
                    Throw New ApplicationException(ex.Message)
                End Try
            End While
        End Using

        Await Task.Run(Sub() Me.GenerateWESMBills(bpNo, stlRun, wesmBilltransType, fileType, transDate, dueDate, remarks, listWBFileOutputItem))
    End Sub

    Private Sub GenerateWESMBills(ByVal bpNo As Integer, ByVal stlRun As String, ByVal wesmBilltransType As String, ByVal fileType As EnumFileType, ByVal transDate As Date,
                                  ByVal dueDate As Date, ByVal remarks As String, ByVal listWBFileOutput As List(Of WESMBillFileOutput))
        Dim listFinalWESMBill As New List(Of WESMBill)
        Dim listWESMCoverSummary As New List(Of WESMBillAllocCoverSummary)
        Dim dicParticipants As New Dictionary(Of String, Integer)
        Dim summaryNo As Integer = 0
        Dim getWBSChangeParentList As List(Of WESMBillSummaryChangeParentId) = (From x In WBillHelper.GetWESMBillSummaryChangeParentIDAll() Where x.Status = EnumStatus.Active Select x).ToList()
        Dim groupListSellerBuyerInformation As List(Of SellerBuyerInformation) = listWBFileOutput.
                                                                                GroupBy(Function(sbInfo) New With {
                                                                                                          Key sbInfo.StlID, Key sbInfo.BillingId,
                                                                                                          Key sbInfo.InvoiceID, Key sbInfo.FacilityType,
                                                                                                          Key sbInfo.WHTTag, Key sbInfo.ITHTag,
                                                                                                          Key sbInfo.NonVatableTag, Key sbInfo.ZeroRatedTag,
                                                                                                          Key sbInfo.NetSellerBuyerTag}).
                                                                                Select(Function(x) New SellerBuyerInformation With {
                                                                                       .StlID = x.Key.StlID, .BillingId = x.Key.BillingId,
                                                                                       .InvoiceID = x.Key.InvoiceID, .FacilityType = x.Key.FacilityType,
                                                                                       .WHTTag = x.Key.WHTTag, .ITHTag = x.Key.ITHTag,
                                                                                       .NonVatableTag = x.Key.NonVatableTag, .ZeroRatedTag = x.Key.ZeroRatedTag,
                                                                                       .NetSellerBuyerTag = x.Key.NetSellerBuyerTag}).OrderBy(Function(z) z.BillingId).
                                                                                ToList

        'Get Seller Transaction Cover Summary
        For Each item In groupListSellerBuyerInformation.Where(Function(x) x.NetSellerBuyerTag.ToUpper = "SELLER").ToList
            summaryNo += 1
            If Not dicParticipants.ContainsKey(item.BillingId) Then
                dicParticipants.Add(item.BillingId, summaryNo)
            End If
            Dim wesmAllocCoverSummary As New WESMBillAllocCoverSummary
            Dim wesmAllocDissagDetailsList As New List(Of WESMBillAllocDisaggDetails)
            Dim getWESMBillFileOutputPerBillingId As List(Of WESMBillFileOutput) = listWBFileOutput.
                                                                                   Where(Function(x) x.BillingId = item.BillingId And x.InvoiceID = item.InvoiceID And x.StlID = item.StlID And x.NetSellerBuyerTag.ToUpper = "SELLER").
                                                                                   Select(Function(z) z).ToList
            For Each dtl In getWESMBillFileOutputPerBillingId
                Dim getSellerInfo As SellerBuyerInformation = groupListSellerBuyerInformation.Where(Function(x) x.BillingId = dtl.SellerOrBuyer).First
                Dim wesmAllocDissagDetails As New WESMBillAllocDisaggDetails
                With wesmAllocDissagDetails
                    Dim changeSTLID As String = getWBSChangeParentList.
                                                Where(Function(x) x.BillingPeriod = bpNo And x.ParentParticipants.IDNumber = getSellerInfo.BillingId And x.ChildParticipants.IDNumber = getSellerInfo.InvoiceID).
                                                Select(Function(y) y.NewParentParticipants.IDNumber).FirstOrDefault
                    If changeSTLID Is Nothing Then
                        .STLID = getSellerInfo.StlID
                    Else
                        .STLID = changeSTLID.Replace("_" & AMModule.FITParticipantCode.ToString, "")
                    End If
                    .BillingID = getSellerInfo.BillingId
                    .FacilityType = getSellerInfo.FacilityType
                    .WHTTag = getSellerInfo.WHTTag
                    .ITHTag = getSellerInfo.ITHTag
                    .NonVatableTag = getSellerInfo.NonVatableTag
                    .ZeroRatedTag = getSellerInfo.ZeroRatedTag
                    .NetSellerBuyerTag = getSellerInfo.NetSellerBuyerTag
                    .VatableSales = dtl.VatableSales
                    .ZeroRatedSales = dtl.ZeroRatedSales
                    .ZeroRatedEcoZoneSales = dtl.ZeroRatedEcoZoneSales
                    .VatablePurchases = 0
                    .ZeroRatedPurchases = 0
                    .ZeroRatedEcoZonePurchases = 0
                    .VatOnSales = dtl.VatOnSales
                    .VatOnPurchases = 0
                    .EWT = dtl.EWT
                    wesmAllocDissagDetailsList.Add(wesmAllocDissagDetails)
                End With
            Next
            wesmAllocDissagDetailsList.TrimExcess()
            With wesmAllocCoverSummary
                .SummaryId = summaryNo
                .BillingPeriod = bpNo
                .STLRun = stlRun
                .StlID = item.StlID
                .BillingID = item.BillingId
                .NonVatableTag = item.NonVatableTag
                .ZeroRatedTag = item.ZeroRatedTag
                .WHT = item.WHTTag
                .ITH = item.ITHTag
                .NetSellerBuyerTag = item.NetSellerBuyerTag
                .TransactionDate = transDate
                .DueDate = dueDate
                .TransactionNo = "TS-" & wesmBilltransType.ToUpper.ToString() & "-" & bpNo & stlRun.ToUpper.ToString() & "-" & summaryNo.ToString("D7")
                .VatableSales = getWESMBillFileOutputPerBillingId.Select(Function(x) x.VatableSales).Sum()
                .ZeroRatedSales = getWESMBillFileOutputPerBillingId.Select(Function(x) x.ZeroRatedSales).Sum()
                .ZeroRatedEcoZoneSales = getWESMBillFileOutputPerBillingId.Select(Function(x) x.ZeroRatedEcoZoneSales).Sum()
                .VatablePurchases = 0
                .ZeroRatedPurchases = 0
                .VatOnSales = getWESMBillFileOutputPerBillingId.Select(Function(x) x.VatOnSales).Sum()
                .VatOnPurchases = 0
                .NSSFlowBack = getWESMBillFileOutputPerBillingId.Select(Function(x) x.NSS).First
                .EWTSales = getWESMBillFileOutputPerBillingId.Select(Function(x) x.EWT).Sum()
                .EWTPurchases = 0
                .GMR = getWESMBillFileOutputPerBillingId.Select(Function(x) x.GMR).First
                .SpotQty = getWESMBillFileOutputPerBillingId.Select(Function(x) x.SpotQty).First
                .MarketFeesRate = getWESMBillFileOutputPerBillingId.Select(Function(x) x.MarketFeesRate).First
                .GenXAmount = getWESMBillFileOutputPerBillingId.Select(Function(x) x.GenXAmount).First
                .Remarks = remarks
                .GenXAmount = getWESMBillFileOutputPerBillingId.Select(Function(x) x.GenXAmount).First
                .ListWBAllocDisDetails = wesmAllocDissagDetailsList
            End With
            listWESMCoverSummary.Add(wesmAllocCoverSummary)
        Next

        'Get Buyer Transaction Cover Summary
        For Each item In groupListSellerBuyerInformation.Where(Function(x) x.NetSellerBuyerTag.ToUpper = "BUYER").ToList
            summaryNo += 1
            If Not dicParticipants.ContainsKey(item.BillingId) Then
                dicParticipants.Add(item.BillingId, summaryNo)
            End If
            Dim wesmAllocCoverSummary As New WESMBillAllocCoverSummary
            Dim wesmAllocDissagDetailsList As New List(Of WESMBillAllocDisaggDetails)
            Dim getWESMBillFileOutputPerBillingId As List(Of WESMBillFileOutput) = listWBFileOutput.
                                                                                   Where(Function(x) x.BillingId = item.BillingId And x.InvoiceID = item.InvoiceID And x.StlID = item.StlID And x.NetSellerBuyerTag.ToUpper = "BUYER").
                                                                                   Select(Function(z) z).ToList
            For Each dtl In getWESMBillFileOutputPerBillingId
                Dim getBuyerInfo As SellerBuyerInformation = groupListSellerBuyerInformation.Where(Function(x) x.BillingId = dtl.SellerOrBuyer).First
                Dim wesmAllocDissagDetails As New WESMBillAllocDisaggDetails
                With wesmAllocDissagDetails
                    Dim changeSTLID As String = getWBSChangeParentList.
                                                Where(Function(x) x.BillingPeriod = bpNo And x.ParentParticipants.IDNumber = getBuyerInfo.BillingId And x.ChildParticipants.IDNumber = getBuyerInfo.InvoiceID).
                                                Select(Function(y) y.NewParentParticipants.IDNumber).FirstOrDefault
                    If changeSTLID Is Nothing Then
                        .STLID = getBuyerInfo.StlID
                    Else
                        .STLID = changeSTLID.Replace("_" & AMModule.FITParticipantCode.ToString, "")
                    End If
                    .BillingID = getBuyerInfo.BillingId
                    .FacilityType = getBuyerInfo.FacilityType
                    .WHTTag = getBuyerInfo.WHTTag
                    .ITHTag = getBuyerInfo.ITHTag
                    .NonVatableTag = getBuyerInfo.NonVatableTag
                    .ZeroRatedTag = getBuyerInfo.ZeroRatedTag
                    .NetSellerBuyerTag = getBuyerInfo.NetSellerBuyerTag
                    .VatableSales = 0
                    .ZeroRatedSales = 0
                    .ZeroRatedEcoZoneSales = 0
                    .VatablePurchases = dtl.VatablePurchases
                    .ZeroRatedPurchases = dtl.ZeroRatedPurchases
                    .ZeroRatedEcoZonePurchases = dtl.ZeroRatedEcoZonePurchases
                    .VatOnSales = 0
                    .VatOnPurchases = dtl.VatOnPurchases
                    .EWT = dtl.EWT
                    wesmAllocDissagDetailsList.Add(wesmAllocDissagDetails)
                End With
            Next
            wesmAllocDissagDetailsList.TrimExcess()
            With wesmAllocCoverSummary
                .SummaryId = summaryNo
                .BillingPeriod = bpNo
                .STLRun = stlRun
                .StlID = item.StlID
                .BillingID = item.BillingId
                .NonVatableTag = item.NonVatableTag
                .ZeroRatedTag = item.ZeroRatedTag
                .WHT = item.WHTTag
                .ITH = item.ITHTag
                .NetSellerBuyerTag = item.NetSellerBuyerTag
                .TransactionDate = transDate
                .DueDate = dueDate
                .TransactionNo = "TS-" & wesmBilltransType.ToUpper.ToString() & "-" & bpNo & stlRun.ToUpper.ToString() & "-" & summaryNo.ToString("D7")
                .VatableSales = 0
                .ZeroRatedSales = 0
                .ZeroRatedEcoZoneSales = 0
                .VatablePurchases = getWESMBillFileOutputPerBillingId.Select(Function(x) x.VatablePurchases).Sum()
                .ZeroRatedPurchases = getWESMBillFileOutputPerBillingId.Select(Function(x) x.ZeroRatedPurchases).Sum() + getWESMBillFileOutputPerBillingId.Select(Function(x) x.ZeroRatedEcoZonePurchases).Sum()
                .VatOnSales = 0
                .VatOnPurchases = getWESMBillFileOutputPerBillingId.Select(Function(x) x.VatOnPurchases).Sum()
                .NSSFlowBack = getWESMBillFileOutputPerBillingId.Select(Function(x) x.NSS).First
                .EWTSales = 0
                .EWTPurchases = getWESMBillFileOutputPerBillingId.Select(Function(x) x.EWT).Sum
                .GMR = getWESMBillFileOutputPerBillingId.Select(Function(x) x.GMR).First
                .SpotQty = getWESMBillFileOutputPerBillingId.Select(Function(x) x.SpotQty).First
                .MarketFeesRate = getWESMBillFileOutputPerBillingId.Select(Function(x) x.MarketFeesRate).First
                .GenXAmount = getWESMBillFileOutputPerBillingId.Select(Function(x) x.GenXAmount).First
                .Remarks = remarks
                .GenXAmount = getWESMBillFileOutputPerBillingId.Select(Function(x) x.GenXAmount).First
                .ListWBAllocDisDetails = wesmAllocDissagDetailsList
            End With
            listWESMCoverSummary.Add(wesmAllocCoverSummary)
        Next

        For Each item In listWESMCoverSummary
            If fileType = EnumFileType.Energy Then
                Dim wesmBillItemEnergy As New WESMBill
                With wesmBillItemEnergy
                    .BillingPeriod = item.BillingPeriod
                    .SettlementRun = item.STLRun
                    .IDNumber = item.StlID
                    .ChargeType = EnumChargeType.E
                    .RegistrationID = item.BillingID
                    .InvoiceDate = item.TransactionDate
                    .InvoiceNumber = item.TransactionNo
                    .DueDate = item.DueDate
                    .Amount = Math.Round(item.VatableSales + item.ZeroRatedSales + item.ZeroRatedEcoZoneSales + item.VatablePurchases + item.ZeroRatedPurchases + item.ZeroRatedEcoZonePurchases, 2)
                    .MarketFeesRate = item.MarketFeesRate
                    .Remarks = item.Remarks
                End With
                listFinalWESMBill.Add(wesmBillItemEnergy)
                Dim wesmBillItemVAT As New WESMBill
                With wesmBillItemVAT
                    .BillingPeriod = item.BillingPeriod
                    .SettlementRun = item.STLRun
                    .IDNumber = item.StlID
                    .ChargeType = EnumChargeType.EV
                    .RegistrationID = item.BillingID
                    .InvoiceDate = item.TransactionDate
                    .InvoiceNumber = item.TransactionNo
                    .DueDate = item.DueDate
                    .Amount = Math.Round(item.VatOnSales + item.VatOnPurchases, 2)
                    .MarketFeesRate = item.MarketFeesRate
                    .Remarks = item.Remarks
                End With
                listFinalWESMBill.Add(wesmBillItemVAT)
            End If
        Next
        listFinalWESMBill.TrimExcess()
        Me._NewListWBAllocCoverSummary = listWESMCoverSummary
        Me._NewListWESMBill = listFinalWESMBill
    End Sub

    Public Sub SaveUplodedWESMBill(ByVal calendarBP As CalendarBillingPeriod, ByVal stlRun As String, ByVal chargeType As EnumChargeType,
                                   ByVal filetype As EnumFileType, ByVal itemJV As JournalVoucher, ByVal itemGP As WESMBillGPPosted,
                                   ByVal countWESMBillExist As Integer, ByVal progress As IProgress(Of ProgressClass), ByVal ct As CancellationToken)
        Dim report As New DataReport
        Dim listSQL As New List(Of String)
        Dim dicInvRef As New Dictionary(Of String, Long)
        Dim ds As New DataSet
        Dim SQL As String
        Dim batchCode As String

        Dim newProgress As ProgressClass = New ProgressClass
        newProgress.ProgressMsg = "Please wait while preparing SQL Scripts to save in AMS Database..."
        progress.Report(newProgress)

        Dim bpValue As String = calendarBP.BillingPeriod.ToString() & "(" & FormatDateTime(calendarBP.StartDate, DateFormat.ShortDate) &
                                " - " & FormatDateTime(calendarBP.EndDate, DateFormat.ShortDate) & ")"
        Try
            'For Deletion of exsting records in AM_WESM_Bill
            'Generate also the Revision Number
            Select Case chargeType
                Case EnumChargeType.E
                    If countWESMBillExist <> 0 Then
                        'For Deletion
                        SQL = "DELETE FROM AM_WESM_BILL  " &
                          "WHERE (CHARGE_TYPE = '" & EnumChargeType.E.ToString() & "' " &
                          "       OR CHARGE_TYPE = '" & EnumChargeType.EV.ToString() & "') AND " &
                          "       BILLING_PERIOD = " & calendarBP.BillingPeriod & " " &
                          "       AND STL_RUN = '" & stlRun & "'"
                        listSQL.Add(SQL)

                        SQL = "DELETE FROM AM_WESM_ALLOC_DISAGG_DETAILS " & vbNewLine _
                        & "WHERE SUMMARY_ID >= (SELECT MIN(SUMMARY_ID) FROM AM_WESM_ALLOC_COVER_SUMMARY WHERE BILLING_PERIOD = " & calendarBP.BillingPeriod & " AND STL_RUN = '" & stlRun & "') " & vbNewLine _
                        & "AND SUMMARY_ID <= (SELECT MAX(SUMMARY_ID) FROM AM_WESM_ALLOC_COVER_SUMMARY WHERE BILLING_PERIOD = " & calendarBP.BillingPeriod & " AND STL_RUN = '" & stlRun & "')"
                        listSQL.Add(SQL)

                        SQL = "DELETE FROM AM_WESM_ALLOC_COVER_SUMMARY " & vbNewLine _
                         & "WHERE BILLING_PERIOD = " & calendarBP.BillingPeriod & " " & vbNewLine _
                         & "AND STL_RUN = '" & stlRun & "'"
                        listSQL.Add(SQL)
                    End If
            End Select

            'Get the Batch Code per charge type
            batchCode = EnumPostedType.U.ToString() & "-" & WBillHelper.GetSequenceID("SEQ_AM_BATCH_CODE").ToString()

            'For deletion of Journal Voucher if there are records to be replaced
            SQL = "UPDATE AM_JV SET STATUS = " & EnumStatus.InActive & " " &
                  "WHERE STATUS = " & EnumStatus.Active & " AND BATCH_CODE = " &
                  "                   (SELECT BATCH_CODE FROM AM_WESM_BILL_GP_POSTED " &
                  "                    WHERE CHARGE_TYPE = '" & chargeType.ToString() & "' " &
                  "                    AND BILLING_PERIOD = " & calendarBP.BillingPeriod & " " &
                  "                    AND STL_RUN = '" & stlRun & "') "
            listSQL.Add(SQL)

            'For Deletion in WESM Bill GP Posted
            SQL = "DELETE FROM AM_WESM_BILL_GP_POSTED " &
                  "WHERE CHARGE_TYPE = '" & chargeType.ToString() & "' AND BILLING_PERIOD = " & calendarBP.BillingPeriod & " " &
                  "AND STL_RUN = '" & stlRun & "'"
            listSQL.Add(SQL)

            Dim getWBSChangeParentList As List(Of WESMBillSummaryChangeParentId) = (From x In WBillHelper.GetWESMBillSummaryChangeParentIDAll() Where x.Status = EnumStatus.Active Select x).ToList()
            'Data for WESM Bills
            For Each item In Me.NewListWESMBill
                Dim amcode As String = calendarBP.BillingPeriod.ToString() & "-" &
                                       item.IDNumber & "-" & WBillHelper.GetSequenceID("SEQ_AM_CODE").ToString()
                With item
                    'Get the unique key to set invoice no
                    Dim UniqueKey = .IDNumber & .RegistrationID & .InvoiceDate.ToString("MMddyyyy") & .DueDate.ToString("MMddyyyy")

                    Dim getParentID As String = (From x In getWBSChangeParentList
                                                 Where x.BillingPeriod = item.BillingPeriod And x.ParentParticipants.IDNumber = item.IDNumber And x.ChildParticipants.IDNumber = item.RegistrationID
                                                 Select x.NewParentParticipants.IDNumber).FirstOrDefault
                    Dim parentID As String = ""
                    If getParentID IsNot Nothing Then
                        parentID = getParentID
                    Else
                        parentID = .IDNumber
                    End If
                    'Get the final invoice no
                    SQL = "INSERT INTO AM_WESM_BILL(BATCH_CODE,AM_CODE,BILLING_PERIOD,STL_RUN,ID_NUMBER,REG_ID,INVOICE_NO,INVOICE_DATE,AMOUNT,CHARGE_TYPE,DUE_DATE,MARKET_FEES_RATE,REMARKS,UPDATED_BY)" & vbNewLine _
                        & "SELECT '" & batchCode & "', '" & amcode & "'," & .BillingPeriod & ",'" & .SettlementRun & "', '" & parentID & "','" & .RegistrationID & "','" & .InvoiceNumber & "'," & vbNewLine _
                        & "TO_DATE('" & .InvoiceDate & "','MM/DD/yyyy')," & .Amount & ",'" & .ChargeType.ToString & "',TO_DATE('" & .DueDate & "','MM/DD/yyyy')," & .MarketFeesRate & ",'" & .Remarks & "','" & AMModule.UserName & "' FROM DUAL"
                    listSQL.Add(SQL)
                End With
            Next

            Dim jvNo As Long = WBillHelper.GetSequenceID("SEQ_AM_JV_NO")
            With itemJV
                SQL = "INSERT INTO AM_JV (AM_JV_NO,BATCH_CODE,STATUS,PREPARED_BY,CHECKED_BY,APPROVED_BY,UPDATED_BY,POSTED_TYPE)" & vbNewLine _
                    & "SELECT " & jvNo & ",'" & batchCode & "',1,'" & AMModule.UserName & "','" & .CheckedBy & "','" & .ApprovedBy & "','" & vbNewLine _
                            & AMModule.UserName & "','" & .PostedType & "' FROM DUAL"
                listSQL.Add(SQL)

                For Each item In itemJV.JVDetails
                    SQL = "INSERT INTO AM_JV_DETAILS(AM_JV_NO,ACCT_CODE,DEBIT,CREDIT,UPDATED_BY)" & vbNewLine _
                        & "SELECT " & jvNo & ",'" & item.AccountCode & "'," & item.Debit & "," & item.Credit & ",'" & AMModule.UserName & "' FROM DUAL"
                    listSQL.Add(SQL)
                Next
            End With

            Dim chargeTypeValue As String = ""
            With itemGP
                'Get the charge type
                Select Case chargeType
                    Case EnumChargeType.E
                        chargeTypeValue = "Energy"
                        'Case EnumChargeType.MF
                        '    chargeTypeValue = "Market Fees"
                End Select
                Dim remarks As String = "Final Statement for " & chargeTypeValue & "." &
                                         " Billing Period = " & bpValue &
                                         ", Settlement Run = " & stlRun & ", Due Date = " & .DueDate.ToString("MM/dd/yyyy") &
                                         " and Batch Code = " & batchCode

                SQL = "INSERT INTO AM_WESM_BILL_GP_POSTED (BILLING_PERIOD,STL_RUN,CHARGE_TYPE,DUE_DATE,REMARKS,POSTED,UPDATED_BY,BATCH_CODE,POSTED_TYPE,DOCUMENT_AMOUNT,AM_JV_NO)" & vbNewLine _
                    & "SELECT " & calendarBP.BillingPeriod & ",'" & stlRun & "','" & .Charge.ToString & "',TO_DATE('" & .DueDate & "','MM/DD/yyyy')," & vbNewLine _
                    & "'" & remarks & "',0" & ",'" & AMModule.UserName & "','" & batchCode & "','" & .PostType & "'," & vbNewLine _
                    & .DocumentAmount & "," & jvNo & " FROM DUAL"
                listSQL.Add(SQL)
            End With

            For Each item In Me.NewListWBAllocCoverSummary
                Dim summaryNo As Long = Me.WBillHelper.GetSequenceID("SEQ_AM_SUMMARY_ID")
                SQL = "INSERT INTO AM_WESM_ALLOC_COVER_SUMMARY(SUMMARY_ID,STL_RUN,GROUP_ID,BILLING_PERIOD,STL_ID,BILLING_ID,NON_VATABLE_TAG,ZERO_RATED_TAG,WHT_TAG,ITH_TAG,NET_SELLER_BUYER_TAG," & vbNewLine _
                                                               & "TRANSACTION_NUMBER,TRANSACTION_DATE,DUE_DATE,VATABLE_SALES,ZERO_RATED_SALES,ZERO_RATED_ECOZONE_SALES,VATABLE_PURCHASES,ZERO_RATED_PURCHASES," & vbNewLine _
                                                               & "NSS_FLOWBACK,VAT_ON_SALES,VAT_ON_PURCHASES,EWT_SALES,EWT_PURCHASES,NET_ENERGY_QTY,GMR,MARKET_FEES_RATE,REMARKS,UPLOAD_FROM,SPOT_QTY,GENX_AMT)" & vbNewLine _
                      & "SELECT " & summaryNo & ", '" & item.STLRun & "', ''," & item.BillingPeriod & ", '" & item.StlID & "', '" & item.BillingID & "', '" & item.NonVatableTag & "', '" & item.ZeroRatedTag & "', " & vbNewLine _
                                  & "'" & item.WHT & "', '" & item.ITH & "', '" & item.NetSellerBuyerTag & "', '" & item.TransactionNo & "', TO_DATE('" & item.TransactionDate.ToShortDateString & "','MM/DD/YYYY'), " & vbNewLine _
                                  & "TO_DATE('" & item.DueDate.ToShortDateString & "','MM/DD/YYYY'), " & item.VatableSales & ", " & item.ZeroRatedSales & ", " & item.ZeroRatedEcoZoneSales & ", " & item.VatablePurchases & vbNewLine _
                                  & ", " & item.ZeroRatedPurchases & ", " & item.NSSFlowBack & ", " & item.VatOnSales & ", " & item.VatOnPurchases & ", " & item.EWTSales & ", " & item.EWTPurchases & vbNewLine _
                                  & ", 0, " & item.GMR & ", " & item.MarketFeesRate & ", '" & item.Remarks & "', 'FLATFILE'," & item.SpotQty & "," & item.GenXAmount & " FROM DUAL"
                listSQL.Add(SQL)
                For Each dtl In item.ListWBAllocDisDetails
                    SQL = "INSERT INTO AM_WESM_ALLOC_DISAGG_DETAILS(SUMMARY_ID,BILLING_PERIOD,BILLING_ID,FACILITY_TYPE,WHT,ITH,NON_VATABLE_TAG,ZERO_RATED_TAG, NET_SELLER_BUYER_TAG,VATABLE_SALES,ZERO_RATED_SALES,ZERO_RATED_ECOZONE_SALES," & vbNewLine _
                                                                    & "VAT_ON_SALES,VATABLE_PURCHASES,ZERO_RATED_PURCHASES,ZERO_RATED_ECOZONE_PURCHASES,VAT_ON_PURCHASES,EWT,STL_ID) " & vbNewLine _
                                & "SELECT " & summaryNo & ", " & item.BillingPeriod & ", '" & dtl.BillingID & "', '" & dtl.FacilityType & "', '" & dtl.WHTTag & "', '" & dtl.ITHTag & "', '" & dtl.NonVatableTag & "', " & vbNewLine _
                                    & "'" & dtl.ZeroRatedTag & "', '" & dtl.NetSellerBuyerTag & "', " & dtl.VatableSales & ", " & dtl.ZeroRatedSales & ", " & dtl.ZeroRatedEcoZoneSales & ", " & dtl.VatOnSales & ", " & dtl.VatablePurchases & ", " & vbNewLine _
                                    & dtl.ZeroRatedPurchases & ", " & dtl.ZeroRatedEcoZonePurchases & ", " & dtl.VatOnPurchases & ", " & dtl.EWT & ",'" & dtl.STLID & "' FROM DUAL"
                    listSQL.Add(SQL)
                Next
            Next

            report = Me.DataAccess.ExecuteSaveQuery2(listSQL, progress, ct)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            newProgress = New ProgressClass
            newProgress.ProgressMsg = "Successfully saved!"
            progress.Report(newProgress)

        Catch ex As Exception
            newProgress = New ProgressClass
            newProgress.ProgressMsg = "Error Encountered"
            progress.Report(newProgress)
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub
#End Region
End Class
