
#Region " Information "
' Class Name : Excel File Handler
' Programmer : Vivek Purohit
' Purpose    : Handle Excel File Operations.
' Date       : 20-Dec-2008
#End Region

#Region " Import Section"
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.OleDb
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Reflection
Imports System.Runtime.InteropServices

#End Region

' <summary>
' Excel File handler used to read and write excel file.
' </summary>
' <remarks></remarks>
' 
Public Class ExcelHandler

#Region "Single Instance Code"
    ' <summary>
    ' This variable stores the reference of the single instance
    ' </summary>
    ' <remarks></remarks>
    Private Shared m_Instance As ExcelHandler = Nothing

    ' <summary>
    ' Gets the current instance of this class
    ' Dependencies:
    '  None
    '   
    '  Output
    '   the reference instance
    ' </summary>
    ' <returns>
    ' The single instance of this class
    ' </returns>
    ' <remarks></remarks>
    Public Shared Function GetInstance() As ExcelHandler
        If m_Instance Is Nothing Then
            m_Instance = New ExcelHandler()
        End If
        Return m_Instance
    End Function

    Private _WBillHelper As WESMBillHelper
    ' <summary>
    ' gets the DataAccessLayer
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public ReadOnly Property WBillHelper() As WESMBillHelper
        Get
            Return Me._WBillHelper
        End Get
    End Property

    Private _BFactory As BusinessFactory
    ' <summary>
    ' gets the DataAccessLayer
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public ReadOnly Property BFactory() As BusinessFactory
        Get
            Return Me._BFactory
        End Get
    End Property
#End Region

#Region "Initialization/Constructor"
    Public Sub New()
        Me._WBillHelper = WESMBillHelper.GetInstance()
        Me._BFactory = BusinessFactory.GetInstance()
    End Sub
#End Region

    ' <summary>
    ' Return data in dataset from excel file.
    ' </summary>
    ' <param name="a_sFilepath">Excel file name for extract data.</param>
    ' <returns>DataSet</returns>
    Public Function GetDataFromExcel(ByVal a_sFilepath As String) As DataSet
        Dim ds As New DataSet()
        Dim cn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & a_sFilepath & ";Extended Properties= Excel 8.0")
        Try
            cn.Open()
        Catch ex As OleDbException
            Console.WriteLine(ex.Message)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        ' It Represents Excel data table Schema.
        Dim dt As New System.Data.DataTable()
        dt = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
            For sheet_count As Integer = 0 To dt.Rows.Count - 1
                Try
                    ' Create Query to get Data from sheet.
                    Dim sheetname As String = dt.Rows(sheet_count)("table_name").ToString()
                    Dim da As New OleDbDataAdapter("SELECT * FROM [" & sheetname & "]", cn)
                    da.Fill(ds, sheetname)
                Catch ex As DataException
                    Console.WriteLine(ex.Message)
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try
            Next
        End If
        cn.Close()
        Return ds
    End Function

    ' <summary>
    ' Write Excel file as given file name with given data.
    ' </summary>
    ' <param name="a_sFilename">full file name for create excel file.</param>
    ' <param name="a_sData">data in dataset to be fill in excel shhet.</param>
    ' <param name="a_sFileTitle">Title of Excel file.</param>
    ' <param name="a_sErrorMessage">output parameter contains error message if error occurrs.</param>
    ' <returns>bool</returns>
    Public Function ExportToExcel(ByVal a_sFilename As String, ByVal a_sData As DataSet, ByVal a_sFileTitle As String, ByRef a_sErrorMessage As String, _
                                  ByVal a_ColNumber As Integer) As Boolean
        a_sErrorMessage = String.Empty
        Dim bRetVal As Boolean = False
        Dim dsDataSet As DataSet = Nothing
        Try
            dsDataSet = a_sData

            Dim xlObject As Excel.Application = Nothing
            Dim xlWB As Excel.Workbook = Nothing
            Dim xlSh As Excel.Worksheet = Nothing
            Dim rg As Excel.Range = Nothing

            Try
                xlObject = New Excel.Application()
                xlObject.AlertBeforeOverwriting = False
                xlObject.DisplayAlerts = False

                'This Adds a new woorkbook, you could open the workbook from file also
                xlWB = xlObject.Workbooks.Add(Type.Missing)
                xlWB.SaveAs(a_sFilename, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, _
                Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value)

                xlSh = DirectCast(xlObject.ActiveWorkbook.ActiveSheet, Excel.Worksheet)

                Dim sUpperRange As String = "A1"
                Dim sLastCol As String = IntegerToExcelColumnCode(a_ColNumber)
                Dim sLowerRange As String = sLastCol + (dsDataSet.Tables(0).Rows.Count + 1).ToString()

                rg = xlSh.Range(sUpperRange, sLowerRange)
                rg.Value2 = GetData(dsDataSet.Tables(0))

                ' formatting
                xlSh.Range("A1", sLastCol & "1").Font.Bold = True
                xlSh.Range("A1", sLastCol & "1").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                xlSh.Range(sUpperRange, sLowerRange).EntireColumn.AutoFit()

                If String.IsNullOrEmpty(a_sFileTitle) Then
                    xlObject.Caption = "untitled"
                Else
                    xlObject.Caption = a_sFileTitle
                End If

                xlWB.Save()
                bRetVal = True
            Catch ex As System.Runtime.InteropServices.COMException
                If ex.ErrorCode = -2147221164 Then
                    a_sErrorMessage = "Error in export: Please install Microsoft Office (Excel) to use the Export to Excel feature."
                ElseIf ex.ErrorCode = -2146827284 Then
                    a_sErrorMessage = "Error in export: Excel allows only 65,536 maximum rows in a sheet."
                Else
                    a_sErrorMessage = (("Error in export: " & ex.Message) + Environment.NewLine & " Error: ") + ex.ErrorCode
                End If
            Catch ex As Exception
                a_sErrorMessage = "Error in export: " & ex.Message
            Finally
                Try
                    If xlWB IsNot Nothing Then
                        xlWB.Close(Nothing, Nothing, Nothing)
                    End If
                    xlObject.Workbooks.Close()
                    xlObject.Quit()
                    If rg IsNot Nothing Then
                        Marshal.ReleaseComObject(rg)
                    End If
                    If xlSh IsNot Nothing Then
                        Marshal.ReleaseComObject(xlSh)
                    End If
                    If xlWB IsNot Nothing Then
                        Marshal.ReleaseComObject(xlWB)
                    End If
                    If xlObject IsNot Nothing Then
                        Marshal.ReleaseComObject(xlObject)
                    End If

                Catch
                End Try
                xlSh = Nothing
                xlWB = Nothing
                xlObject = Nothing
                ' force final cleanup!
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try
        Catch ex As Exception
            a_sErrorMessage = "Error in export: " & ex.Message
        End Try

        Return bRetVal
    End Function

    ' <summary>
    ' returns data as two dimentional object array.
    ' </summary>
    ' <param name="a_dtData">DataTable of data.</param>
    ' <returns>Object Array</returns>
    Private Function GetData(ByVal a_dtData As System.Data.DataTable) As Object(,)
        Dim obj As Object(,) = New Object((a_dtData.Rows.Count + 1) - 1, a_dtData.Columns.Count - 1) {}

        Try
            For j As Integer = 0 To a_dtData.Columns.Count - 1
                obj(0, j) = a_dtData.Columns(j).Caption
            Next

            Dim dt As New DateTime()
            Dim sTmpStr As String = String.Empty

            For i As Integer = 1 To a_dtData.Rows.Count
                For j As Integer = 0 To a_dtData.Columns.Count - 1
                    If a_dtData.Columns(j).DataType Is dt.[GetType]() Then
                        If a_dtData.Rows(i - 1)(j) IsNot DBNull.Value Then
                            DateTime.TryParse(a_dtData.Rows(i - 1)(j).ToString(), dt)
                            obj(i, j) = dt.ToString("MM/dd/yy hh:mm tt")
                        Else
                            obj(i, j) = a_dtData.Rows(i - 1)(j)
                        End If
                    ElseIf a_dtData.Columns(j).DataType Is sTmpStr.[GetType]() Then
                        If a_dtData.Rows(i - 1)(j) IsNot DBNull.Value Then
                            sTmpStr = a_dtData.Rows(i - 1)(j).ToString().Replace(vbCr, "")
                            obj(i, j) = sTmpStr
                        Else
                            obj(i, j) = a_dtData.Rows(i - 1)(j)
                        End If
                    Else
                        obj(i, j) = a_dtData.Rows(i - 1)(j)
                    End If

                Next
            Next
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function IntegerToExcelColumnCode(ByVal columnIndex As Integer) As String
        ' Taking a log will provide a rough estimate of the power that 
        ' we should start with. Because Excel's column
        ' numbering system has no representation for 0, the natural log 
        ' cannot give us a precise number because the
        ' numbering system doesn't have a true "base" 
        ' (though base 26 roughly corresponds for relatively low numbers).

        Dim l_startingExponent = CInt(Math.Ceiling(Math.Log(columnIndex) / Math.Log(26)))

        Dim l_remainder = columnIndex
        Dim l_code As String = ""
        For l_exponent = l_startingExponent To 0 Step -1
            ' Z for the previous place would be equal to A in the current 
            ' place except that there are no zeroes, so A in
            ' the current place is only valid if the value being represented 
            ' is greater than the value represented by A
            ' in the current place (otherwise, use Z in the next place). 
            ' The only exception is the value 1 as there is
            ' no "tenth's" (or rather, "twenty-sixth's") 
            ' place in this numbering system.
            If l_exponent = 0 OrElse l_remainder Mod Math.Pow(26, l_exponent) > 0 Then
                Dim l_placeValue = Math.Floor(l_remainder / Math.Pow(26, l_exponent))

                ' This should only ever happen as a result of guessing 
                ' the starting exponent too high. As zeroes are
                ' never valid, they should never occur.
                If l_placeValue > 0 Then
                    l_remainder -= Math.Pow(26, l_exponent) * l_placeValue
                    l_code &= Chr(&H40 + l_placeValue)
                End If
            ElseIf l_remainder > Math.Pow(26, l_exponent) Then
                Dim l_placeValue = Math.Floor((l_remainder - _
                Math.Pow(26, l_exponent)) / Math.Pow(26, l_exponent))

                ' This should only ever happen as a result of guessing 
                ' the starting exponent too high. As zeroes are
                ' never valid, they should never occur.
                If l_placeValue > 0 Then
                    l_remainder -= Math.Pow(26, l_exponent) * l_placeValue
                    l_code &= Chr(&H40 + l_placeValue)
                End If
            End If
        Next

        Return l_code
    End Function

End Class
