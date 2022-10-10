Imports System.Text
Imports AccountsManagementObjects
Imports AccountsManagementDataAccess

Public Class ImportWESMBillFromCRSSDBHelper
    Dim ListOfErrorDic As Dictionary(Of String, List(Of String))
    Public Sub New()        
        Me._NpgDataAccess = NpgsqlDAL.GetInstance
        Me._NpgDataAccess.ConnectionString = AMModule.ConnectionStringCRSS
        Me._WBillHelper = WESMBillHelper.GetInstance
        Me._WBillHelper.ConnectionString = AMModule.ConnectionString                
        Me._newWESMBillDueDateList = Me.GetNewWESMInvoicesDueDate()
        Me._chargeID = Me._WBillHelper.GetChargeIDCodes
        Me.ListOfErrorDic = New Dictionary(Of String, List(Of String))
    End Sub

    Private _NpgDataAccess As NpgsqlDAL
    Public ReadOnly Property NpgDataAccess() As NpgsqlDAL
        Get
            Return Me._NpgDataAccess
        End Get
    End Property

#Region "WESMBillHelper"
    Private _WBillHelper As WESMBillHelper
    Public ReadOnly Property WBillHelper() As WESMBillHelper
        Get
            Return _WBillHelper
        End Get
    End Property
#End Region

#Region "Property of WESM Bill Due Date List"
    Private _newWESMBillDueDateList As New List(Of Date)
    Public Property newWESMBillDueDateList() As List(Of Date)
        Get
            Return _newWESMBillDueDateList
        End Get
        Set(ByVal value As List(Of Date))
            _newWESMBillDueDateList = value
        End Set
    End Property
#End Region

#Region "Property of WESM Invoice List"
    Private _newWESMInvoiceList As New List(Of WESMInvoice)
    Public Property newWESMInvoiceList() As List(Of WESMInvoice)
        Get
            Return _newWESMInvoiceList
        End Get
        Set(ByVal value As List(Of WESMInvoice))
            _newWESMInvoiceList = value
        End Set
    End Property
#End Region

#Region "Property of WESM Bill Sales and Purchases List"
    Private _newWESMBillSalesAndPurchaseList As New List(Of WESMBillSalesAndPurchased)
    Public Property newWESMBillSalesPurchaseList() As List(Of WESMBillSalesAndPurchased)
        Get
            Return _newWESMBillSalesAndPurchaseList
        End Get
        Set(ByVal value As List(Of WESMBillSalesAndPurchased))
            _newWESMBillSalesAndPurchaseList = value
        End Set
    End Property
#End Region

#Region "Property of WESM Bill List"
    Private _newWESMBillList As New List(Of WESMBill)
    Public Property newWESMBillList() As List(Of WESMBill)
        Get
            Return _newWESMBillList
        End Get
        Set(ByVal value As List(Of WESMBill))
            _newWESMBillList = value
        End Set
    End Property
#End Region

#Region "Property of WESM Bill List per group"
    Private _newWESMBillGroupList As New List(Of ImportWESMBillFromCRSS)
    Public Property newWESMBillGroupList() As List(Of ImportWESMBillFromCRSS)
        Get
            Return _newWESMBillGroupList
        End Get
        Set(ByVal value As List(Of ImportWESMBillFromCRSS))
            _newWESMBillGroupList = value
        End Set
    End Property
#End Region

#Region "Property of ChargeID"
    Private _chargeID As New List(Of ChargeId)
    Public Property ChargeID() As List(Of ChargeId)
        Get
            Return _chargeID
        End Get
        Set(ByVal value As List(Of ChargeId))
            _chargeID = value
        End Set
    End Property
#End Region

#Region "Get New WESM Bill Due Date"
    Public Function GetNewWESMInvoicesDueDate() As List(Of Date)
        Dim result As New List(Of Date)
        Dim report As New DataReport
        Dim SQL As String

        Try
            If AMModule.RegionType = "LV" Then
                SQL = "SELECT DISTINCT DUE_DATE FROM settlement.txn_wesm_bill_inv WHERE REGION_GROUP = 'LUZON_VISAYAS' Order by due_date desc"

            Else
                SQL = "SELECT DISTINCT DUE_DATE FROM settlement.txn_wesm_bill_inv WHERE REGION_GROUP = 'MINDANAO' Order by due_date desc"
            End If

            report = Me.NpgDataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            result = Me.GetNewWESMInvoicesDueDate(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function
    Private Function GetNewWESMInvoicesDueDate(ByVal dr As IDataReader) As List(Of Date)
        Dim result As New List(Of Date)

        Try
            While dr.Read()
                With dr
                    result.Add(CDate(.Item("DUE_DATE")))
                End With
            End While
            result.TrimExcess()

        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try

        Return result
    End Function
#End Region

#Region "Get WESM Bills"
    Public Function GetWESMBills(ByVal DueDate As Date) As List(Of WESMBill)
        Dim result As New List(Of WESMBill)
        Dim report As New DataReport
        Dim SQL As String

        Try

            If AMModule.RegionType = "LV" Then
                SQL = "SELECT * FROM settlement.vw_txn_wesm_bill_inv " & vbNewLine _
               & "WHERE DUE_DATE = TO_DATE('" & DueDate & "', 'MM/DD/YYYY') AND REGION_GROUP = 'LUZON_VISAYAS'"

            Else
                SQL = "SELECT * FROM settlement.vw_txn_wesm_bill_inv " & vbNewLine _
               & "WHERE DUE_DATE = TO_DATE('" & DueDate & "', 'MM/DD/YYYY') AND REGION_GROUP = 'MINDANAO'"
            End If

           
            report = Me.NpgDataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            result = Me.GetWESMBillList(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function

    Private Function GetWESMBillList(ByVal dr As IDataReader) As List(Of WESMBill)
        Dim result As New List(Of WESMBill)
        Dim cntr As Long = 0
        Try
            While dr.Read()
                cntr += 1
                With dr
                    Using _WESMBill As New WESMBill
                        _WESMBill.BatchCode = .Item("BATCH_CODE").ToString()
                        _WESMBill.AMCode = .Item("AM_CODE").ToString()
                        _WESMBill.BillingPeriod = CInt(.Item("BILLING_PERIOD"))
                        _WESMBill.SettlementRun = CStr(.Item("STL_RUN"))
                        _WESMBill.IDNumber = CStr(.Item("ID_NUMBER"))
                        _WESMBill.RegistrationID = CStr(.Item("REG_ID"))
                        _WESMBill.ForTheAccountOf = CStr(.Item("FOR_ACCOUNT_OF").ToString())
                        _WESMBill.FullName = CStr(.Item("FULL_NAME").ToString())
                        _WESMBill.InvoiceDate = CDate(.Item("INVOICE_DATE"))
                        _WESMBill.Amount = CDec(.Item("AMOUNT"))
                        _WESMBill.ChargeType = CType(System.Enum.Parse(GetType(EnumChargeType), CStr(.Item("CHARGE_TYPE"))), EnumChargeType)
                        _WESMBill.DueDate = CDate(.Item("DUE_DATE"))
                        _WESMBill.MarketFeesRate = CDec(.Item("MARKET_FEES_RATE"))
                        _WESMBill.Remarks = If(IsDBNull(.Item("REMARKS").ToString()), "", .Item("REMARKS").ToString())
                        result.Add(_WESMBill)
                    End Using
                End With
            End While
        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message & ".. Error starts at row no. " & cntr)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try
        Return result
    End Function
#End Region

#Region "Get WESM Invoice Sales and Purchased"
    Public Function GetWESMInvoiceSalesAndPurchased(ByVal billingperiod As Integer, ByVal settlementrun As String) _
                                                    As List(Of WESMBillSalesAndPurchased)

        Dim result As New List(Of WESMBillSalesAndPurchased)
        Dim report As New DataReport
        Dim SQL As String

        Try
            If AMModule.RegionType = "LV" Then
                'SQL = "SELECT a.* " &
                '  "FROM settlement.vw_txn_wesm_bill_sp_agg a " &
                '  "WHERE a.billing_period = " & billingperiod & " " &
                '  "AND a.stl_run = '" & settlementrun & "' AND REGION_GROUP = 'LUZON_VISAYAS'"

                SQL = "SELECT a.* " &
                  "FROM settlement.vw_txn_wesm_bill_sp a " &
                  "WHERE a.billing_period = " & billingperiod & " " &
                  "AND a.stl_run = '" & settlementrun & "' AND REGION_GROUP = 'LUZON_VISAYAS'"

            Else
                SQL = "SELECT a.* " & _
                  "FROM settlement.vw_txn_wesm_bill_sp_agg a " & _
                  "WHERE a.billing_period = " & billingperiod & " " & _
                  "AND a.stl_run = '" & settlementrun & "' AND REGION_GROUP = 'MINDANAO'"
            End If

            report = Me.NpgDataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            result = Me.GetWESMInvoiceSalesAndPurchased(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function
    Private Function GetWESMInvoiceSalesAndPurchased(ByVal dr As IDataReader) As List(Of WESMBillSalesAndPurchased)
        Dim result As New List(Of WESMBillSalesAndPurchased)

        Try
            While dr.Read()
                Dim item As New WESMBillSalesAndPurchased

                With dr
                    item.BillingPeriod = CInt(Trim(.Item("BILLING_PERIOD")))
                    item.SettlementRun = CStr(Trim(.Item("STL_RUN")))
                    item.IDNumber = New AMParticipants(CStr(Trim(.Item("ID_NUMBER"))))
                    item.RegistrationID = CStr(Trim(.Item("REG_ID")))
                    item.VatableSales = CDec(Trim(.Item("VATABLE_SALES")))
                    item.ZeroRatedSales = CDec(Trim(.Item("ZERO_RATED_SALES")))
                    item.ZeroRatedEcozone = CDec(Trim(.Item("ZERO_RATED_ECOZONE")))
                    item.VatablePurchases = CDec(Trim(.Item("VATABLE_PURCHASES")))
                    item.ZeroRatedPurchases = CDec(Trim(.Item("ZERO_RATED_PURCHASES")))
                    item.NetSettlementAmount = CDec(Trim(.Item("TTA")))
                    item.VATonSales = CDec(Trim(.Item("VAT_ON_SALES")))
                    item.VATonPurchases = CDec(Trim(.Item("VAT_ON_PURCHASES")))
                    item.WithholdingTAX = CDec(Trim(.Item("WITHHOLDING_TAX")))
                    item.GMR = CDec(Trim(.Item("GMR")))
                    item.InvoiceNumber = CStr(Trim(.Item("INVOICE_NO")))
                    result.Add(item)
                End With
            End While
            result.TrimExcess()

        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try

        Return result
    End Function
#End Region

#Region "Get WESM Invoices Total AR"
    Public Function getWESMInvoicesTotalAR(ByVal file_type As EnumFileType, ByVal billing_period As String, ByVal stl_run As String) As Decimal
        Dim result As Decimal
        Dim report As New DataReport
        Dim SQL As String

        Try
            If AMModule.RegionType = "LV" Then
                'SQL = "SELECT sum(total) as TOTAL_AR " & vbNewLine _
                '    & "FROM ( SELECT id_number, reg_id, sum(amount) as total " & vbNewLine _
                '    & "       FROM settlement.vw_txn_wesm_bill_inv_agg " & vbNewLine _
                '    & "       WHERE billing_period = '" & billing_period & "' AND stl_run = '" & stl_run & "' AND region_group = 'LUZON_VISAYAS' AND file_type = '" & file_type & "' AND amount < 0 " & vbNewLine _
                '    & "GROUP BY id_number, reg_id) where total < 0"

                SQL = "SELECT sum(total) as TOTAL_AR " & vbNewLine _
                    & "FROM ( SELECT id_number, reg_id, sum(amount) as total " & vbNewLine _
                    & "       FROM settlement.vw_txn_wesm_bill_inv " & vbNewLine _
                    & "       WHERE billing_period = '" & billing_period & "' AND stl_run = '" & stl_run & "' AND region_group = 'LUZON_VISAYAS' AND file_type = '" & file_type & "' AND amount < 0 " & vbNewLine _
                    & "GROUP BY id_number, reg_id)"
            Else
                SQL = "SELECT sum(total) as TOTAL_AR " & vbNewLine _
                    & "FROM ( SELECT id_number, reg_id, sum(amount) as total " & vbNewLine _
                    & "       FROM settlement.vw_txn_wesm_bill_inv_agg " & vbNewLine _
                    & "       WHERE billing_period = '" & billing_period & "' AND stl_run = '" & stl_run & "' AND region_group = 'MINDANAO'  AND file_type = '" & file_type & "'  AND amount > 0 " & vbNewLine _
                    & "GROUP BY id_number, reg_id) where total < 0"
            End If

            report = Me.NpgDataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            result = Me.getWESMInvoicesTotalAR(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function
    Private Function getWESMInvoicesTotalAR(ByVal dr As IDataReader) As Decimal
        Dim result As Decimal
        Try
            While dr.Read()                
                result = If(IsDBNull(dr.Item("TOTAL_AR")), 0, CDec(Trim(dr.Item("TOTAL_AR"))))
            End While

        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try

        Return result
    End Function
#End Region

#Region "Get WESM Invoices Total AP"
    Public Function getWESMInvoicesTotalAP(ByVal file_type As EnumFileType, ByVal billing_period As String, ByVal stl_run As String) As Decimal
        Dim result As Decimal
        Dim report As New DataReport
        Dim SQL As String

        Try
            If AMModule.RegionType = "LV" Then
                'SQL = "SELECT sum(total) as TOTAL_AP " & vbNewLine _
                '    & "FROM ( SELECT id_number, reg_id, sum(amount) as total " & vbNewLine _
                '    & "       FROM settlement.vw_txn_wesm_bill_inv_agg " & vbNewLine _
                '    & "       WHERE billing_period = '" & billing_period & "' AND stl_run = '" & stl_run & "' AND region_group = 'LUZON_VISAYAS'  AND file_type = '" & file_type & "' AND amount > 0" & vbNewLine _
                '    & "GROUP BY id_number, reg_id) where total > 0"

                SQL = "SELECT sum(total) as TOTAL_AP " & vbNewLine _
                   & "FROM ( SELECT id_number, reg_id, sum(amount) as total " & vbNewLine _
                   & "       FROM settlement.vw_txn_wesm_bill_inv " & vbNewLine _
                   & "       WHERE billing_period = '" & billing_period & "' AND stl_run = '" & stl_run & "' AND region_group = 'LUZON_VISAYAS'  AND file_type = '" & file_type & "' AND amount > 0" & vbNewLine _
                   & "GROUP BY id_number, reg_id)"
            Else
                SQL = "SELECT sum(total) as TOTAL_AP " & vbNewLine _
                    & "FROM ( SELECT id_number, reg_id, sum(amount) as total " & vbNewLine _
                    & "       FROM settlement.vw_txn_wesm_bill_inv_agg " & vbNewLine _
                    & "       WHERE billing_period = '" & billing_period & "' AND stl_run = '" & stl_run & "' AND region_group = 'MINDANAO'  AND file_type = '" & file_type & "' AND amount > 0" & vbNewLine _
                    & "GROUP BY id_number, reg_id) where total > 0"
            End If

            report = Me.NpgDataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            result = Me.getWESMInvoicesTotalAP(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function
    Private Function getWESMInvoicesTotalAP(ByVal dr As IDataReader) As Decimal
        Dim result As Decimal
        Try
            While dr.Read()
                result = If(IsDBNull(dr.Item("TOTAL_AP")), 0, CDec(Trim(dr.Item("TOTAL_AP"))))
            End While

        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try

        Return result
    End Function
#End Region

#Region "Get WESM Sales & Purchases Total AR"
    Public Function getWESMSalesPurchasesTotalAR(ByVal file_type As EnumFileType, ByVal billing_period As String, ByVal stl_run As String) As Decimal
        Dim result As Decimal
        Dim report As New DataReport
        Dim SQL As String

        Try
            If AMModule.RegionType = "LV" Then
                SQL = "SELECT sum(total) as TOTAL_AR " & vbNewLine _
                    & "FROM ( SELECT id_number, reg_id, (vatable_sales + zero_rated_sales + zero_rated_ecozone + vatable_purchases + zero_rated_purchases + vat_on_sales + vat_on_purchases) as total " & vbNewLine _
                    & "       FROM settlement.vw_txn_wesm_bill_sp " & vbNewLine _
                    & "       WHERE billing_period = '" & billing_period & "' AND stl_run = '" & stl_run & "' AND region_group = 'LUZON_VISAYAS'  AND file_type = '" & file_type & "' " & vbNewLine _
                    & ") where total < 0"
            Else
                SQL = "SELECT sum(total) as TOTAL_AR " & vbNewLine _
                    & "FROM ( SELECT id_number, reg_id, (vatable_sales + zero_rated_sales + zero_rated_ecozone + vatable_purchases + zero_rated_purchases + vat_on_sales + vat_on_purchases) as total " & vbNewLine _
                    & "       FROM settlement.vw_txn_wesm_bill_sp " & vbNewLine _
                    & "       WHERE billing_period = '" & billing_period & "' AND stl_run = '" & stl_run & "' AND region_group = 'MINDANAO'  AND file_type = '" & file_type & "' " & vbNewLine _
                    & ") where total < 0"
            End If

            report = Me.NpgDataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            result = Me.getWESMSalesPurchasesTotalAR(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function
    Private Function getWESMSalesPurchasesTotalAR(ByVal dr As IDataReader) As Decimal
        Dim result As Decimal
        Try
            While dr.Read()
                result = If(IsDBNull(dr.Item("TOTAL_AR")), 0, CDec(Trim(dr.Item("TOTAL_AR"))))
            End While

        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try

        Return result
    End Function
#End Region

#Region "Get WESM Sales & Purchases Total AP"
    Public Function getWESMSalesPurchasesTotalAP(ByVal file_type As EnumFileType, ByVal billing_period As String, ByVal stl_run As String) As Decimal
        Dim result As Decimal
        Dim report As New DataReport
        Dim SQL As String

        Try
            If AMModule.RegionType = "LV" Then
                SQL = "SELECT sum(total) as TOTAL_AP " & vbNewLine _
                    & "FROM ( SELECT id_number, reg_id, (vatable_sales + zero_rated_sales + zero_rated_ecozone + vatable_purchases + zero_rated_purchases + vat_on_sales + vat_on_purchases) as total " & vbNewLine _
                    & "       FROM settlement.vw_txn_wesm_bill_sp " & vbNewLine _
                    & "       WHERE billing_period = '" & billing_period & "' AND stl_run = '" & stl_run & "' AND region_group = 'LUZON_VISAYAS'  AND file_type = '" & file_type & "' " & vbNewLine _
                    & ") where total > 0"
            Else
                SQL = "SELECT sum(total) as TOTAL_AP " & vbNewLine _
                    & "FROM ( SELECT id_number, reg_id, (vatable_sales + zero_rated_sales + zero_rated_ecozone + vatable_purchases + zero_rated_purchases + vat_on_sales + vat_on_purchases) as total " & vbNewLine _
                    & "       FROM settlement.vw_txn_wesm_bill_sp " & vbNewLine _
                    & "       WHERE billing_period = '" & billing_period & "' AND stl_run = '" & stl_run & "' AND region_group = 'MINDANAO'  AND file_type = '" & file_type & "' " & vbNewLine _
                    & ") where total > 0"
            End If

            report = Me.NpgDataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            result = Me.getWESMSalesPurchasesTotalAP(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function
    Private Function getWESMSalesPurchasesTotalAP(ByVal dr As IDataReader) As Decimal
        Dim result As Decimal
        Try
            While dr.Read()
                result = If(IsDBNull(dr.Item("TOTAL_AP")), 0, CDec(Trim(dr.Item("TOTAL_AP"))))
            End While

        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try

        Return result
    End Function
#End Region

#Region "Get WESM Invoices"
    Public Function GetWESMInvoices(ByVal duedate As Date) As List(Of WESMInvoice)
        Dim result As New List(Of WESMInvoice)
        Dim report As New DataReport
        Dim SQL As String

        Try
            If AMModule.RegionType = "LV" Then
                'SQL = "SELECT * FROM settlement.vw_txn_wesm_bill_inv_agg WHERE DUE_DATE = TO_DATE('" & duedate & "', 'mm/dd/yyyy') AND REGION_GROUP = 'LUZON_VISAYAS'"
                SQL = "SELECT * FROM settlement.vw_txn_wesm_bill_inv WHERE DUE_DATE = TO_DATE('" & duedate & "', 'mm/dd/yyyy') AND REGION_GROUP = 'LUZON_VISAYAS'"
            Else
                SQL = "SELECT * FROM settlement.vw_txn_wesm_bill_inv_agg WHERE DUE_DATE = TO_DATE('" & duedate & "', 'mm/dd/yyyy')  AND REGION_GROUP = 'MINDANAO'"
            End If

            report = Me.NpgDataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            result = Me.GetWESMInvoices(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function


    Private Function GetWESMInvoices(ByVal dr As IDataReader) As List(Of WESMInvoice)
        Dim result As New List(Of WESMInvoice)

        Try
            While dr.Read()
                Dim item As New WESMInvoice

                With dr
                    item.FileType = CType(System.Enum.Parse(GetType(EnumFileType), CStr(.Item("FILE_TYPE"))), EnumFileType)
                    item.BillingPeriod = CInt(Trim(.Item("BILLING_PERIOD")))
                    item.SettlementRun = CStr(Trim(.Item("STL_RUN")))
                    item.IDNumber = CStr(Trim(.Item("ID_NUMBER")))
                    item.RegistrationID = CStr(Trim(.Item("REG_ID")))
                    item.InvoiceNumber = CStr(Trim(.Item("INVOICE_NO")))
                    item.InvoiceDate = CDate(Trim(.Item("INVOICE_DATE")))
                    item.Quantity = CDec(Trim(.Item("QUANTITY")))
                    item.Amount = CDec(Trim(.Item("AMOUNT")))
                    item.ChargeID = CStr(Trim(.Item("CHARGE_ID")))
                    item.DueDate = CDate(Trim(.Item("DUE_DATE")))
                    item.MarketFeesRate = CDec(Trim(.Item("MARKET_FEES_RATE")))
                    item.Remarks = CStr(Trim(.Item("REMARKS").ToString()))
                    result.Add(item)
                End With
            End While
            result.TrimExcess()

        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Finally
            If Not dr.IsClosed Then
                dr.Close()
            End If
        End Try

        Return result
    End Function

#End Region


    Public Sub InitializeData()
        Me._newWESMBillDueDateList = Me._WBillHelper.GetNewWESMInvoicesDueDate()
    End Sub

    Public Function FillInTable(ByVal duedate As Date) As List(Of ImportWESMBillFromCRSS)

        Me._newWESMBillGroupList = New List(Of ImportWESMBillFromCRSS)
        Me._newWESMBillList = New List(Of WESMBill)
        Me._newWESMBillSalesAndPurchaseList = New List(Of WESMBillSalesAndPurchased)
        Me._newWESMInvoiceList = New List(Of WESMInvoice)

        Dim iniWESMInvoicesList As List(Of WESMInvoice) = Me.GetWESMInvoices(duedate)
        Dim iniWESMInvoicesDetail = (From x In iniWESMInvoicesList Select x.BillingPeriod, x.SettlementRun, x.FileType, x.Remarks).Distinct.ToList()

        Dim objImportWESMBillList As New List(Of ImportWESMBillFromCRSS)
        For Each item In iniWESMInvoicesDetail
            Dim newWESMBill As New ImportWESMBillFromCRSS
            With newWESMBill
                .BillingPeriod = item.BillingPeriod
                .FileType = item.FileType
                .Remarks = item.Remarks
                .SettlementRun = item.SettlementRun
                Dim getiniWESMInvoicesDetails = (From x In iniWESMInvoicesList _
                                                 Where x.BillingPeriod = .BillingPeriod And x.FileType = .FileType And x.SettlementRun = .SettlementRun _
                                                 And x.Remarks = .Remarks _
                                                 Select x).ToList()
                Me.GetWESMInvoices(newWESMBill, .BillingPeriod, .FileType, .Remarks, .SettlementRun, getiniWESMInvoicesDetails)


                .TotalARAmount = Me.getWESMInvoicesTotalAR(.FileType, .BillingPeriod, .SettlementRun)
                .TotalAPAmount = Me.getWESMInvoicesTotalAP(.FileType, .BillingPeriod, .SettlementRun)

                .WESMBillType = EnumImportFileType.WESMInvoice
            End With

            objImportWESMBillList.Add(newWESMBill)
            objImportWESMBillList.TrimExcess()

            Dim newWESMBillDetails As New ImportWESMBillFromCRSS
            If Not item.FileType = EnumFileType.MarketFees Then
                With newWESMBillDetails
                    .BillingPeriod = item.BillingPeriod
                    .FileType = item.FileType
                    .Remarks = item.Remarks
                    .SettlementRun = item.SettlementRun
                    Dim getiniWESMInvoicesDetails = (From x In iniWESMInvoicesList _
                                                     Where x.BillingPeriod = .BillingPeriod And x.FileType = .FileType And x.SettlementRun = .SettlementRun _
                                                     Select x).ToList()
                    Me.GetWESMSalesAndPurchases(newWESMBillDetails, .BillingPeriod, .FileType, .Remarks, .SettlementRun, getiniWESMInvoicesDetails)
                    .TotalARAmount = Me.getWESMSalesPurchasesTotalAR(.FileType, .BillingPeriod, .SettlementRun)
                    .TotalAPAmount = Me.getWESMSalesPurchasesTotalAP(.FileType, .BillingPeriod, .SettlementRun)
                    .WESMBillType = EnumImportFileType.WESMSalesAndPurchases
                End With
                objImportWESMBillList.Add(newWESMBillDetails)
                objImportWESMBillList.TrimExcess()
            End If
        Next
        Me._newWESMBillGroupList = objImportWESMBillList
        Return objImportWESMBillList
    End Function

    Private Sub GetWESMSalesAndPurchases(ByRef newImpWBFCRSS As ImportWESMBillFromCRSS, ByVal obillingperiod As Integer,
                                   ByVal ofiletype As EnumFileType, ByVal oremarks As String, ByVal osettlementrun As String,
                                   ByVal WESMInvoicesList As List(Of WESMInvoice))
        Dim iniWESMBillList As New List(Of WESMBillSalesAndPurchased)
        Dim indexRow As Integer = 0
        Dim oListOfErrors As New List(Of String)
        'Check Participants
        Dim getAllParticipants = (From x In WBillHelper.GetAMParticipants() Where x.Status = EnumStatus.Active Select x).ToList()
        Dim getWBSChangeParentList As List(Of WESMBillSummaryChangeParentId) = (From x In WBillHelper.GetWESMBillSummaryChangeParentIDAll() Where x.Status = EnumStatus.Active Select x).ToList()        
        Dim getWBSListPerGroup = (From x In WESMInvoicesList Select x.IDNumber, x.RegistrationID, x.FileType, x.BillingPeriod, x.SettlementRun).Distinct.ToList()
        Dim getWBSSalesAndPurchases As List(Of WESMBillSalesAndPurchased) = Me.GetWESMInvoiceSalesAndPurchased(obillingperiod, osettlementrun)

        For Each item In getWBSListPerGroup

            Dim getWBSItem = (From x In WESMInvoicesList Where x.IDNumber = item.IDNumber And x.RegistrationID = item.RegistrationID Select x).ToList()
            If item.FileType = EnumFileType.Energy Then
                Dim itemWESMBillAmount = (From x In WESMInvoicesList Join y In ChargeID On _
                                         x.ChargeID Equals y.ChargeId _
                                         Where x.IDNumber = item.IDNumber _
                                         And x.RegistrationID = item.RegistrationID _
                                         And Not x.ChargeID.Contains("TAX")
                                         Select x.Amount).Sum()

                Dim itemWESMBillAmountVat = (From x In WESMInvoicesList Join y In ChargeID On _
                                         x.ChargeID Equals y.ChargeId _
                                         Where x.IDNumber = item.IDNumber _
                                         And x.RegistrationID = item.RegistrationID _
                                         And x.ChargeID.Contains("TAX")
                                         Select x.Amount).Sum()

                Dim getWBSSalesAndPurchasesPerID = (From x In getWBSSalesAndPurchases _
                                                Where x.BillingPeriod = item.BillingPeriod And x.SettlementRun = item.SettlementRun _
                                                And x.IDNumber.IDNumber = item.IDNumber And x.RegistrationID = item.RegistrationID _
                                                Select x).FirstOrDefault

                If getWBSSalesAndPurchasesPerID Is Nothing Then
                    oListOfErrors.Add("Cannot found sales and purchases of MP that has " & vbNewLine _
                                    & "IDNumber: " & item.IDNumber & vbNewLine _
                                    & "RegistrationID: " & item.RegistrationID & vbNewLine _
                                    & "Row Index No: " & indexRow.ToString)
                Else
                    Dim selectedItem = getWBSSalesAndPurchasesPerID
                    Dim TotalTTA As Decimal = selectedItem.NetSettlementAmount
                    Dim TotalTTAperColumn As Decimal = selectedItem.VatableSales + selectedItem.ZeroRatedSales + selectedItem.ZeroRatedEcozone + selectedItem.VatablePurchases + selectedItem.ZeroRatedPurchases

                    Dim totalVATinSP As Decimal = selectedItem.VATonSales + selectedItem.VATonPurchases

                    If itemWESMBillAmount <> TotalTTA And itemWESMBillAmount <> TotalTTAperColumn And Not getWBSSalesAndPurchasesPerID Is Nothing Then
                        'If itemWESMBillAmount <> TotalTTAperColumn And Not getWBSSalesAndPurchasesPerID Is Nothing Then
                        oListOfErrors.Add("There is difference between the Invoice and Sales and Purchases Net Amount: " & vbNewLine _
                                         & "ID Number: " & selectedItem.IDNumber.IDNumber & vbNewLine _
                                         & "Registration ID: " & selectedItem.RegistrationID & vbNewLine _
                                         & "Invoice Net Amount: " & FormatNumber(itemWESMBillAmount, 2) & vbNewLine _
                                         & "SalesAndPurchase TTA: " & FormatNumber(TotalTTA, 2) & vbNewLine _
                                         & "SalesAndPurchase Computed TTA: " & FormatNumber(TotalTTAperColumn, 2) & vbNewLine _
                                         & "RowIndex: " & indexRow)
                    End If

                    If itemWESMBillAmountVat <> totalVATinSP And Not getWBSSalesAndPurchasesPerID Is Nothing Then
                        oListOfErrors.Add("There is difference between the Invoice and Sales and Purchases Vat Amount: " & vbNewLine _
                                         & "ID Number: " & selectedItem.IDNumber.IDNumber & vbNewLine _
                                         & "Registration ID: " & selectedItem.RegistrationID & vbNewLine _
                                         & "Invoice Vat Amount: " & FormatNumber(itemWESMBillAmountVat, 2) & vbNewLine _
                                         & "SalesAndPurchase Vat Amount: " & FormatNumber(totalVATinSP, 2) & vbNewLine _
                                         & "RowIndex: " & indexRow)
                    End If

                    Me._newWESMBillSalesAndPurchaseList.Add(getWBSSalesAndPurchasesPerID)
                    Me._newWESMBillSalesAndPurchaseList.TrimExcess()
                End If
            End If
            indexRow += 1
        Next
        'Me._newWESMBillSalesAndPurchaseList = getWBSSalesAndPurchases
        newImpWBFCRSS.ListOfError = oListOfErrors
    End Sub

    Private Sub GetWESMInvoices(ByRef newImpWBFCRSS As ImportWESMBillFromCRSS, ByVal obillingperiod As Integer,
                                   ByVal ofiletype As EnumFileType, ByVal oremarks As String, ByVal osettlementrun As String,
                                   ByVal WESMInvoicesList As List(Of WESMInvoice))
        Dim iniWESMBillList As New List(Of WESMBill)
        Dim indexRow As Integer = 0

        'Check Participants
        Dim getAllParticipants = (From x In WBillHelper.GetAMParticipants() Where x.Status = EnumStatus.Active Select x).ToList()
        Dim getWBSChangeParentList As List(Of WESMBillSummaryChangeParentId) = (From x In WBillHelper.GetWESMBillSummaryChangeParentIDAll() Where x.Status = EnumStatus.Active Select x).ToList()
        Dim getWBSListPerGroup = (From x In WESMInvoicesList Select x.IDNumber, x.RegistrationID, x.FileType, x.BillingPeriod, x.SettlementRun).Distinct.ToList()
        Dim oListOfErrors As New List(Of String)
        For Each item In WESMInvoicesList
            Dim newWESMBill As New WESMBill
            Dim validateChargeID = (From x In Me.ChargeID Where x.ChargeId = item.ChargeID Select x).FirstOrDefault
            If validateChargeID Is Nothing Then
                oListOfErrors.Add("Charge ID: " & item.ChargeID.ToString & " is not in ChargeID Table.")
            End If

            'disabled due to  crss testing for uploading 05/20/2019
            Dim ParentID = item.IDNumber
            Dim CheckParticipant = (From x In getAllParticipants Where x.IDNumber = ParentID)
            If CheckParticipant.Count = 0 Then
                oListOfErrors.Add("Parent IDNumber: " & ParentID & " is not in Master List.")
            End If

            Dim ChildID = item.RegistrationID
            CheckParticipant = (From x In getAllParticipants Where x.IDNumber = ChildID)
            If CheckParticipant.Count = 0 Then
                oListOfErrors.Add("Child IDNumber: " & ChildID & " is not in Master List.")
            End If

            'Check Billing Period
            Dim CalBilling = WBillHelper.GetCalendarBP()

            Dim countBP = (From x In CalBilling _
                          Where x.BillingPeriod = item.BillingPeriod _
                          Select x).Count()

            If countBP = 0 Then
                oListOfErrors.Add("Billing period " & obillingperiod.ToString() & " does not exist in Calendar BP table")
            End If

            'Dim getParentID As String = (From x In getWBSChangeParentList
            '                             Where x.BillingPeriod = item.BillingPeriod And x.ParentParticipants.IDNumber = item.IDNumber And x.ChildParticipants.IDNumber = item.RegistrationID
            '                             Select x.NewParentParticipants.IDNumber).FirstOrDefault
            'ParentID = ""
            'If getParentID IsNot Nothing Then
            '    ParentID = getParentID
            'Else
            '    ParentID = item.IDNumber
            'End If

            With newWESMBill
                .IDNumber = item.IDNumber 'ParentID
                .RegistrationID = item.RegistrationID
                .InvoiceDate = item.InvoiceDate
                .Amount = item.Amount
                .ChargeType = (From x In Me.ChargeID Where Replace(x.ChargeId, " ", "") = Replace(item.ChargeID.ToUpper, " ", "") Select x.cIDType).FirstOrDefault
                .DueDate = item.DueDate
                .MarketFeesRate = item.MarketFeesRate
                .Remarks = item.Remarks
            End With
            iniWESMBillList.Add(newWESMBill)
            Me._newWESMInvoiceList.Add(item)
            Me._newWESMInvoiceList.TrimExcess()
            indexRow += 1
        Next

        newImpWBFCRSS.ListOfError = oListOfErrors

        Dim tmpWESMBillList = (From x In iniWESMBillList Group x By _
                      x.IDNumber, x.RegistrationID, x.ChargeType _
                      Into _
                      Amount = Sum(x.Amount) _
                      Select New With {.BillingPeriod = obillingperiod, .IDNumber = IDNumber, _
                                       .RegistrationID = RegistrationID, _
                                       .Amount = Amount, .ChargeType = ChargeType, _
                                       .SettlementRun = osettlementrun}).ToList

        For Each item In tmpWESMBillList
            Dim selectedItem = item

            Dim itemWESMBill = (From x In iniWESMBillList _
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

            Me._newWESMBillList.Add(itemNewWESMBill)
            Me._newWESMBillList.TrimExcess()
        Next

    End Sub

    Public Function GetAggregatedWESMInvoicesList(ByVal getWBSCRSSMappingList As List(Of WESMInvoiceCRSSMappping),
                                                  ByVal billingPeriod As Integer, ByVal stlRun As String, ByVal fileType As EnumFileType) As List(Of WESMInvoice)
        Dim ret As New List(Of WESMInvoice)
        'Added by LAVV 08082021 for aggregated WESM Innvoices                        
        Dim dicCRSSMapping As New Dictionary(Of String, WESMInvoice)
        Dim getWESMInvoiceList As List(Of WESMInvoice) = (From x In Me.newWESMInvoiceList Where x.BillingPeriod = billingPeriod And x.SettlementRun = stlRun And x.FileType = fileType Select x).ToList

        For Each item In getWESMInvoiceList          
            Dim getNewRegID As WESMInvoiceCRSSMappping = (From x In getWBSCRSSMappingList Where x.IDNumber = item.IDNumber And x.RegIDNumber = item.RegistrationID Select x).FirstOrDefault          
            If getNewRegID Is Nothing Then
                Dim keyStr As String = item.IDNumber & "|" & item.RegistrationID & "|" & item.ChargeID.ToString
                If Not dicCRSSMapping.ContainsKey(keyStr) Then
                    dicCRSSMapping.Add(keyStr, item)
                End If
            Else
                Dim keyStr As String = item.IDNumber & "|" & getNewRegID.NewRegIDNumber & "|" & item.ChargeID.ToString
                If Not dicCRSSMapping.ContainsKey(keyStr) Then
                    Dim itemNewRegID As New WESMInvoice
                    With itemNewRegID
                        .FileType = item.FileType
                        .BillingPeriod = item.BillingPeriod
                        .SettlementRun = item.SettlementRun
                        .IDNumber = item.IDNumber
                        .RegistrationID = getNewRegID.NewRegIDNumber
                        .ForTheAccountOf = item.ForTheAccountOf
                        .FullName = item.FullName
                        .InvoiceNumber = item.InvoiceNumber
                        .InvoiceDate = item.InvoiceDate
                        .Quantity = item.Quantity
                        .MarketFeesRate = item.MarketFeesRate
                        .Amount = item.Amount
                        .ChargeID = item.ChargeID
                        .DueDate = item.DueDate
                        .Remarks = item.Remarks
                    End With
                    dicCRSSMapping.Add(keyStr, itemNewRegID)
                Else
                    With dicCRSSMapping.Item(keyStr)
                        .Quantity += item.Quantity
                        .Amount += item.Amount
                    End With
                End If
            End If
        Next

        'Loop to get all aggregated invoices and put to a new list of WESM Invoices        
        For Each key In dicCRSSMapping.Keys
            ret.Add(dicCRSSMapping.Item(key))
        Next
        ret.TrimExcess()

        Return ret
    End Function

    Public Function GetAggregatedWESMSalesPurchases(ByVal getWBSCRSSMappingList As List(Of WESMInvoiceCRSSMappping),
                                                  ByVal billingPeriod As Integer, ByVal stlRun As String) As List(Of WESMBillSalesAndPurchased)
        Dim ret As New List(Of WESMBillSalesAndPurchased)
        Dim getWESMBillSalesPurchasesList As List(Of WESMBillSalesAndPurchased) = (From x In Me.newWESMBillSalesPurchaseList Where x.BillingPeriod = billingPeriod And x.SettlementRun = stlRun).ToList()

        'Added by LAVV 08082021 for aggregated WESM Sales and Purchases                        
        Dim dicCRSSMappingSAP As New Dictionary(Of String, WESMBillSalesAndPurchased)
        For Each item In getWESMBillSalesPurchasesList
            Dim getNewRegID As WESMInvoiceCRSSMappping = (From x In getWBSCRSSMappingList Where x.IDNumber = item.IDNumber.IDNumber And x.RegIDNumber = item.RegistrationID Select x).FirstOrDefault
         
            If getNewRegID Is Nothing Then
                Dim keyStr As String = item.IDNumber.IDNumber & "|" & item.RegistrationID
                If Not dicCRSSMappingSAP.ContainsKey(keyStr) Then
                    dicCRSSMappingSAP.Add(keyStr, item)
                End If
            Else
                Dim keyStr As String = item.IDNumber.IDNumber & "|" & getNewRegID.NewRegIDNumber
                If Not dicCRSSMappingSAP.ContainsKey(keyStr) Then
                    Dim itemNewRegID As New WESMBillSalesAndPurchased
                    With itemNewRegID
                        .BillingPeriod = item.BillingPeriod
                        .SettlementRun = item.SettlementRun
                        .IDNumber = item.IDNumber
                        .RegistrationID = getNewRegID.NewRegIDNumber
                        .InvoiceNumber = item.InvoiceNumber
                        .VatableSales = item.VatableSales
                        .ZeroRatedSales = item.ZeroRatedSales
                        .VatablePurchases = item.VatablePurchases
                        .ZeroRatedEcozone = item.ZeroRatedEcozone
                        .ZeroRatedPurchases = item.ZeroRatedPurchases
                        .NetSettlementAmount = item.NetSettlementAmount
                        .VATonSales = item.VATonSales
                        .VATonPurchases = item.VATonPurchases
                        .WithholdingTAX = item.WithholdingTAX
                        .GMR = item.GMR
                    End With
                    dicCRSSMappingSAP.Add(keyStr, itemNewRegID)
                Else
                    With dicCRSSMappingSAP.Item(keyStr)
                        .VatableSales += item.VatableSales
                        .ZeroRatedSales += item.ZeroRatedSales
                        .VatablePurchases += item.VatablePurchases
                        .ZeroRatedPurchases += item.ZeroRatedPurchases
                        .ZeroRatedEcozone += item.ZeroRatedEcozone
                        .NetSettlementAmount += item.NetSettlementAmount
                        .VATonSales += item.VATonSales
                        .VATonPurchases += item.VATonPurchases
                        .WithholdingTAX += item.WithholdingTAX
                    End With
                End If
            End If
        Next

        'Loop to get all aggregated sales and purchases and put to a new list of WESM Sales and Purchases
        For Each key In dicCRSSMappingSAP.Keys
            ret.Add(dicCRSSMappingSAP.Item(key))
        Next
        ret.TrimExcess()

        Return ret
    End Function

    Public Function GetAggregatedWESMBills(ByVal getWBSCRSSMappingList As List(Of WESMInvoiceCRSSMappping),
                                           ByVal billingPeriod As Integer, ByVal stlRun As String, ByVal chargeTye As EnumChargeType) As List(Of WESMBill)
        Dim ret As New List(Of WESMBill)
        'Added by LAVV 08082021 for aggregated WESM Bills                        
        Dim dicCRSSMapping As New Dictionary(Of String, WESMBill)
        Dim listOfWESMBills As New List(Of WESMBill)

        If chargeTye = EnumChargeType.E Then
            listOfWESMBills = (From x In Me.newWESMBillList Where x.BillingPeriod = billingPeriod And x.SettlementRun = stlRun And (x.ChargeType = EnumChargeType.E Or x.ChargeType = EnumChargeType.EV) Select x Select x).ToList()
        Else
            listOfWESMBills = (From x In Me.newWESMBillList Where x.BillingPeriod = billingPeriod And x.SettlementRun = stlRun And (x.ChargeType = EnumChargeType.MF Or x.ChargeType = EnumChargeType.MFV) Select x Select x).ToList()
        End If

        For Each item In listOfWESMBills
            Dim getNewRegID As WESMInvoiceCRSSMappping = (From x In getWBSCRSSMappingList Where x.IDNumber = item.IDNumber And x.RegIDNumber = item.RegistrationID Select x).FirstOrDefault            
            If getNewRegID Is Nothing Then
                Dim keyStr As String = item.IDNumber & "|" & item.RegistrationID & "|" & item.ChargeType.ToString
                If Not dicCRSSMapping.ContainsKey(keyStr) Then
                    dicCRSSMapping.Add(keyStr, item)
                End If
            Else
                Dim keyStr As String = item.IDNumber & "|" & getNewRegID.NewRegIDNumber & "|" & item.ChargeType.ToString
                If Not dicCRSSMapping.ContainsKey(keyStr) Then
                    Dim itemNewRegID As New WESMBill
                    With itemNewRegID
                        .BillingPeriod = item.BillingPeriod
                        .IDNumber = item.IDNumber
                        .RegistrationID = getNewRegID.NewRegIDNumber
                        .ChargeType = item.ChargeType
                        .Amount = item.Amount
                        .SettlementRun = item.SettlementRun
                        .ForTheAccountOf = item.ForTheAccountOf
                        .FullName = item.FullName
                        .InvoiceDate = item.InvoiceDate
                        .DueDate = item.DueDate
                        .MarketFeesRate = item.MarketFeesRate
                        .Remarks = item.Remarks
                    End With
                    dicCRSSMapping.Add(keyStr, itemNewRegID)
                Else
                    With dicCRSSMapping.Item(keyStr)
                        .Amount += item.Amount
                    End With
                End If
            End If
        Next

        'Loop to get all aggregated invoices and put to a new list of WESM Invoices        
        For Each key In dicCRSSMapping.Keys
            ret.Add(dicCRSSMapping.Item(key))
        Next
        ret.TrimExcess()

        Return ret
    End Function

End Class
