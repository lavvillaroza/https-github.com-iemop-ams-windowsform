
Option Explicit On
Option Strict On
Imports System.Data
Imports System.Data.OleDb
Imports AccountsManagementObjects
Imports AccountsManagementLogic
Imports System.IO

Public Class ImporterUtility


#Region "Single Instance Code"
    ' <summary>
    ' This variable stores the reference of the single instance
    ' </summary>
    ' <remarks></remarks>
    Private Shared m_Instance As ImporterUtility = Nothing

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
    Public Shared Function GetInstance() As ImporterUtility
        If m_Instance Is Nothing Then
            m_Instance = New ImporterUtility()
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
        Me._Provider = "Microsoft.Jet.OLEDB.4.0" '"Microsoft.ACE.OLEDB.12.0" 
        Me._ExtendedProperties = ControlChars.Quote & "Text;HDR=Yes;FMT=Delimeted;IMEX=1;" & ControlChars.Quote 'ControlChars.Quote & "Excel 12.0;HDR=YES;" & ControlChars.Quote 
        Me._WESMBillCharges = New List(Of ChargeId)
        Me._WBillHelper = WESMBillHelper.GetInstance()
        Me._BFactory = BusinessFactory.GetInstance()
    End Sub
#End Region

#Region "ConnectionString"
    Private _ConnectionString As String

    ' <summary>
    ' gets or sets the database connection string
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public Property ConnectionString() As String
        Get
            Return _ConnectionString
        End Get
        Set(ByVal value As String)
            Me._ConnectionString = value
        End Set
    End Property
#End Region

#Region "Provider"
    Private _Provider As String
    Public Property Provider() As String
        Get
            Return _Provider
        End Get
        Set(ByVal value As String)
            _Provider = value
        End Set
    End Property
#End Region

#Region "ExtendedProperties"
    Private _ExtendedProperties As String
    Public Property ExtendedProperties() As String
        Get
            Return _ExtendedProperties
        End Get
        Set(ByVal value As String)
            _ExtendedProperties = value
        End Set
    End Property
#End Region

#Region "WESMBillCharges"
    Private _WESMBillCharges As List(Of ChargeId)
    ' <summary>
    ' Gets or sets the WESM Bill Charges.
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public Property WESMBillCharges() As List(Of ChargeId)
        Get
            Return _WESMBillCharges
        End Get
        Set(ByVal value As List(Of ChargeId))
            _WESMBillCharges = value
        End Set
    End Property

#End Region

#Region "Total Energy Withholding Tax for BIR"
    Private _TotalEWT As Decimal
    Public ReadOnly Property TotalEWT() As Decimal
        Get
            Return _TotalEWT
        End Get
    End Property
#End Region

#Region "Import WESM Bill"
    Public Function ImportWESMBill(ByVal FileBillingPd As Integer, ByVal FileSTLRun As String, ByVal PathFolder As String, _
                                   ByVal FileName As String) As List(Of WESMBill)
        'Dim FileConnectionString As String
        'Dim myData As OleDbDataAdapter
        Dim ds As New DataSet
        Dim listFinalWESMBill As New List(Of WESMBill)

        Try
            PathFolder = PathFolder & "\"
            'FileConnectionString = "Provider = " & Me.Provider & ";Data Source = " & PathFolder & "; Extended Properties = " & Me.ExtendedProperties
            'myData = New OleDbDataAdapter("SELECT * FROM " & FileName, FileConnectionString)
            'myData.TableMappings.Add("dt", "dS")
            'myData.Fill(ds)


            'Dim listWESMBill = Me.ImportWESMBill(ds.Tables(0), FileBillingPd, FileSTLRun)
            Dim listWESMBill = Me.ImportWESMBill(PathFolder, FileName)

            'Check if there is many due date in a file
            Dim listDueDate = (From x In listWESMBill Select x.DueDate Distinct).ToList()
            If listDueDate.Count > 1 Then
                Throw New ApplicationException("There must be only one Due Date in Invoice File")
            End If

            'Check if there is many due date in a file
            Dim listInvoiceDate = (From x In listWESMBill Select x.InvoiceDate Distinct).ToList()
            If listInvoiceDate.Count > 1 Then
                Throw New ApplicationException("There must be only one Invoice Date in Invoice File")
            End If

            Dim tmpList = From x In listWESMBill Group x By _
                          x.IDNumber, x.RegistrationID, x.ChargeType _
                          Into _
                          Amount = Sum(x.Amount) _
                          Select New With {.BillingPeriod = FileBillingPd, .IDNumber = IDNumber, _
                                           .RegistrationID = RegistrationID, _
                                           .Amount = Amount, .ChargeType = ChargeType, _
                                           .SettlementRun = FileSTLRun}

            For Each item In tmpList
                Dim selectedItem = item

                Dim itemWESMBill = (From x In listWESMBill _
                                    Where x.IDNumber = selectedItem.IDNumber And _
                                    x.RegistrationID = selectedItem.RegistrationID And _
                                    x.ChargeType = selectedItem.ChargeType _
                                    Select x).First()

                Dim itemNewWESMBill As New WESMBill
                With itemNewWESMBill
                    .BillingPeriod = selectedItem.BillingPeriod
                    .IDNumber = selectedItem.IDNumber
                    .RegistrationID = selectedItem.RegistrationID
                    .ChargeType = selectedItem.ChargeType
                    .Amount = selectedItem.Amount
                    .SettlementRun = selectedItem.SettlementRun
                    .ForTheAccountOf = itemWESMBill.ForTheAccountOf
                    .FullName = itemWESMBill.FullName
                    .InvoiceDate = itemWESMBill.InvoiceDate
                    .DueDate = itemWESMBill.DueDate
                    .MarketFeesRate = itemWESMBill.MarketFeesRate
                    .Remarks = itemWESMBill.Remarks
                End With

                listFinalWESMBill.Add(itemNewWESMBill)
                listFinalWESMBill.TrimExcess()
            Next

        Catch ex1 As ArgumentException
            Throw New ApplicationException("Invalid date for Invoice Date or Due Date")
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return listFinalWESMBill
    End Function

    Public Function ImportWESMBill(ByVal dt As DataTable, ByVal FileBillingPd As Integer, ByVal FileSTLRun As String) As List(Of WESMBill)
        Dim remarksValue As String = ""
        WBillHelper.ConnectionString = Me.ConnectionString

        Dim ChargeCodes = WBillHelper.GetChargeIDCodes()
        Dim results As New List(Of WESMBill)
        Dim index As Integer = 1
        Try
            For Each i As DataRow In dt.Rows
                index += 1
                Dim row As DataRow = i
                Dim item As New WESMBill()
                Dim chargeid As New ChargeId

                If IsDBNull((row("Amount"))) Then
                    Throw New ApplicationException("Error in row " & index & "." & "Invalid Amount found in File")
                Else
                    If CDec(row("Amount")) = 0 Then ' Disregard zero amount line item
                        Continue For
                    End If
                End If

                If IsDBNull(row("STL_ID")) Then
                    Throw New ApplicationException("Error in row " & index & "." & "Invalid STL_ID found in File")
                End If
                item.IDNumber = CStr(row("STL_ID"))

                If IsDBNull(row("REG_ID")) Then
                    Throw New ApplicationException("Error in row " & index & "." & "Invalid REG_ID found in File")
                End If
                item.RegistrationID = CStr(row("REG_ID"))

                If IsDBNull(row("For_Account_Of")) Then
                    Throw New ApplicationException("Error in row " & index & "." & "Invalid For Account Of found in File")
                End If
                item.ForTheAccountOf = CStr(row("For_Account_Of"))

                If IsDBNull(row("Full_Name")) Then
                    Throw New ApplicationException("Error in row " & index & "." & "Invalid Full Name found in File")
                End If
                item.FullName = CStr(row("Full_Name"))

                If IsDBNull(row("INVOICE_DATE")) Then
                    Throw New ApplicationException("Error in row " & index & "." & "Invalid Invoice Date found in File")
                End If
                item.InvoiceDate = CDate(row("INVOICE_DATE"))

                item.Amount = If(Not IsDBNull(row("Amount")), Math.Round(CDec(row("Amount")), 2), 0)

                If IsDBNull(row("Due_Date")) Then
                    Throw New ApplicationException("Error in row " & index & "." & "Invalid Due Date found in File")
                End If
                item.DueDate = CDate(row("Due_Date"))

                If Not IsDBNull(row("Quantity")) And Not IsNumeric(row("Quantity")) Then
                    Throw New ApplicationException("Error in row " & index & "." & "Invalid Quantity found in File")
                End If

                If Not IsDBNull((row("MF_Rate"))) Then
                    If Not IsNumeric(row("MF_Rate")) Then
                        Throw New ApplicationException("Error in row " & index & "." & "Invalid MF Rate")
                    End If
                End If
                item.MarketFeesRate = If(Not IsDBNull(row("MF_Rate")), CDec(row("MF_Rate")), 0)

                item.Remarks = If(Not IsDBNull(row("Remarks")), CStr(row("Remarks")), "")

                If row("Charge_Id").ToString.Trim.Length = 0 Then
                    Throw New ApplicationException("Error in row " & index & "." & "No define charge id")
                End If

                Try
                    chargeid = (From x In ChargeCodes Where x.ChargeId = row("CHARGE_ID").ToString()).First()
                    item.ChargeType = chargeid.cIDType
                Catch ex As Exception
                    Throw New ApplicationException("Error in row " & index & "." & "Could not map " & row("CHARGE_ID").ToString() & " in AM_CHARGE_ID_LIB table")
                End Try

                results.Add(item)
            Next

        Catch ex1 As InvalidCastException
            Throw New ApplicationException("Error in row " & index & " --- " & ex1.Message)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return results
    End Function

    Public Function ImportCSVScript(filepath As String, filename As String) As List(Of String)
        Dim results As New List(Of String)
        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(filepath & "\" & filename)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    Dim currentField As String
                    For Each currentField In currentRow
                        results.Add(currentField)
                    Next
                Catch ex As Microsoft.VisualBasic.
                            FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message &
                    "is not valid and will be skipped.")
                End Try
            End While
        End Using
        results.TrimExcess()
        Return results
    End Function

    Public Function ImportCRSSMappingID(filepath As String, filename As String) As List(Of WESMInvoiceCRSSMappping)
        Dim results As New List(Of WESMInvoiceCRSSMappping)
        Dim item As WESMInvoiceCRSSMappping
        Dim dicItem As New Dictionary(Of String, String)

        Using MyReader As New Microsoft.VisualBasic.
                       FileIO.TextFieldParser(filepath & "\" & filename)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            Dim currentRow As String()
            Dim indexRow As Integer, indexColumn As Integer
            While Not MyReader.EndOfData
                item = New WESMInvoiceCRSSMappping
                indexColumn = 0
                'Read the current row
                currentRow = MyReader.ReadFields()
                Try
                    Dim currentField As String
                    For Each currentField In currentRow
                        Select Case indexColumn
                            Case 0
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow.ToString() & "." & "Invalid ID_NUMBER found in File")
                                    End If
                                    item.IDNumber = CStr(currentField)
                                Else
                                    If currentField.Trim().ToUpper() <> "ID_NUMBER" Then
                                        Throw New ApplicationException("Invalid row header for ID_NUMBER")
                                    End If
                                End If
                            Case 1
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow.ToString() & "." & "Invalid REG_ID found in File")
                                    End If
                                    item.RegIDNumber = CStr(currentField)
                                Else
                                    If currentField.Trim().ToUpper() <> "REG_ID" Then
                                        Throw New ApplicationException("Invalid row header for REG_ID")
                                    End If
                                End If

                            Case 2
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow.ToString() & "." & "Invalid NEW_REG_ID found in File")
                                    End If
                                    item.NewRegIDNumber = CStr(currentField)
                                Else
                                    If currentField.Trim().ToUpper() <> "NEW_REG_ID" Then
                                        Throw New ApplicationException("Invalid row header for NEW_REG_ID")
                                    End If
                                End If
                            Case 3
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow.ToString() & "." & "Invalid REMARKS found in File")
                                    End If
                                    item.Remarks = CStr(currentField)
                                Else
                                    If currentField.Trim().ToUpper() <> "REMARKS" Then
                                        Throw New ApplicationException("Invalid row header for REMARKS")
                                    End If
                                End If
                        End Select
                        indexColumn += 1
                    Next

                    'Add the WESM Bil
                    If indexRow <> 0 Then
                        Dim itemKey As String = item.IDNumber & "|" & item.RegIDNumber
                        If Not dicItem.ContainsKey(itemKey) Then
                            dicItem.Add(itemKey, item.NewRegIDNumber)
                        Else
                            Throw New Exception("Duplicate found! IDNumber: " & item.IDNumber & " And RegIDNumber: " & item.RegIDNumber)
                        End If
                        results.Add(item)
                    End If

                    'Increment the Row Index
                    indexRow += 1
                Catch ex As Microsoft.VisualBasic.
                            FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message &
                    "is not valid and will be skipped.")
                Catch ex1 As Exception
                    Throw New ApplicationException(ex1.Message)
                End Try
            End While
        End Using
        results.TrimExcess()
        Return results
    End Function
    Public Function ImportWESMBill(filePath As String, fileName As String) As List(Of WESMBill)
        Dim results As New List(Of WESMBill)
        Dim chargeid As ChargeId
        Dim item As WESMBill

        Dim ChargeCodes = WBillHelper.GetChargeIDCodes()

        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(filePath & fileName)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")


            Dim currentRow As String()
            Dim indexRow As Integer, indexColumn As Integer
            Me._TotalEWT = 0D
            While Not MyReader.EndOfData
                item = New WESMBill
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
                                    item.IDNumber = CStr(currentField)
                                Else
                                    If currentField.Trim().ToUpper() <> "STL_ID" Then
                                        Throw New ApplicationException("Invalid row header for STL_ID")
                                    End If
                                End If
                            Case 1
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow.ToString() & "." & "Invalid REG_ID found in File")
                                    End If
                                    item.RegistrationID = CStr(currentField)
                                Else
                                    If currentField.Trim().ToUpper() <> "REG_ID" Then
                                        Throw New ApplicationException("Invalid row header for REG_ID")
                                    End If
                                End If

                            Case 4
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow.ToString() & "." & "Invalid INVOICE_DATE found in File")
                                    End If
                                    item.InvoiceDate = CDate(currentField)
                                Else
                                    If currentField.Trim().ToUpper() <> "INVOICE_DATE" Then
                                        Throw New ApplicationException("Invalid row header for INVOICE_DATE")
                                    End If
                                End If

                            Case 5
                                If indexRow <> 0 Then
                                    If Not IsDBNull(currentField) Then
                                        If currentField.Trim.Length <> 0 And Not IsNumeric(currentField) Then
                                            Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid Quantity found in File")
                                        End If
                                    End If
                                Else
                                    If currentField.Trim().ToUpper() <> "QUANTITY" Then
                                        Throw New ApplicationException("Invalid row header QUANTITY")
                                    End If
                                End If

                            Case 6
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid Amount found in File")
                                    End If

                                    If Not IsNumeric(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid Amount found in File")
                                    End If
                                    item.Amount = CDec(currentField)
                                Else
                                    If currentField.Trim().ToUpper() <> "AMOUNT" Then
                                        Throw New ApplicationException("Invalid row header for AMOUNT")
                                    End If
                                End If

                            Case 7
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid CHARGE_ID found in File")
                                    End If
                                    Try
                                        chargeIdLib = currentField.ToUpper
                                        If chargeIdLib = "WT" Then
                                            Me._TotalEWT += item.Amount
                                        End If
                                        chargeid = (From x In ChargeCodes Where Replace(x.ChargeId, " ", "") = Replace(currentField.ToUpper, " ", "")).First()
                                        item.ChargeType = chargeid.cIDType
                                    Catch ex As Exception
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Could not map " & currentField & " in AM_CHARGE_ID_LIB table")
                                    End Try
                                Else
                                    If currentField.Trim().ToUpper() <> "CHARGE_ID" Then
                                        Throw New ApplicationException("Invalid row header for CHARGE_ID")
                                    End If
                                End If

                            Case 8
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid DUE_DATE found in File")
                                    End If

                                    item.DueDate = CDate(currentField)
                                Else
                                    If currentField.Trim().ToUpper() <> "DUE_DATE" Then
                                        Throw New ApplicationException("Invalid row header for DUE_DATE")
                                    End If
                                End If

                            Case 9
                                If indexRow <> 0 Then
                                    If Not IsDBNull(currentField) And Len(currentField) <> 0 Then
                                        If Not IsNumeric(currentField) Then
                                            Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid MF Rate")
                                        End If
                                    End If
                                    item.MarketFeesRate = If(Not IsDBNull(currentField) And Len(currentField) <> 0, CDec(currentField), 0)
                                Else
                                    If currentField.Trim().ToUpper() <> "MF_RATE" Then
                                        Throw New ApplicationException("Invalid row header for MF_RATE")
                                    ElseIf currentField.Trim() Is Nothing Or Len(currentField.Trim()) = 0 Then
                                        item.MarketFeesRate = 0
                                    End If
                                End If

                            Case 10
                                If indexRow <> 0 Then
                                    item.Remarks = If(Not IsDBNull(currentField) And Len(currentField) <> 0, CStr(currentField), "")
                                Else
                                    If currentField.Trim().ToUpper() <> "REMARKS" Then
                                        Throw New ApplicationException("Invalid row header for REMARKS")
                                    End If
                                End If
                        End Select
                        indexColumn += 1
                    Next

                    ''Add the WESM Bil
                    'If indexRow <> 0 And chargeIdLib <> "WT" Then
                    '    results.Add(item)
                    'End If

                    'Add the WESM Bil
                    If indexRow <> 0 Then
                        results.Add(item)
                    End If

                    'Increment the Row Index
                    indexRow += 1

                Catch ex1 As Microsoft.VisualBasic.
                            FileIO.MalformedLineException
                Catch ex As Exception
                    Throw New ApplicationException(ex.Message)
                End Try

            End While
        End Using

        Return results
    End Function
#End Region

#Region "Import WESM Invoice"
    Public Function ImportWESMInvoice(ByVal FileBillingPd As Integer, ByVal FileSTLRun As String, ByVal FileType As EnumFileType, _
                                      ByVal PathFolder As String, ByVal FileName As String) As List(Of WESMInvoice)
        'Dim FileConnectionString As String
        'Dim myData As OleDbDataAdapter
        Dim listWESMBill As New List(Of WESMInvoice)
        Dim ds As New DataSet

        Try
            PathFolder = PathFolder & "\"
            'FileConnectionString = "Provider = " & Me.Provider & ";Data Source = " & PathFolder & "; Extended Properties = " & Me.ExtendedProperties
            'myData = New OleDbDataAdapter("SELECT * FROM " & FileName, FileConnectionString)
            'myData.TableMappings.Add("dT", "dS")
            'myData.Fill(ds)
            'listWESMBill = Me.ImportWESMInvoice(ds.Tables(0), FileBillingPd, FileSTLRun, FileType)
            listWESMBill = Me.ImportWESMInvoice(PathFolder, FileName, FileBillingPd, FileSTLRun, FileType)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return listWESMBill
    End Function

    Public Function ImportWESMInvoice(ByVal dt As DataTable, ByVal billingperiod As Integer, ByVal settlementrun As String, _
                                      ByVal filetype As EnumFileType) As List(Of WESMInvoice)

        WBillHelper.ConnectionString = Me.ConnectionString
        Dim ChargeCodes = WBillHelper.GetChargeIDCodes()
        Dim results As New List(Of WESMInvoice)
        Dim index As Integer = 1
        Try
            For Each i As DataRow In dt.Rows
                index += 1
                Dim row As DataRow = i
                Dim item As New WESMInvoice
                Dim chargeid As New ChargeId
                With item
                    .FileType = filetype
                    .BillingPeriod = billingperiod
                    .SettlementRun = settlementrun
                    .IDNumber = CStr(row("STL_ID"))
                    .RegistrationID = CStr(row("REG_ID"))
                    .ForTheAccountOf = CStr(row("For_Account_Of"))
                    .FullName = CStr(row("Full_Name"))
                    .InvoiceDate = CDate(row("INVOICE_DATE"))
                    .Amount = If(Not IsDBNull(row("Amount")), Math.Round(CDec(row("Amount")), 2), 0)
                    .DueDate = CDate(row("DUE_DATE"))
                    .Quantity = If(Not IsDBNull(row("Quantity")), Math.Round(CDec(row("Quantity")), 3), 0)
                    .ChargeID = CStr(row("Charge_Id"))
                    .MarketFeesRate = If(Not IsDBNull(row("MF_Rate")), CDec(row("MF_Rate")), 0)
                    .Remarks = If(Not IsDBNull(row("Remarks")), CStr(row("Remarks")), "")

                    If settlementrun.Length > 3 Then
                        Throw New ApplicationException("Maximum character for Settlement Run is 3.")
                    End If

                    If .BillingPeriod.ToString().Length > 4 Then
                        Throw New ApplicationException("Maximum digit for Billing Period is 4.")
                    End If

                    If .ChargeID.Length > 13 Then
                        Throw New ApplicationException("Error in row " & index & " --- " & vbCrLf & _
                                                       "Maximum character for Quantity column is 13.")
                    End If

                    If Not Me.BFactory.CheckPrecisionAndScale(15, 2, .Amount.ToString()) Then
                        Throw New ApplicationException("Error in row " & index & " --- " & vbCrLf & _
                                                       "Please provide a value for Amount column that has a maximum of 15 digits and 2 decimal places.")
                    End If
                End With

                results.Add(item)
            Next
            results.TrimExcess()

        Catch ex1 As InvalidCastException
            Throw New ApplicationException("Error in row " & index & " --- " & ex1.Message)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return results
    End Function

    Public Function ImportWESMInvoice(filePath As String, fileName As String, ByVal billingperiod As Integer, ByVal settlementrun As String, _
                                      ByVal filetype As EnumFileType) As List(Of WESMInvoice)
        Dim results As New List(Of WESMInvoice)
        Dim chargeid As ChargeId
        Dim item As WESMInvoice

        Dim ChargeCodes = WBillHelper.GetChargeIDCodes()

        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(filePath & "\" & fileName)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")


            Dim currentRow As String()
            Dim indexRow As Integer, indexColumn As Integer

            While Not MyReader.EndOfData
                'Read the current row
                currentRow = MyReader.ReadFields()

                If indexRow = 0 Then
                    indexRow = 1
                    Continue While
                End If

                item = New WESMInvoice
                indexColumn = 0
                Try
                    Dim currentField As String
                    For Each currentField In currentRow
                        Select Case indexColumn
                            Case 0
                                item.IDNumber = CStr(currentField)

                            Case 1
                                item.RegistrationID = CStr(currentField)

                            Case 4
                                item.InvoiceDate = CDate(currentField)

                            Case 5
                                item.Quantity = If(Not IsDBNull(currentField) And Not currentField.Trim.Length = 0, Math.Round(CDec(currentField), 3), 0)

                            Case 6
                                item.Amount = If(Not IsDBNull(currentField), Math.Round(CDec(currentField), 2), 0)

                            Case 7
                                chargeid = (From x In ChargeCodes Where Replace(x.ChargeId, " ", "") = Replace(currentField.ToUpper, " ", "")).First()
                                item.ChargeID = chargeid.ChargeId

                            Case 8
                                item.DueDate = CDate(currentField)

                            Case 9
                                item.MarketFeesRate = If(Not IsDBNull(currentField) And Not currentField.Trim.Length = 0, CDec(currentField), 0)

                            Case 10
                                item.Remarks = If(Not IsDBNull(currentField), CStr(currentField), "")
                        End Select

                        indexColumn += 1
                    Next

                    'Add the WESM Bil

                    With item
                        .FileType = filetype
                        .BillingPeriod = billingperiod
                        .SettlementRun = settlementrun

                        If settlementrun.Length > 3 Then
                            Throw New ApplicationException("Maximum character for Settlement Run is 3.")
                        End If

                        If .BillingPeriod.ToString().Length > 4 Then
                            Throw New ApplicationException("Maximum digit for Billing Period is 4.")
                        End If

                        If Not Me.BFactory.CheckPrecisionAndScale(15, 2, .Amount.ToString()) Then
                            Throw New ApplicationException("Error in row " & indexRow & " --- " & vbCrLf & _
                                                           "Please provide a value for Amount column that has a maximum of 15 digits and 2 decimal places.")
                        End If
                    End With

                    results.Add(item)

                    'Increment the Row Index
                    indexRow += 1
                Catch ex1 As Microsoft.VisualBasic.
                            FileIO.MalformedLineException
                Catch ex As Exception
                    Throw New ApplicationException(ex.Message)
                End Try
            End While
        End Using

        Return results
    End Function
#End Region

#Region "Import NSS Summary"
    Public Function ImportNSSSummary(ByVal AllocationDate As Date, ByVal Period As EnumQuarterlyPeriod, ByVal YearName As Integer, _
                                     ByVal PathFolder As String, ByVal FileName As String) As List(Of NetSettlementSurplusSummary)
        Dim FileConnectionString As String
        Dim myData As OleDbDataAdapter
        Dim listNSSSummary As New List(Of NetSettlementSurplusSummary)
        Dim ds As New DataSet

        Try
            PathFolder = PathFolder & "\"
            FileConnectionString = "Provider = " & Me.Provider & ";Data Source = " & PathFolder & "; Extended Properties = " & Me.ExtendedProperties
            myData = New OleDbDataAdapter("SELECT * FROM " & FileName, FileConnectionString)
            myData.TableMappings.Add("dt", "dS")

            myData.Fill(ds)
            listNSSSummary = Me.ImportNSSSummary(AllocationDate, Period, YearName, ds.Tables(0))
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return listNSSSummary
    End Function

    Public Function ImportNSSSummary(ByVal AllocationDate As Date, ByVal Period As EnumQuarterlyPeriod, ByVal YearName As Integer, ByVal dt As DataTable) As List(Of NetSettlementSurplusSummary)
        Dim results As New List(Of NetSettlementSurplusSummary)
        Dim index As Integer = 1

        WBillHelper.ConnectionString = Me.ConnectionString
        Dim listParticipants = WBillHelper.GetAMParticipants()

        Try
            For Each i As DataRow In dt.Rows
                index += 1
                Dim row As DataRow = i
                Dim item As New NetSettlementSurplusSummary()
                Dim tempParticipant As New AMParticipants()

                If IsDBNull(row("PARTICIPANT_ID")) Then
                    Throw New ApplicationException("Error in row " & index & " --- " & "Invalid Participant ID found in File")
                End If

                If IsDBNull(row("ID_NUMBER")) Then
                    Throw New ApplicationException("Error in row " & index & " --- " & "Invalid ID Number found in File")
                End If

                If row("ID_NUMBER").ToString().Contains(".") Then
                    Throw New ApplicationException("Error in row " & index & " --- " & "ID_NUMBER " & row("ID_NUMBER").ToString() & " must be whole number")
                Else
                    tempParticipant.IDNumber = CStr(row("ID_NUMBER"))
                End If
                item.IDNumber = New AMParticipants(CStr(row("ID_NUMBER")), CStr(row("PARTICIPANT_ID")))

                Dim listSelectedParticipants = (From x In listParticipants _
                                                Where x.IDNumber = item.IDNumber.IDNumber _
                                                Select x)

                If listSelectedParticipants.Count = 0 Then
                    Throw New ApplicationException("Error in row " & index & " --- " & "Invalid ID Number " & item.IDNumber.IDNumber.ToString())
                Else
                    If listSelectedParticipants.First.ParticipantID.ToUpper() <> item.IDNumber.ParticipantID.ToUpper() Then
                        Throw New ApplicationException("Error in row " & index & " --- " & "ID Number " & item.IDNumber.IDNumber.ToString() & _
                                                       " does not match to " & item.IDNumber.ParticipantID & " into master file.")
                    End If
                End If

                If IsDBNull((row("TOTAL_NSS_INTEREST"))) Then
                    Throw New ApplicationException("Error in row " & index & " --- " & "Invalid Total NSS Interest found in File")
                End If

                item.TotalNSSInterest = If(Not IsDBNull(row("TOTAL_NSS_INTEREST")), CDec(row("TOTAL_NSS_INTEREST")), 0)

                If item.TotalNSSInterest < 0 Then
                    Throw New ApplicationException("Error in row " & index & " --- " & "Negative Total NSS Interest is not allowed!")
                End If

                If IsDBNull((row("TOTAL_NSS_INTEREST_NET_WTAX"))) Then
                    Throw New ApplicationException("Error in row " & index & " --- " & "Invalid Total NSS Interest Net WTax found in File")
                End If
                item.TotalNSSInterestNetWTax = CDec(row("TOTAL_NSS_INTEREST_NET_WTAX"))

                If item.TotalNSSInterestNetWTax < 0 Then
                    Throw New ApplicationException("Error in row " & index & " --- " & "Negative Total NSS Interest Net WTAX is not allowed!")
                End If

                If IsDBNull((row("TOTAL_STL_INTEREST"))) Then
                    Throw New ApplicationException("Error in row " & index & " --- " & "Invalid Total STL Interest found in File")
                End If
                item.TotalSTLInterest = CDec(row("TOTAL_STL_INTEREST"))

                If item.TotalSTLInterest < 0 Then
                    Throw New ApplicationException("Error in row " & index & " --- " & "Negative Total Settlement Interest is not allowed!")
                End If

                If IsDBNull((row("TOTAL_STL_INTEREST_NET_WTAX"))) Then
                    Throw New ApplicationException("Error in row " & index & " --- " & "Invalid Total STL Interest Net WTax found in File")
                End If
                item.TotalSTLInterestNetWTax = CDec(row("TOTAL_STL_INTEREST_NET_WTAX"))

                If item.TotalSTLInterestNetWTax < 0 Then
                    Throw New ApplicationException("Error in row " & index & " --- " & "Negative Total Settlement Interest Net WTAX is not allowed!")
                End If

                If IsDBNull((row("TOTAL"))) Then
                    Throw New ApplicationException("Error in row " & index & " --- " & "Invalid Total found in File")
                End If
                item.Total = CDec(row("TOTAL"))

                If item.Total < 0 Then
                    Throw New ApplicationException("Error in row " & index & " --- " & "Negative Total is not allowed!")
                End If

                item.AllocationDate = AllocationDate
                item.QuarterlyPeriod = Period
                item.YearPeriod = YearName

                results.Add(item)
            Next

        Catch ex1 As InvalidCastException
            Throw New ApplicationException("Error in row " & index & " --- " & ex1.Message)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return results
    End Function

#End Region

#Region "GetNSSRA"
    Public Function GetNSSRA(ByVal FileBillingPd As Integer, ByVal FileSTLRun As String, _
                             ByVal PathFolder As String, ByVal FileName As String) As Decimal
        Dim result As Decimal = 0
        Dim dt As DataTable = New DataTable        
        Try            
            dt = csvToDatatable_2(PathFolder & FileName, ",")
            For Each i As DataRow In dt.Rows
                Dim row As DataRow = i
                If CStr(row("CHARGE_ID")) = AMModule.NSSRACode Then
                    result += CDec(row("Amount"))
                End If
            Next

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function

    Public Function csvToDatatable_2(ByVal filename As String, ByVal separator As String) As DataTable
        Dim dt As New System.Data.DataTable
        Dim firstLine As Boolean = True
        If IO.File.Exists(filename) Then
            Using sr As New StreamReader(filename)
                While Not sr.EndOfStream
                    If firstLine Then
                        firstLine = False
                        Dim cols = sr.ReadLine.Split(CChar(separator))
                        For Each col In cols
                            dt.Columns.Add(New DataColumn(col, GetType(String)))
                        Next
                    Else
                        Dim data() As String = sr.ReadLine.Split(CChar(separator))
                        dt.Rows.Add(data.ToArray)
                    End If
                End While
            End Using
        End If
        Return dt
    End Function
#End Region

#Region "Import WESM Bill Sales and Purchased"
    Public Function ImportWESMBillSalesAndPurchased(ByVal FileBillingPd As Integer, ByVal FileSTLRun As String, _
                                                    ByVal PathFolder As String, ByVal FileName As String, _
                                                    ByVal listWESMInvoice As List(Of WESMInvoice), _
                                                    ByVal listParticipants As List(Of AMParticipants), _
                                                    ByVal listWBChangeID As List(Of WESMBillSummaryChangeParentId)) As List(Of WESMBillSalesAndPurchased)

        Dim listWESMBillSalesAndPurchases As New List(Of WESMBillSalesAndPurchased)
        Dim ds As New DataSet

        Try
            PathFolder = PathFolder & "\"
            listWESMBillSalesAndPurchases = Me.ImportWESMBillSalesAndPurchased(PathFolder, FileName, FileBillingPd, FileSTLRun)

            Dim listGMR = From x In listWESMBillSalesAndPurchases _
                          Select x.GMR Distinct

            If listGMR.Count <> 1 Then
                Throw New ApplicationException("The file contains different GMR values")
            End If

            Dim listFinalSP = From x In listWESMBillSalesAndPurchases Join y In listParticipants _
                            On x.IDNumber.IDNumber Equals y.IDNumber _
                            Select x, y.GenLoad

            For Each item In listFinalSP
                Dim seletectedItem = item

                'Check if the WESM Bill of the selected participant does exist
                Try
                    item.x.InvoiceNumber = (From x In listWESMInvoice _
                                            Where x.IDNumber = seletectedItem.x.IDNumber.IDNumber _
                                            And x.RegistrationID = seletectedItem.x.RegistrationID _
                                            Select x.InvoiceNumber).FirstOrDefault

                    If item.x.InvoiceNumber Is Nothing Then
                        Dim getItemChangeID = (From x In listWBChangeID Where x.BillingPeriod = FileBillingPd _
                                    And x.ParentParticipants.IDNumber = item.x.IDNumber.IDNumber _
                                    And x.ChildParticipants.IDNumber = item.x.RegistrationID Select x).FirstOrDefault

                        item.x.InvoiceNumber = (From x In listWESMInvoice _
                                          Where x.IDNumber = If(getItemChangeID.NewParentParticipants.IDNumber.Contains(AMModule.FITParticipantCode.ToString), getItemChangeID.ParentParticipants.IDNumber, getItemChangeID.NewParentParticipants.IDNumber) _
                                          And x.RegistrationID = getItemChangeID.ChildParticipants.IDNumber _
                                          Select x.InvoiceNumber).FirstOrDefault

                    End If

                Catch ex As Exception
                    Throw New ApplicationException("Cannot find the WESM Bill of " & seletectedItem.x.IDNumber.IDNumber & " " & _
                                                   "with Registration ID of " & seletectedItem.x.RegistrationID)
                End Try

                'Update the NSSRA 
                'item.x.NSSRA = (From x In listWESMInvoice _
                '                Where x.IDNumber = seletectedItem.x.IDNumber.IDNumber _
                '                And x.RegistrationID = seletectedItem.x.RegistrationID _
                '                And x.ChargeID = AMModule.NSSRACode _
                '                Select x.Amount).Sum()

                'Update the Invoice Number
                'item.x.InvoiceNumber = (From x In listWESMInvoice _
                '                        Where x.IDNumber = seletectedItem.x.IDNumber.IDNumber _
                '                        And x.RegistrationID = seletectedItem.x.RegistrationID _
                '                        Select x.InvoiceNumber).First

                'Not applicable since all columns have already values. In old process, load has no values
                ''If all amount is zero
                'If item.x.NetSettlementAmount - item.x.NSSRA = 0 And item.x.ZeroRatedSales = 0 _
                '   And item.x.VatablePurchases = 0 And item.x.VATonSales = 0 And item.x.VATonPurchases = 0 And item.x.GMR = 0 Then

                '    Dim itemSalesOrPurchase As Decimal = 0
                '    Dim itemVAT As Decimal = 0

                '    itemSalesOrPurchase = (From x In listWESMInvoice _
                '                           Where x.IDNumber = seletectedItem.x.IDNumber.IDNumber _
                '                           And x.RegistrationID = seletectedItem.x.RegistrationID _
                '                           And x.ChargeID <> AMModule.VatCode And x.ChargeID <> AMModule.NSSRACode _
                '                           Select x.Amount).Sum()

                '    itemVAT = (From x In listWESMInvoice _
                '               Where x.IDNumber = seletectedItem.x.IDNumber.IDNumber _
                '               And x.RegistrationID = seletectedItem.x.RegistrationID _
                '               And x.ChargeID = AMModule.VatCode _
                '               Select x.Amount).Sum()

                '    If itemSalesOrPurchase > 0 Then
                '        item.x.VatableSales = itemSalesOrPurchase
                '        item.x.VATonSales = itemVAT
                '        item.x.VatablePurchases = 0
                '        item.x.VATonPurchases = 0
                '    Else
                '        item.x.VatableSales = 0
                '        item.x.VATonSales = 0
                '        item.x.VatablePurchases = itemSalesOrPurchase
                '        item.x.VATonPurchases = itemVAT
                '    End If

                '    item.x.TransactionType = EnumWESMBillSalesAndPurchasedTransType.Computed
                'End If

                'If seletectedItem.GenLoad = EnumGenLoad.L Then
                '    item.x.Purchases = (From x In listWESMInvoice _
                '                        Where x.IDNumber = seletectedItem.x.IDNumber.IDNumber _
                '                        And x.RegistrationID = seletectedItem.x.RegistrationID _
                '                        And x.ChargeID <> AMModule.VatCode And x.ChargeID <> AMModule.NSSRACode _
                '                        Select x.Amount).Sum()

                '    item.x.VATonPurchases = (From x In listWESMInvoice _
                '                             Where x.IDNumber = seletectedItem.x.IDNumber.IDNumber _
                '                             And x.RegistrationID = seletectedItem.x.RegistrationID _
                '                             And x.ChargeID = AMModule.VatCode _
                '                             Select x.Amount).Sum()

                '    item.x.TransactionType = EnumWESMBillSalesAndPurchasedTransType.Computed
                'End If
            Next

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return listWESMBillSalesAndPurchases
    End Function

    Public Function ImportWESMBillSalesAndPurchased(ByVal FileBillingPd As Integer, ByVal FileSTLRun As String, _
                                                    ByVal dt As DataTable) As List(Of WESMBillSalesAndPurchased)
        WBillHelper.ConnectionString = Me.ConnectionString

        Dim results As New List(Of WESMBillSalesAndPurchased)
        Dim index As Integer = 1
        Try
            For Each i As DataRow In dt.Rows
                index += 1
                Dim row As DataRow = i
                Dim item As New WESMBillSalesAndPurchased()

                item.BillingPeriod = FileBillingPd
                item.SettlementRun = FileSTLRun

                If IsDBNull(row("STL_ID")) Then
                    Throw New ApplicationException("Error in row " & index & " --- " & "Invalid STL_ID found in File")
                End If
                item.IDNumber = New AMParticipants(CStr(row("STL_ID")))

                If IsDBNull(row("REG_ID")) Then
                    Throw New ApplicationException("Error in row " & index & " --- " & "Invalid REG_ID found in File")
                End If
                item.RegistrationID = CStr(row("REG_ID"))

                If IsDBNull((row("Zero_Rated_Sales"))) Then
                    Throw New ApplicationException("Error in row " & index & " --- " & "Invalid Zero Rated Sales value found in File")
                End If
                item.ZeroRatedSales = Math.Round(CDec(row("Zero_Rated_Sales")), 2)

                If IsDBNull((row("Purchases"))) Then
                    Throw New ApplicationException("Error in row " & index & " --- " & "Invalid Purchases value found in File")
                End If
                item.VatablePurchases = Math.Round(CDec(row("Purchases")), 2)

                If IsDBNull((row("Zero_Rated_Purchases"))) Then
                    Throw New ApplicationException("Error in row " & index & " --- " & "Invalid Zero Rated Purchases value found in File")
                End If
                item.ZeroRatedPurchases = Math.Round(CDec(row("Zero_Rated_Purchases")), 2)

                If IsDBNull((row("VAT_On_Sales"))) Then
                    Throw New ApplicationException("Error in row " & index & " --- " & "Invalid VAT on Sales value found in File")
                End If
                item.VATonSales = Math.Round(CDec(row("VAT_On_Sales")), 2)

                If IsDBNull((row("VAT_On_Purchases"))) Then
                    Throw New ApplicationException("Error in row " & index & " --- " & "Invalid VAT on Purchases value found in File")
                End If
                item.VATonPurchases = Math.Round(CDec(row("VAT_On_Purchases")), 2)

                If IsDBNull((row("Withholding_VAT"))) Then
                    Throw New ApplicationException("Error in row " & index & " --- " & "Invalid Withholding VAT value found in File")
                End If
                item.WithholdingTAX = Math.Round(CDec(row("Withholding_TAX")), 2)

                If IsDBNull((row("GMR"))) Then
                    Throw New ApplicationException("Error in row " & index & " --- " & "Invalid GMR value found in File")
                End If
                item.GMR = CDec(row("GMR"))

                'Update the Transction Type
                item.TransactionType = EnumWESMBillSalesAndPurchasedTransType.Uploaded

                results.Add(item)
            Next

        Catch ex1 As InvalidCastException
            Throw New ApplicationException("Error in row " & index & " --- " & ex1.Message)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return results
    End Function

    Public Function ImportWESMBillSalesAndPurchased(filePath As String, fileName As String, ByVal FileBillingPd As Integer, _
                                                    ByVal FileSTLRun As String) As List(Of WESMBillSalesAndPurchased)
        Dim results As New List(Of WESMBillSalesAndPurchased)
        Dim item As WESMBillSalesAndPurchased

        Dim ChargeCodes = WBillHelper.GetChargeIDCodes()

        Using MyReader As New Microsoft.VisualBasic.
                        FileIO.TextFieldParser(filePath & fileName)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")


            Dim currentRow As String()
            Dim indexRow As Integer, indexColumn As Integer

            While Not MyReader.EndOfData
                item = New WESMBillSalesAndPurchased
                indexColumn = 0

                'Read the current row
                currentRow = MyReader.ReadFields()
                Try
                    Dim currentField As String
                    For Each currentField In currentRow
                        Select Case indexColumn
                            Case 0
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow.ToString() & "." & "Invalid STL_ID found in File")
                                    End If
                                    item.IDNumber = New AMParticipants(CStr(currentField))
                                Else
                                    If currentField.Trim().ToUpper() <> "STL_ID" Then
                                        Throw New ApplicationException("Invalid row header for STL_ID")
                                    End If
                                End If
                            Case 1
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow.ToString() & "." & "Invalid REG_ID found in File")
                                    End If
                                    item.RegistrationID = CStr(currentField)
                                Else
                                    If currentField.Trim().ToUpper() <> "REG_ID" Then
                                        Throw New ApplicationException("Invalid row header for REG_ID")
                                    End If
                                End If

                            Case 2
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow.ToString() & "." & "Invalid Vatable Sales found in File")
                                    End If

                                    If Not IsNumeric(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid Vatable Sales found in File")
                                    End If

                                    item.VatableSales = Math.Round(CDec(currentField), 2)
                                Else
                                    If currentField.Trim().ToUpper() <> "VATABLE_SALES" Then
                                        Throw New ApplicationException("Invalid row header for Vatable Sales")
                                    End If
                                End If

                            Case 3
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow.ToString() & "." & "Invalid Zero_Rated_Sales found in File")
                                    End If

                                    If Not IsNumeric(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid Zero_Rated_Sales found in File")
                                    End If

                                    item.ZeroRatedSales = Math.Round(CDec(currentField), 2)
                                Else
                                    If currentField.Trim().ToUpper() <> "ZERO_RATED_SALES" Then
                                        Throw New ApplicationException("Invalid row header for Zero_Rated_Sales")
                                    End If
                                End If

                            Case 4
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow.ToString() & "." & "Invalid Purchases found in File")
                                    End If

                                    If Not IsNumeric(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid Purchases found in File")
                                    End If

                                    item.VatablePurchases = Math.Round(CDec(currentField), 2)
                                Else
                                    If currentField.Trim().ToUpper() <> "VATABLE_PURCHASES" Then
                                        Throw New ApplicationException("Invalid row header for Purchases")
                                    End If
                                End If

                            Case 5
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow.ToString() & "." & "Invalid Zero Rated Purchases found in File")
                                    End If

                                    If Not IsNumeric(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid Zero Rated Purchases found in File")
                                    End If

                                    item.ZeroRatedPurchases = Math.Round(CDec(currentField), 2)
                                Else
                                    If currentField.Trim().ToUpper() <> "ZERO_RATED_PURCHASES" Then
                                        Throw New ApplicationException("Invalid row header for Zero Rated Purchases")
                                    End If
                                End If

                            Case 6
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow.ToString() & "." & "Invalid TTA found in File")
                                    End If

                                    If Not IsNumeric(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid TTA found in File")
                                    End If

                                    item.NetSettlementAmount = Math.Round(CDec(currentField), 2)
                                Else
                                    If currentField.Trim().ToUpper() <> "TTA" Then
                                        Throw New ApplicationException("Invalid row header for TTA")
                                    End If
                                End If

                            Case 7
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow.ToString() & "." & "Invalid VAT_On_Sales found in File")
                                    End If

                                    If Not IsNumeric(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid VAT_On_Sales found in File")
                                    End If

                                    item.VATonSales = Math.Round(CDec(currentField), 2)
                                Else
                                    If currentField.Trim().ToUpper() <> "VAT_ON_SALES" Then
                                        Throw New ApplicationException("Invalid row header for VAT_On_Sales")
                                    End If
                                End If

                            Case 8
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow.ToString() & "." & "Invalid VAT_On_Purchases found in File")
                                    End If

                                    If Not IsNumeric(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid VAT_On_Purchases found in File")
                                    End If

                                    item.VATonPurchases = Math.Round(CDec(currentField), 2)
                                Else
                                    If currentField.Trim().ToUpper() <> "VAT_ON_PURCHASES" Then
                                        Throw New ApplicationException("Invalid row header for VAT_On_Purchases")
                                    End If
                                End If
                            Case 9
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow.ToString() & "." & "Invalid Withholding_TAX found in File")
                                    End If

                                    If Not IsNumeric(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid Withholding_TAX found in File")
                                    End If

                                    item.WithholdingTAX = CDec(currentField)
                                Else
                                    If currentField.Trim().ToUpper() <> "WITHHOLDING_TAX" Then
                                        Throw New ApplicationException("Invalid row header for Withholding_TAX")
                                    End If
                                End If
                            Case 10
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow.ToString() & "." & "Invalid Zero_Rated_Ecozone found in File")
                                    End If

                                    If Not IsNumeric(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid Zero_Rated_Ecozone found in File")
                                    End If

                                    item.ZeroRatedEcozone = Math.Round(CDec(currentField), 2)
                                Else
                                    If currentField.Trim().ToUpper() <> "ZERO_RATED_ECOZONE" Then
                                        Throw New ApplicationException("Invalid row header for Zero_Rated_Ecozone")
                                    End If
                                End If
                            Case 11
                                If indexRow <> 0 Then
                                    If IsDBNull(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow.ToString() & "." & "Invalid GMR found in File")
                                    End If

                                    If Not IsNumeric(currentField) Then
                                        Throw New ApplicationException("Error in row " & indexRow & "." & "Invalid GMR found in File")
                                    End If
                                    item.GMR = CDec(currentField)
                                Else
                                    If currentField.Trim().ToUpper() <> "GMR" Then
                                        Throw New ApplicationException("Invalid row header for GMR")
                                    End If
                                End If
                        End Select

                        indexColumn += 1
                    Next

                    'Add the Sales and Purchased
                    If indexRow <> 0 Then
                        item.BillingPeriod = FileBillingPd
                        item.SettlementRun = FileSTLRun
                        results.Add(item)
                    End If

                    'Increment the Row Index
                    indexRow += 1

                Catch ex1 As Microsoft.VisualBasic.
                            FileIO.MalformedLineException
                Catch ex As Exception
                    Throw New ApplicationException(ex.Message)
                End Try

            End While
        End Using

        Return results
    End Function
#End Region




End Class
