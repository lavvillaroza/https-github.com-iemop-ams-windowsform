'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             frmReportViewer
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     August 25, 2011
'Development Group:      Software Development and Support Division
'Description:            Generic form which is used to view RPT files.
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   August 25, 2011         Vladimir E. Espiritu            GUI initialization.
'   September 01, 2011      Vladimir E. Espiritu            Rename the form and added ReportType property. Redesigned also the functionality to 
'                                                           view the report.
'   September 23, 2011      Vladimir E. Espiritu            Changed the viewing of report to have one method per report view.
'   September 30, 2011      Juan Carlo L. Panopio           Added LoadWESMBillCollectionNotice() function
'   October 04, 2011        Vladimir E. Espiritu            Added LoadDebitCreditMemo() function     
'   October 04, 2011        Juan Carlo L. Panopio           Added LoadJournalVoucher() function
'   October 05, 2011        Vladimir E. Espiritu            Removed unused class properties
'   December 11, 2011       Vladimir E. Espiritu            Revised LoadWESMBillReport() function
'   February 05, 2012       Vladimir E. Espiritu            Added LoadWESMInvoice() function
'

Option Explicit On
Option Strict On

Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports AccountsManagementObjects
Imports AccountsManagementLogic
Imports System.Drawing.Printing

Public Class frmReportViewer
    Dim oRPT As New ReportDocument
    Private bFactory As BusinessFactory
    Private docNumber As String
    Private wbHelper As WESMBillHelper
    Private dSource As DataTable
    Public printingOR As Boolean

#Region "Property"
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me._FormTitle = ""
    End Sub

    Private _FormTitle As String
    Public Property FormTitle() As String
        Get
            Return _FormTitle
        End Get
        Set(ByVal value As String)
            _FormTitle = value
        End Set
    End Property

    Private Sub frmReportViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = Me.FormTitle

        If printingOR = True Then
            ' Hide default button
            RPTViewer.ShowPrintButton = False

            ' New print button
            For Each ctrl As Control In RPTViewer.Controls
                If TypeOf ctrl Is Windows.Forms.ToolStrip Then
                    Dim btnNew As New ToolStripButton
                    btnNew.Text = "Print"
                    btnNew.ToolTipText = "Print"
                    btnNew.Image = My.Resources.PrinterIcon22x22
                    btnNew.DisplayStyle = ToolStripItemDisplayStyle.Image

                    CType(ctrl, ToolStrip).Items.Insert(0, btnNew)

                    AddHandler btnNew.Click, AddressOf PrintingCounter
                End If
            Next
        End If
    End Sub

#End Region


    Public Sub LoadWESMBillReportSummary(ByVal datasource As DataSet, ByVal ChargeType As String, _
                                         ByVal ParticipantId As String, ByVal Dates As String, ByVal BillPeriod As String)
        Me.FormTitle = "WESM Bill Summary Report"
        Dim rpt As New RPTWESMBillSummaryOnly

        Dim fParticipantIDValue As New ParameterDiscreteValue
        Dim fChargeTypeValue As New ParameterDiscreteValue
        Dim fDueDateValue As New ParameterDiscreteValue
        Dim fBillPeriod As New ParameterDiscreteValue

        Dim paramParticipantID As New ParameterField
        Dim paramChargeType As New ParameterField
        Dim paramDueDate As New ParameterField
        Dim paramBillPeriod As New ParameterField

        paramParticipantID.Name = "paramParticipant_ID"
        paramDueDate.Name = "paramDue_Date"
        paramChargeType.Name = "paramCharge_type"
        paramBillPeriod.Name = "paramBillingPeriod"

        fParticipantIDValue.Value = ParticipantId
        fChargeTypeValue.Value = ChargeType
        fDueDateValue.Value = Dates
        fBillPeriod.Value = BillPeriod

        paramChargeType.CurrentValues.Add(fChargeTypeValue)
        paramParticipantID.CurrentValues.Add(fParticipantIDValue)
        paramDueDate.CurrentValues.Add(fDueDateValue)
        paramBillPeriod.CurrentValues.Add(fBillPeriod)

        paramChargeType.HasCurrentValue = True
        paramParticipantID.HasCurrentValue = True
        paramDueDate.HasCurrentValue = True
        paramBillPeriod.HasCurrentValue = True

        rpt.SetDataSource(datasource)

        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(paramBillPeriod)
            .ParameterFieldInfo.Add(paramParticipantID)
            .ParameterFieldInfo.Add(paramChargeType)
            .ParameterFieldInfo.Add(paramDueDate)
        End With
    End Sub

    Public Sub LoadWESMBillCollectionNotice(ByVal datasource As DataSet, ByVal DateDue As Date, ByVal DefaultInterestRate As Decimal)
        Me.FormTitle = "WESM Collection Notice"
        Dim rpt As New RPTCollectionNotice

        Dim fDueDateValue As New ParameterDiscreteValue
        Dim fDefaultRate As New ParameterDiscreteValue

        Dim ParamDueDate As New ParameterField
        Dim ParamDefaultRate As New ParameterField

        ParamDueDate.Name = "paramDueDate"
        ParamDefaultRate.Name = "paramDefaultRate"

        fDueDateValue.Value = DateDue.ToString("dd-MMM-yyyy")
        fDefaultRate.Value = DefaultInterestRate

        ParamDueDate.CurrentValues.Add(fDueDateValue)
        ParamDefaultRate.CurrentValues.Add(fDefaultRate)

        ParamDueDate.HasCurrentValue = True
        ParamDefaultRate.HasCurrentValue = True

        rpt.SetDataSource(datasource)
        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(ParamDueDate)
            .ParameterFieldInfo.Add(ParamDefaultRate)
        End With
    End Sub

    Public Sub LoadWESMBillSubReportSummaryDetails(ByVal datasource As DataTable)
        Me.FormTitle = "WESM Bill Summary, Participant Details"
        Dim rpt As New subRPTWESMBillSummaryDetails
        rpt.SetDataSource(datasource)
        With Me.RPTViewer
            .ReportSource = rpt
        End With
    End Sub

    Public Sub LoadJournalVoucherDraft(ByVal datasource As DataSet, Optional ByVal HeaderString As String = "")
        Me.FormTitle = "Journal Voucher"
        Dim rpt As New RPTJournalVoucherDraft

        Dim vParamHeader As New ParameterDiscreteValue
        Dim fParamHeader As New ParameterField
        Dim fParamBIRPermit As New ParameterField
        Dim vParamBIRPermit As New ParameterDiscreteValue
        Dim fParamSeriesNo As New ParameterField
        Dim vParamSeriesNo As New ParameterDiscreteValue
        Dim paramBIRDateIssuedVal As New ParameterDiscreteValue
        Dim paramBIRDateIssuedField As New ParameterField

        Dim paramBIRValidUntilVal As New ParameterDiscreteValue
        Dim paramBIRValidUntilField As New ParameterField


        fParamBIRPermit.Name = "paramBIRPermit"
        vParamBIRPermit.Value = AMModule.BIRPermitNumber
        fParamHeader.Name = "paramHeader"
        vParamHeader.Value = HeaderString
        fParamSeriesNo.Name = "paramSeriesNo"
        vParamSeriesNo.Value = AMModule.JVNumberPrefix.ToString


        fParamHeader.CurrentValues.Add(vParamHeader)
        fParamHeader.HasCurrentValue = True
        fParamBIRPermit.CurrentValues.Add(vParamBIRPermit)
        fParamBIRPermit.HasCurrentValue = True

        paramBIRDateIssuedField.Name = "paramBIRDateIssued"
        paramBIRDateIssuedVal.Value = AMModule.BIRDateIssued
        paramBIRDateIssuedField.CurrentValues.Add(paramBIRDateIssuedVal)
        paramBIRDateIssuedField.HasCurrentValue = True

        paramBIRValidUntilField.Name = "paramBIRValidUntil"
        paramBIRValidUntilVal.Value = AMModule.BIRValidUntil
        paramBIRValidUntilField.CurrentValues.Add(paramBIRValidUntilVal)
        paramBIRValidUntilField.HasCurrentValue = True

        rpt.SetDataSource(datasource)
        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(fParamHeader)
            .ParameterFieldInfo.Add(fParamBIRPermit)
            .ParameterFieldInfo.Add(fParamSeriesNo)
            .ParameterFieldInfo.Add(paramBIRDateIssuedField)
            .ParameterFieldInfo.Add(paramBIRValidUntilField)
        End With
    End Sub

    Public Sub LoadJournalVoucher(ByVal datasource As DataSet, Optional ByVal HeaderString As String = "")
        Me.FormTitle = "Journal Voucher"
        Dim rpt As New RPTJournalVoucher

        Dim vParamHeader As New ParameterDiscreteValue
        Dim fParamHeader As New ParameterField
        Dim fParamBIRPermit As New ParameterField
        Dim vParamBIRPermit As New ParameterDiscreteValue
        Dim fParamSeriesNo As New ParameterField
        Dim vParamSeriesNo As New ParameterDiscreteValue
        Dim paramBIRDateIssuedVal As New ParameterDiscreteValue
        Dim paramBIRDateIssuedField As New ParameterField

        Dim paramBIRValidUntilVal As New ParameterDiscreteValue
        Dim paramBIRValidUntilField As New ParameterField

        fParamBIRPermit.Name = "paramBIRPermit"
        vParamBIRPermit.Value = AMModule.BIRPermitNumber
        fParamHeader.Name = "paramHeader"
        vParamHeader.Value = HeaderString
        fParamSeriesNo.Name = "paramSeriesNo"
        vParamSeriesNo.Value = AMModule.JVNumberPrefix.ToString


        fParamHeader.CurrentValues.Add(vParamHeader)
        fParamHeader.HasCurrentValue = True
        fParamBIRPermit.CurrentValues.Add(vParamBIRPermit)
        fParamBIRPermit.HasCurrentValue = True
        fParamSeriesNo.CurrentValues.Add(vParamSeriesNo)
        fParamSeriesNo.HasCurrentValue = True

        paramBIRDateIssuedField.Name = "paramBIRDateIssued"
        paramBIRDateIssuedVal.Value = AMModule.BIRDateIssued
        paramBIRDateIssuedField.CurrentValues.Add(paramBIRDateIssuedVal)
        paramBIRDateIssuedField.HasCurrentValue = True

        paramBIRValidUntilField.Name = "paramBIRValidUntil"
        paramBIRValidUntilVal.Value = AMModule.BIRValidUntil
        paramBIRValidUntilField.CurrentValues.Add(paramBIRValidUntilVal)
        paramBIRValidUntilField.HasCurrentValue = True

        rpt.SetDataSource(datasource)
        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(fParamHeader)
            .ParameterFieldInfo.Add(fParamBIRPermit)
            .ParameterFieldInfo.Add(fParamSeriesNo)
            .ParameterFieldInfo.Add(paramBIRDateIssuedField)
            .ParameterFieldInfo.Add(paramBIRValidUntilField)
        End With
    End Sub

    Public Sub LoadWESMBillReportSummaryDetails(ByVal datasource As DataTable, ByVal BillPeriod As String, _
                                        ByVal ChargeType As String, ByVal ParticipantId As String, ByVal Dates As String)

        Me.FormTitle = "WESM Bill Summary, Participant Details"
        Dim rpt As New RPTWESMBillSummaryOnly

        Dim fParticipantIDValue As New ParameterDiscreteValue
        Dim fChargeTypeValue As New ParameterDiscreteValue
        Dim fTranDateValue As New ParameterDiscreteValue
        Dim fBillingPeriodValue As New ParameterDiscreteValue

        Dim paramBillingPeriod As New ParameterField
        Dim paramParticipantID As New ParameterField
        Dim paramChargeType As New ParameterField
        Dim paramTranDate As New ParameterField

        paramParticipantID.Name = "paramParticipant_ID"
        paramChargeType.Name = "paramCharge_Type"
        paramTranDate.Name = "paramDue_Date"
        paramBillingPeriod.Name = "paramBill_Pd"

        fParticipantIDValue.Value = ParticipantId
        fChargeTypeValue.Value = ChargeType
        fTranDateValue.Value = Dates
        fBillingPeriodValue.Value = BillPeriod

        paramChargeType.CurrentValues.Add(fChargeTypeValue)
        paramParticipantID.CurrentValues.Add(fParticipantIDValue)
        paramTranDate.CurrentValues.Add(fTranDateValue)
        paramBillingPeriod.CurrentValues.Add(fBillingPeriodValue)

        paramBillingPeriod.HasCurrentValue = True
        paramChargeType.HasCurrentValue = True
        paramParticipantID.HasCurrentValue = True
        paramTranDate.HasCurrentValue = True

        rpt.SetDataSource(datasource)
        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(paramParticipantID)
            .ParameterFieldInfo.Add(paramChargeType)
            .ParameterFieldInfo.Add(paramTranDate)
            .ParameterFieldInfo.Add(paramBillingPeriod)
        End With
    End Sub

    Public Sub LoadWESMBillReport(ByVal datasource As DataTable, ByVal billingperiod As String, ByVal billingperiodlabel As String, ByVal wesmbilltype As String, _
                                  ByVal EnergyMFAP As Decimal, ByVal EnergyMFAR As Decimal, ByVal EnergyMFNSS As Decimal, _
                                  ByVal VatAP As Decimal, ByVal VatAR As Decimal, ByVal VatNSS As Decimal)
        Me.Text = "WESM Bill Summary Report"
        Dim rpt As New RPTWESMBillReport

        'Variable for discrete value
        Dim fBillingPeriodLabel As New ParameterDiscreteValue
        Dim fBillingPeriodValue As New ParameterDiscreteValue
        Dim fWESMBillTypeValue As New ParameterDiscreteValue
        Dim fEnergyMFAP As New ParameterDiscreteValue
        Dim fEnergyMFAR As New ParameterDiscreteValue
        Dim fEnergyMFNSS As New ParameterDiscreteValue
        Dim fVatAP As New ParameterDiscreteValue
        Dim fVatAR As New ParameterDiscreteValue
        Dim fVatNSS As New ParameterDiscreteValue

        'Varible for paramter
        Dim paramBillingPeriodLabel As New ParameterField
        Dim paramBillingPeriod As New ParameterField
        Dim paramWESMBillType As New ParameterField
        Dim paramEnergyMFAP As New ParameterField
        Dim paramEnergyMFAR As New ParameterField
        Dim paramEnergyMFNSS As New ParameterField
        Dim paramVatAP As New ParameterField
        Dim paramVatAR As New ParameterField
        Dim paramVatNSS As New ParameterField

        'Assign the name of parameters 
        paramBillingPeriodLabel.Name = "FilterLabel"
        paramBillingPeriod.Name = "BillingPeriod"
        paramWESMBillType.Name = "WESMBillType"
        paramEnergyMFAP.Name = "EnergyMFAP"
        paramEnergyMFAR.Name = "EnergyMFAR"
        paramEnergyMFNSS.Name = "EnergyMFNSS"
        paramVatAP.Name = "VatAP"
        paramVatAR.Name = "VatAR"
        paramVatNSS.Name = "VatNSS"

        'Assign the value of parameters
        fBillingPeriodLabel.Value = billingperiodlabel
        fBillingPeriodValue.Value = billingperiod
        fWESMBillTypeValue.Value = wesmbilltype
        fEnergyMFAP.Value = EnergyMFAP
        fEnergyMFAR.Value = EnergyMFAR
        fEnergyMFNSS.Value = EnergyMFNSS
        fVatAP.Value = VatAP
        fVatAR.Value = VatAR
        fVatNSS.Value = VatNSS

        'Assign the current values
        paramBillingPeriodLabel.CurrentValues.Add(fBillingPeriodLabel)
        paramBillingPeriod.CurrentValues.Add(fBillingPeriodValue)
        paramWESMBillType.CurrentValues.Add(fWESMBillTypeValue)
        paramEnergyMFAP.CurrentValues.Add(fEnergyMFAP)
        paramEnergyMFAR.CurrentValues.Add(fEnergyMFAR)
        paramEnergyMFNSS.CurrentValues.Add(fEnergyMFNSS)
        paramVatAP.CurrentValues.Add(fVatAP)
        paramVatAR.CurrentValues.Add(fVatAR)
        paramVatNSS.CurrentValues.Add(fVatNSS)

        'Assign if the parameters has current values
        paramBillingPeriodLabel.HasCurrentValue = True
        paramBillingPeriod.HasCurrentValue = True
        paramWESMBillType.HasCurrentValue = True
        paramEnergyMFAP.HasCurrentValue = True
        paramEnergyMFAR.HasCurrentValue = True
        paramEnergyMFNSS.HasCurrentValue = True
        paramVatAP.HasCurrentValue = True
        paramVatAR.HasCurrentValue = True
        paramVatNSS.HasCurrentValue = True

        rpt.SetDataSource(datasource)
        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(paramBillingPeriodLabel)
            .ParameterFieldInfo.Add(paramBillingPeriod)
            .ParameterFieldInfo.Add(paramWESMBillType)
            .ParameterFieldInfo.Add(paramEnergyMFAP)
            .ParameterFieldInfo.Add(paramEnergyMFAR)
            .ParameterFieldInfo.Add(paramEnergyMFNSS)
            .ParameterFieldInfo.Add(paramVatAP)
            .ParameterFieldInfo.Add(paramVatAR)
            .ParameterFieldInfo.Add(paramVatNSS)
        End With
    End Sub

    Public Sub LoadDebitCreditMemo(ByVal datasource As DataTable)
        Me.FormTitle = "Debit/Credit Memo"
        Dim rpt As New RPTDebitCreditMemo
        Dim paramBIRDateIssuedVal As New ParameterDiscreteValue
        Dim paramBIRDateIssuedField As New ParameterField

        Dim paramBIRValidUntilVal As New ParameterDiscreteValue
        Dim paramBIRValidUntilField As New ParameterField

        Dim paramSeriesNoVal As New ParameterDiscreteValue
        Dim paramSeriesNoField As New ParameterField


        paramBIRDateIssuedField.Name = "paramBIRDateIssued"
        paramBIRDateIssuedVal.Value = AMModule.BIRDateIssued
        paramBIRDateIssuedField.CurrentValues.Add(paramBIRDateIssuedVal)
        paramBIRDateIssuedField.HasCurrentValue = True

        paramBIRValidUntilField.Name = "paramBIRValidUntil"
        paramBIRValidUntilVal.Value = AMModule.BIRValidUntil
        paramBIRValidUntilField.CurrentValues.Add(paramBIRValidUntilVal)
        paramBIRValidUntilField.HasCurrentValue = True

        paramSeriesNoField.Name = "paramSeriesNo"
        paramSeriesNoVal.Value = AMModule.DMCMNumberPrefix.ToString
        paramSeriesNoField.CurrentValues.Add(paramSeriesNoVal)
        paramSeriesNoField.HasCurrentValue = True


        rpt.SetDataSource(datasource)

        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(paramBIRDateIssuedField)
            .ParameterFieldInfo.Add(paramBIRValidUntilField)
            .ParameterFieldInfo.Add(paramSeriesNoField)
        End With
    End Sub

    Public Sub LoadDebitCreditMemoDraft(ByVal datasource As DataTable)
        Me.FormTitle = "Debit/Credit Memo"
        Dim rpt As New RPTDebitCreditMemoDraft
        Dim paramBIRDateIssuedVal As New ParameterDiscreteValue
        Dim paramBIRDateIssuedField As New ParameterField

        Dim paramBIRValidUntilVal As New ParameterDiscreteValue
        Dim paramBIRValidUntilField As New ParameterField

        Dim paramSeriesNoVal As New ParameterDiscreteValue
        Dim paramSeriesNoField As New ParameterField


        paramBIRDateIssuedField.Name = "paramBIRDateIssued"
        paramBIRDateIssuedVal.Value = AMModule.BIRDateIssued
        paramBIRDateIssuedField.CurrentValues.Add(paramBIRDateIssuedVal)
        paramBIRDateIssuedField.HasCurrentValue = True

        paramBIRValidUntilField.Name = "paramBIRValidUntil"
        paramBIRValidUntilVal.Value = AMModule.BIRValidUntil
        paramBIRValidUntilField.CurrentValues.Add(paramBIRValidUntilVal)
        paramBIRValidUntilField.HasCurrentValue = True

        paramSeriesNoField.Name = "paramSeriesNo"
        paramSeriesNoVal.Value = AMModule.DMCMNumberPrefix.ToString
        paramSeriesNoField.CurrentValues.Add(paramSeriesNoVal)
        paramSeriesNoField.HasCurrentValue = True

        rpt.SetDataSource(datasource)

        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(paramBIRDateIssuedField)
            .ParameterFieldInfo.Add(paramBIRValidUntilField)
            .ParameterFieldInfo.Add(paramSeriesNoField)
        End With
    End Sub

    Public Sub LoadJournalVoucherDetailsUploaded(ByVal JVWESMBills As DataSet, ByVal BillPeriod As String, ByVal JVNumber As Integer, ByVal GPRef As String, _
                                                 ByVal BatchCode As String, ByVal Charge As String)
        Me.FormTitle = "WESM Bill Journal Voucher Details Report"
        Dim rpt As New RPTWESMJVDetailsUpload

        Dim dv_BillPeriod As New ParameterDiscreteValue
        Dim dv_JVNumber As New ParameterDiscreteValue
        Dim dv_GPRef As New ParameterDiscreteValue
        Dim dv_BatchCode As New ParameterDiscreteValue
        Dim dv_Charge As New ParameterDiscreteValue
        Dim dv_TotalAP As New ParameterDiscreteValue
        Dim dv_TotalAR As New ParameterDiscreteValue
        Dim dv_TotalNSS As New ParameterDiscreteValue

        Dim paramBillPd As New ParameterField
        Dim paramJVNumber As New ParameterField
        Dim paramGPRef As New ParameterField
        Dim paramBatchCode As New ParameterField
        Dim paramCharge As New ParameterField
        Dim paramTotalAP As New ParameterField
        Dim paramTotalAR As New ParameterField
        Dim paramTotalNSS As New ParameterField

        paramBillPd.Name = "pBillPeriod"
        paramJVNumber.Name = "pJVNumber"
        paramGPRef.Name = "pGPRef"
        paramBatchCode.Name = "pBatch"
        paramCharge.Name = "pCharge"

        dv_BillPeriod.Value = BillPeriod
        dv_JVNumber.Value = bFactory.GenerateBIRDocumentNumber(JVNumber, BIRDocumentsType.JournalVoucher)
        dv_GPRef.Value = GPRef
        dv_BatchCode.Value = BatchCode
        dv_Charge.Value = Charge

        paramBillPd.CurrentValues.Add(dv_BillPeriod)
        paramBillPd.HasCurrentValue = True

        paramJVNumber.CurrentValues.Add(dv_JVNumber)
        paramJVNumber.HasCurrentValue = True

        paramGPRef.CurrentValues.Add(dv_GPRef)
        paramGPRef.HasCurrentValue = True

        paramBatchCode.CurrentValues.Add(dv_BatchCode)
        paramBatchCode.HasCurrentValue = True

        paramCharge.CurrentValues.Add(dv_Charge)
        paramCharge.HasCurrentValue = True

        rpt.SetDataSource(JVWESMBills)

        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Add(paramBillPd)
            .ParameterFieldInfo.Add(paramJVNumber)
            .ParameterFieldInfo.Add(paramGPRef)
            .ParameterFieldInfo.Add(paramBatchCode)
            .ParameterFieldInfo.Add(paramCharge)
        End With

    End Sub

    Public Sub LoadJournalVoucherDetailsAdjustment(ByVal JVWESMBills As DataSet, ByVal BillPeriod As String, ByVal JVNumber As Integer, ByVal GPRef As String, _
                                                 ByVal BatchCode As String, ByVal Charge As String)
        Me.FormTitle = "WESM Bill Journal Voucher Details Report"
        Dim rpt As New RPTWESMJVDetailsUpload

        Dim dv_BillPeriod As New ParameterDiscreteValue
        Dim dv_JVNumber As New ParameterDiscreteValue
        Dim dv_GPRef As New ParameterDiscreteValue
        Dim dv_BatchCode As New ParameterDiscreteValue
        Dim dv_Charge As New ParameterDiscreteValue
        Dim dv_TotalAP As New ParameterDiscreteValue
        Dim dv_TotalAR As New ParameterDiscreteValue
        Dim dv_TotalNSS As New ParameterDiscreteValue

        Dim paramBillPd As New ParameterField
        Dim paramJVNumber As New ParameterField
        Dim paramGPRef As New ParameterField
        Dim paramBatchCode As New ParameterField
        Dim paramCharge As New ParameterField
        Dim paramTotalAP As New ParameterField
        Dim paramTotalAR As New ParameterField
        Dim paramTotalNSS As New ParameterField

        paramBillPd.Name = "pBillPeriod"
        paramJVNumber.Name = "pJVNumber"
        paramGPRef.Name = "pGPRef"
        paramBatchCode.Name = "pBatch"
        paramCharge.Name = "pCharge"
        paramTotalAP.Name = "pTotalAP"
        paramTotalAR.Name = "pTotalAR"
        paramTotalNSS.Name = "pTotalNSS"


        dv_BillPeriod.Value = BillPeriod
        dv_JVNumber.Value = JVNumber
        dv_GPRef.Value = GPRef
        dv_BatchCode.Value = BatchCode
        dv_Charge.Value = Charge
        paramBillPd.CurrentValues.Add(dv_BillPeriod)
        paramBillPd.HasCurrentValue = True

        paramJVNumber.CurrentValues.Add(dv_JVNumber)
        paramJVNumber.HasCurrentValue = True

        paramGPRef.CurrentValues.Add(dv_GPRef)
        paramGPRef.HasCurrentValue = True

        paramBatchCode.CurrentValues.Add(dv_BatchCode)
        paramBatchCode.HasCurrentValue = True

        paramCharge.CurrentValues.Add(dv_Charge)
        paramCharge.HasCurrentValue = True

        'paramTotalAP.CurrentValues.Add(dv_TotalAP)
        'paramTotalAP.HasCurrentValue = True

        'paramTotalAR.CurrentValues.Add(dv_TotalAR)
        'paramTotalAR.HasCurrentValue = True

        'paramTotalNSS.CurrentValues.Add(dv_TotalNSS)
        'paramTotalNSS.HasCurrentValue = True
        rpt.SetDataSource(JVWESMBills)

        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Add(paramBillPd)
            .ParameterFieldInfo.Add(paramJVNumber)
            .ParameterFieldInfo.Add(paramGPRef)
            .ParameterFieldInfo.Add(paramBatchCode)
            .ParameterFieldInfo.Add(paramCharge)
            .ParameterFieldInfo.Add(paramTotalAP)
            .ParameterFieldInfo.Add(paramTotalAR)
            .ParameterFieldInfo.Add(paramTotalNSS)
        End With
    End Sub

    Public Sub LoadJournalVoucherDetailsOffset(ByVal JVWESMBills As DataSet, ByVal BillPeriod As String, ByVal JVNumber As Integer, ByVal GPRef As String, _
                                                 ByVal BatchCode As String, ByVal Charge As String)
        Me.FormTitle = "WESM Bill Journal Voucher Details Report"
        Dim rpt As New RPTWESMJVDetailsOffset

        Dim dv_BillPeriod As New ParameterDiscreteValue
        Dim dv_JVNumber As New ParameterDiscreteValue
        Dim dv_GPRef As New ParameterDiscreteValue
        Dim dv_BatchCode As New ParameterDiscreteValue
        Dim dv_Charge As New ParameterDiscreteValue
        Dim dv_TotalAP As New ParameterDiscreteValue
        Dim dv_TotalAR As New ParameterDiscreteValue
        Dim dv_TotalNSS As New ParameterDiscreteValue

        Dim paramBillPd As New ParameterField
        Dim paramJVNumber As New ParameterField
        Dim paramGPRef As New ParameterField
        Dim paramBatchCode As New ParameterField
        Dim paramCharge As New ParameterField
        Dim paramTotalAP As New ParameterField
        Dim paramTotalAR As New ParameterField
        Dim paramTotalNSS As New ParameterField

        paramBillPd.Name = "pBillPeriod"
        paramJVNumber.Name = "pJVNumber"
        paramGPRef.Name = "pGPRef"
        paramBatchCode.Name = "pBatch"
        paramCharge.Name = "pCharge"

        dv_BillPeriod.Value = BillPeriod
        dv_JVNumber.Value = Me.bFactory.GenerateBIRDocumentNumber(JVNumber, BIRDocumentsType.JournalVoucher)
        dv_GPRef.Value = GPRef
        dv_BatchCode.Value = BatchCode
        dv_Charge.Value = Charge

        dv_TotalAR.Value = 12345
        dv_TotalNSS.Value = 12345

        paramBillPd.CurrentValues.Add(dv_BillPeriod)
        paramBillPd.HasCurrentValue = True

        paramJVNumber.CurrentValues.Add(dv_JVNumber)
        paramJVNumber.HasCurrentValue = True

        paramGPRef.CurrentValues.Add(dv_GPRef)
        paramGPRef.HasCurrentValue = True

        paramBatchCode.CurrentValues.Add(dv_BatchCode)
        paramBatchCode.HasCurrentValue = True

        paramCharge.CurrentValues.Add(dv_Charge)
        paramCharge.HasCurrentValue = True

        rpt.SetDataSource(JVWESMBills)

        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Add(paramBillPd)
            .ParameterFieldInfo.Add(paramJVNumber)
            .ParameterFieldInfo.Add(paramGPRef)
            .ParameterFieldInfo.Add(paramBatchCode)
            .ParameterFieldInfo.Add(paramCharge)
        End With

    End Sub

    Public Sub LoadWESMInvoice(ByVal datasource As DataSet, ByVal chargetype As EnumChargeType, ByVal marketFeeRate As String, ByVal systemGeneratedMessage As String)
        Me.Text = "WESM Invoice"
        Dim rpt As New RPTWESMInvoice

        Dim paramBIRDateIssuedVal As New ParameterDiscreteValue
        Dim paramBIRDateIssuedField As New ParameterField

        Dim paramBIRValidUntilVal As New ParameterDiscreteValue
        Dim paramBIRValidUntilField As New ParameterField

        Dim paramSeriesNoVal As New ParameterDiscreteValue
        Dim paramSeriesNoField As New ParameterField

        Dim paramMarketFeeRateVal As New ParameterDiscreteValue
        Dim paramMarketFeeRateField As New ParameterField

        Dim paramSystemGeneratedMsgVal As New ParameterDiscreteValue
        Dim paramSystemGeneratedMsgField As New ParameterField

        paramBIRDateIssuedField.Name = "paramBIRDateIssued"
        paramBIRDateIssuedVal.Value = AMModule.BIRDateIssued

        paramBIRDateIssuedField.CurrentValues.Add(paramBIRDateIssuedVal)
        paramBIRDateIssuedField.HasCurrentValue = True

        paramBIRValidUntilField.Name = "paramBIRValidUntil"
        paramBIRValidUntilVal.Value = AMModule.BIRValidUntil

        paramBIRValidUntilField.CurrentValues.Add(paramBIRValidUntilVal)
        paramBIRValidUntilField.HasCurrentValue = True

        paramSeriesNoField.Name = "paramSeriesNo"
        paramSeriesNoVal.Value = AMModule.FSNumberPrefix.ToString

        paramSeriesNoField.CurrentValues.Add(paramSeriesNoVal)
        paramSeriesNoField.HasCurrentValue = True

        paramMarketFeeRateField.Name = "paramMarketFeeRate"
        paramMarketFeeRateVal.Value = marketFeeRate

        paramMarketFeeRateField.CurrentValues.Add(paramMarketFeeRateVal)
        paramMarketFeeRateField.HasCurrentValue = True

        paramSystemGeneratedMsgField.Name = "paramSystemGeneratedMsg"
        paramSystemGeneratedMsgVal.Value = systemGeneratedMessage

        paramSystemGeneratedMsgField.CurrentValues.Add(paramSystemGeneratedMsgVal)
        paramSystemGeneratedMsgField.HasCurrentValue = True

        rpt.SetDataSource(datasource)

        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(paramBIRDateIssuedField)
            .ParameterFieldInfo.Add(paramBIRValidUntilField)
            .ParameterFieldInfo.Add(paramSeriesNoField)
            .ParameterFieldInfo.Add(paramMarketFeeRateField)
            .ParameterFieldInfo.Add(paramSystemGeneratedMsgField)
        End With

    End Sub

    Public Sub LoadCollectionSummary(ByVal datasource As DataTable, ByVal dateFrom As Date, ByVal dateTo As Date)
        Me.FormTitle = "Collection Summary"
        Dim rpt As New RPTCollectionSummary

        Dim fDateFrom As New ParameterDiscreteValue
        Dim fDateTo As New ParameterDiscreteValue

        Dim paramDateFrom As New ParameterField
        Dim paramDateTo As New ParameterField

        paramDateFrom.Name = "DateFrom"
        paramDateTo.Name = "DateTo"

        fDateFrom.Value = dateFrom
        fDateTo.Value = dateTo

        paramDateFrom.CurrentValues.Add(fDateFrom)
        paramDateTo.CurrentValues.Add(fDateTo)

        paramDateFrom.HasCurrentValue = True
        paramDateTo.HasCurrentValue = True

        rpt.SetDataSource(datasource)

        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(paramDateFrom)
            .ParameterFieldInfo.Add(paramDateTo)
        End With
    End Sub

    Public Sub LoadCollectionSummary(ByVal datasource As DataTable)
        Me.FormTitle = "Collection Summary"
        Dim rpt As New RPTCollectionSummary

        rpt.SetDataSource(datasource)

        With Me.RPTViewer
            .ReportSource = rpt
        End With
    End Sub

    Public Sub LoadPrudential(ByVal datasource As DataTable)
        Me.FormTitle = "Prudential"
        Dim rpt As New RPTDrawdown

        rpt.SetDataSource(datasource)

        With Me.RPTViewer
            .ReportSource = rpt
        End With
    End Sub

    Public Sub LoadMarginCall(ByVal datasource As DataTable)
        Me.FormTitle = "Margin Call"
        Dim rpt As New RPTMarginCallFinal

        rpt.SetDataSource(datasource)

        With Me.RPTViewer
            .ReportSource = rpt
        End With
    End Sub

    Public Sub LoadFTF(ByVal datasource As DataSet)
        Me.FormTitle = "Fund TransFer Form"
        Dim rpt As New RPTFTFMain

        rpt.SetDataSource(datasource)

        With Me.RPTViewer
            .ReportSource = rpt
        End With
    End Sub

    Public Sub LoadFTFDraft(ByVal datasource As DataSet)
        Me.FormTitle = "Fund TransFer Form"
        Dim rpt As New RPTFTFMainDraft

        rpt.SetDataSource(datasource)

        With Me.RPTViewer
            .ReportSource = rpt
        End With
    End Sub

    Public Sub LoadMarketFeesSummary(ByVal datasource As DataTable)
        Me.FormTitle = "MMarket Fees Summary"
        Dim rpt As New RPTMarketFees

        rpt.SetDataSource(datasource)

        With Me.RPTViewer
            .ReportSource = rpt
        End With
    End Sub

    Public Sub LoadCashReceiptSummary(ByVal datasource As DataTable)
        Me.FormTitle = "Cash Receipt Summary"
        Dim rpt As New RPTCashReceipt

        rpt.SetDataSource(datasource)

        With Me.RPTViewer
            .ReportSource = rpt
        End With
    End Sub

    Public Sub LoadRFP(ByVal ds As DataSet, Optional ByVal ReviewedBy As String = "", Optional ByVal ApprovedBy As String = "", Optional ByVal LoggedIn As String = "")
        Me.FormTitle = "Request for Payment"
        Dim rpt As New RPTRequestForPayment
        rpt.SetDataSource(ds)

        With Me.RPTViewer
            .ReportSource = rpt
        End With

    End Sub

    Public Sub LoadOR(ByVal datasource As DataTable)
        Me.FormTitle = "Official Receipt"
        bFactory = BusinessFactory.GetInstance()
        wbHelper = WESMBillHelper.GetInstance()

        Dim paramBIRDateIssuedVal As New ParameterDiscreteValue
        Dim paramBIRDateIssuedField As New ParameterField

        Dim paramBIRValidUntilVal As New ParameterDiscreteValue
        Dim paramBIRValidUntilField As New ParameterField

        Dim paramSeriesNoVal As New ParameterDiscreteValue
        Dim paramSeriesNoField As New ParameterField


        paramBIRDateIssuedField.Name = "paramBIRDateIssued"
        paramBIRDateIssuedVal.Value = AMModule.BIRDateIssued
        paramBIRDateIssuedField.CurrentValues.Add(paramBIRDateIssuedVal)
        paramBIRDateIssuedField.HasCurrentValue = True

        paramBIRValidUntilField.Name = "paramBIRValidUntil"
        paramBIRValidUntilVal.Value = AMModule.BIRValidUntil
        paramBIRValidUntilField.CurrentValues.Add(paramBIRValidUntilVal)
        paramBIRValidUntilField.HasCurrentValue = True

        paramSeriesNoField.Name = "paramSeriesNo"
        paramSeriesNoVal.Value = AMModule.ORNumberPrefix.ToString
        paramSeriesNoField.CurrentValues.Add(paramSeriesNoVal)
        paramSeriesNoField.HasCurrentValue = True

        dSource = New DataTable

        dSource = datasource

        docNumber = datasource.AsEnumerable().Select(Function(x) x.Field(Of String)("OR_NO")).FirstOrDefault
        Dim docPrintingCount As Integer = wbHelper.GetPrintCount(docNumber)

        If docPrintingCount = 0 Then
            Dim rpt As New RPTOfficialReceiptNew
            rpt.SetDataSource(datasource)

            oRPT = rpt

            With Me.RPTViewer
                .ReportSource = rpt
                .ParameterFieldInfo.Clear()
                .ParameterFieldInfo.Add(paramBIRDateIssuedField)
                .ParameterFieldInfo.Add(paramBIRValidUntilField)
                .ParameterFieldInfo.Add(paramSeriesNoField)
            End With
        ElseIf docPrintingCount >= 1 And docPrintingCount < AMModule.PrintingMaxLimit Then
            Dim rpt As New RPTOfficialReceiptNew
            rpt.SetDataSource(datasource)

            oRPT = rpt

            With Me.RPTViewer
                .ReportSource = rpt
                .ParameterFieldInfo.Clear()
                .ParameterFieldInfo.Add(paramBIRDateIssuedField)
                .ParameterFieldInfo.Add(paramBIRValidUntilField)
                .ParameterFieldInfo.Add(paramSeriesNoField)
            End With
        Else
            Dim rpt2 As New RPTOfficialReceiptNewRePrint
            rpt2.SetDataSource(datasource)

            oRPT = rpt2

            With Me.RPTViewer
                .ReportSource = rpt2
                .ParameterFieldInfo.Clear()
                .ParameterFieldInfo.Add(paramBIRDateIssuedField)
                .ParameterFieldInfo.Add(paramBIRValidUntilField)
                .ParameterFieldInfo.Add(paramSeriesNoField)
            End With
        End If
    End Sub

    Public Sub LoadORCancelled(ByVal datasource As DataTable, odateTimePrinted As Date)
        Me.FormTitle = "Official Receipt"
        Dim dateTimePrinted As New ParameterDiscreteValue
        Dim paramDateTimePrinted As New ParameterField
        Dim paramSeriesNoVal As New ParameterDiscreteValue
        Dim paramSeriesNoField As New ParameterField

        paramDateTimePrinted.Name = "ParamDateTimePrinted"
        dateTimePrinted.Value = odateTimePrinted.ToString

        paramDateTimePrinted.Name = "ParamDateTimePrinted"
        dateTimePrinted.Value = odateTimePrinted.ToString

        paramDateTimePrinted.CurrentValues.Add(dateTimePrinted)
        paramDateTimePrinted.HasCurrentValue = True

        paramSeriesNoField.Name = "paramSeriesNo"
        paramSeriesNoVal.Value = AMModule.ORNumberPrefix.ToString
        paramSeriesNoField.CurrentValues.Add(paramSeriesNoVal)
        paramSeriesNoField.HasCurrentValue = True

        Dim rpt As New RPTOfficialReceiptNewCancelled

        rpt.SetDataSource(datasource)

        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(paramDateTimePrinted)
            .ParameterFieldInfo.Add(paramSeriesNoField)
        End With

    End Sub

    Public Sub LoadCollectionSummaryPerJV(ByVal datasource As DataTable, ByVal TotalCash As Decimal, ByVal TotalDrawdown As Decimal)
        Me.FormTitle = "Collection Summary"
        Dim rpt As New RPTCollectionSummaryPerJV

        Dim fCash As New ParameterDiscreteValue
        Dim fDrawdown As New ParameterDiscreteValue

        Dim paramCash As New ParameterField
        Dim paramDrawdown As New ParameterField

        paramCash.Name = "TotalCash"
        paramDrawdown.Name = "TotalDrawdown"

        fCash.Value = TotalCash
        fDrawdown.Value = TotalDrawdown

        paramCash.CurrentValues.Add(fCash)
        paramDrawdown.CurrentValues.Add(fDrawdown)

        paramCash.HasCurrentValue = True
        paramDrawdown.HasCurrentValue = True

        rpt.SetDataSource(datasource)

        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(paramCash)
            .ParameterFieldInfo.Add(paramDrawdown)

        End With
    End Sub

    Public Sub LoadDMCMSummary(ByVal datasource As DataTable, ByVal AllocationDAte As Date, ByVal PaymentBatch As String)
        Me.FormTitle = "DMCMSummary"
        Dim rpt As New RPTDMCMSummary

        Dim fAllocationDate As New ParameterDiscreteValue
        Dim fPaymentbatch As New ParameterDiscreteValue

        Dim paramAllocationDate As New ParameterField
        Dim paramPaymentBatch As New ParameterField

        paramAllocationDate.Name = "AllocationDate"
        paramPaymentBatch.Name = "PaymentBatch"

        fAllocationDate.Value = AllocationDAte
        fPaymentbatch.Value = PaymentBatch

        paramAllocationDate.CurrentValues.Add(fAllocationDate)
        paramPaymentBatch.CurrentValues.Add(fPaymentbatch)

        paramAllocationDate.HasCurrentValue = True
        paramPaymentBatch.HasCurrentValue = True

        rpt.SetDataSource(datasource)

        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(paramAllocationDate)
            .ParameterFieldInfo.Add(paramPaymentBatch)

        End With
    End Sub

    Public Sub LoadC2PDMCMSummary(ByVal datasource As DataTable, ByVal AllocationDAte As Date, ByVal PaymentBatch As String)
        Me.FormTitle = "C2P DMCMSummary"
        Dim rpt As New RPTC2PDMCMSummary

        Dim fAllocationDate As New ParameterDiscreteValue
        Dim fPaymentbatch As New ParameterDiscreteValue

        Dim paramAllocationDate As New ParameterField
        Dim paramPaymentBatch As New ParameterField

        paramAllocationDate.Name = "AllocationDate"
        paramPaymentBatch.Name = "PaymentBatch"

        fAllocationDate.Value = AllocationDAte
        fPaymentbatch.Value = PaymentBatch

        paramAllocationDate.CurrentValues.Add(fAllocationDate)
        paramPaymentBatch.CurrentValues.Add(fPaymentbatch)

        paramAllocationDate.HasCurrentValue = True
        paramPaymentBatch.HasCurrentValue = True

        rpt.SetDataSource(datasource)

        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(paramAllocationDate)
            .ParameterFieldInfo.Add(paramPaymentBatch)

        End With
    End Sub

    Public Sub LoadNSSSummary(ByVal datasource As DataTable)
        Me.FormTitle = "NSS Summary"
        Dim rpt As New RPTNSSSummary

        rpt.SetDataSource(datasource)

        With Me.RPTViewer
            .ReportSource = rpt
        End With
    End Sub

    Public Sub LoadDefaultNotice(ByVal DefaultDetails As DataSet, ByVal InterestRate As Decimal, ByVal CurDueDate As Date)
        Me.FormTitle = "Default Notice / SOA"
        Dim rpt As New RPTDefaultNotice

        Dim pdvCompanyAccount As New ParameterDiscreteValue
        Dim pfCompanyAccount As New ParameterField

        Dim pdvCompanyShortName As New ParameterDiscreteValue
        Dim pfCompanyShortName As New ParameterField

        pfCompanyAccount.Name = "paramCompanyAccount"
        pdvCompanyAccount.Value = AMModule.CompanyBDOAccountName.ToString

        pfCompanyAccount.HasCurrentValue = True
        pfCompanyAccount.CurrentValues.Add(pdvCompanyAccount)

        pfCompanyShortName.Name = "paramCompanyShortName"
        pdvCompanyShortName.Value = AMModule.CompanyShortName.ToString

        pfCompanyShortName.HasCurrentValue = True
        pfCompanyShortName.CurrentValues.Add(pdvCompanyShortName)

        rpt.SetDataSource(DefaultDetails)

        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(pfCompanyAccount)
            .ParameterFieldInfo.Add(pfCompanyShortName)
        End With
    End Sub

    Public Sub LoadPrudentialHistory(ByVal datasource As DataTable)
        Me.FormTitle = "Transfered Interest into PR"
        Dim rpt As New RPTPRHistory

        rpt.SetDataSource(datasource)

        With Me.RPTViewer
            .ReportSource = rpt
        End With
    End Sub

    Public Sub LoadPrudentialSummaryReport(ByVal datasource As DataTable, ByVal TransType As EnumPrudentialTransType)
        Select Case TransType
            Case EnumPrudentialTransType.Drawdown

            Case EnumPrudentialTransType.Replenishment
                Me.FormTitle = "Prudential Replenishment Summary Report"

            Case EnumPrudentialTransType.InterestAmount
                Me.FormTitle = "Prudential Interest Summary Report"

            Case EnumPrudentialTransType.TransferInterestAmount
                Me.FormTitle = "Prudential Transfer of Interest Summary Report"

        End Select

        Dim rpt As New RPTPrudentialSummaryReport
        rpt.SetDataSource(datasource)

        Dim fTransType As New ParameterDiscreteValue
        Dim ParamTransType As New ParameterField

        ParamTransType.Name = "paramTransType"
        fTransType.Value = TransType

        ParamTransType.CurrentValues.Add(fTransType)
        ParamTransType.HasCurrentValue = True

        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(ParamTransType)
        End With

    End Sub

    Public Sub LoadPaymentSummary(ByVal datasource As DataTable)
        Me.FormTitle = "Payment Summary"
        Dim rpt As New RPTPaymentSummary

        rpt.SetDataSource(datasource)

        With Me.RPTViewer
            .ReportSource = rpt

        End With
    End Sub

    Public Sub LoadCheckVoucher(ByVal ds As DataTable)
        Me.FormTitle = "Check Voucher"
        Dim paramBIRDateIssuedVal As New ParameterDiscreteValue
        Dim paramBIRDateIssuedField As New ParameterField

        Dim paramBIRValidUntilVal As New ParameterDiscreteValue
        Dim paramBIRValidUntilField As New ParameterField

        Dim paramSeriesNoVal As New ParameterDiscreteValue
        Dim paramSeriesNoField As New ParameterField


        paramBIRDateIssuedField.Name = "paramBIRDateIssued"
        paramBIRDateIssuedVal.Value = AMModule.BIRDateIssued
        paramBIRDateIssuedField.CurrentValues.Add(paramBIRDateIssuedVal)
        paramBIRDateIssuedField.HasCurrentValue = True

        paramBIRValidUntilField.Name = "paramBIRValidUntil"
        paramBIRValidUntilVal.Value = AMModule.BIRValidUntil
        paramBIRValidUntilField.CurrentValues.Add(paramBIRValidUntilVal)
        paramBIRValidUntilField.HasCurrentValue = True

        paramSeriesNoField.Name = "paramSeriesNo"
        paramSeriesNoVal.Value = AMModule.CVNumberPrefix.ToString
        paramSeriesNoField.CurrentValues.Add(paramSeriesNoVal)
        paramSeriesNoField.HasCurrentValue = True


        Dim rpt As New RPTCheckVoucher

        rpt.SetDataSource(ds)

        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(paramBIRDateIssuedField)
            .ParameterFieldInfo.Add(paramBIRValidUntilField)
            .ParameterFieldInfo.Add(paramSeriesNoField)
        End With
    End Sub

    Public Sub LoadCheckRegister(ByVal ds As DataTable, ByVal dateFrom As Date, ByVal dateTo As Date)
        Me.FormTitle = "Check Register"
        Dim rpt As New RPTCheckRegister

        Dim dvDateFrom As New ParameterDiscreteValue
        Dim pfDateFrom As New ParameterField

        pfDateFrom.Name = "dFrom"
        dvDateFrom.Value = dateFrom

        Dim dvDateTo As New ParameterDiscreteValue
        Dim pfDateTo As New ParameterField

        pfDateTo.Name = "dTo"
        dvDateTo.Value = dateTo

        pfDateFrom.CurrentValues.Add(dvDateFrom)
        pfDateTo.CurrentValues.Add(dvDateTo)

        pfDateFrom.HasCurrentValue = True
        pfDateTo.HasCurrentValue = True

        rpt.SetDataSource(ds)

        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(pfDateFrom)
            .ParameterFieldInfo.Add(pfDateTo)

        End With

    End Sub

    Public Sub LoadOutstandingCheck(ByVal ds As DataTable, ByVal dateFrom As Date, ByVal dateTo As Date)
        Me.FormTitle = "Outstanding Check Summary"
        Dim rpt As New RPTCheckOutstanding

        Dim dvDateFrom As New ParameterDiscreteValue
        Dim pfDateFrom As New ParameterField

        pfDateFrom.Name = "dFrom"
        dvDateFrom.Value = dateFrom

        Dim dvDateTo As New ParameterDiscreteValue
        Dim pfDateTo As New ParameterField

        pfDateTo.Name = "dTo"
        dvDateTo.Value = dateTo

        pfDateFrom.CurrentValues.Add(dvDateFrom)
        pfDateTo.CurrentValues.Add(dvDateTo)

        pfDateFrom.HasCurrentValue = True
        pfDateTo.HasCurrentValue = True

        rpt.SetDataSource(ds)

        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(pfDateFrom)
            .ParameterFieldInfo.Add(pfDateTo)

        End With

    End Sub

    Public Sub LoadAgingReport(ByVal ds As DataTable, ByVal UserName As String, ByVal UserDate As Date)
        Me.FormTitle = "Detailed Historical Aged Trial Balance"
        Dim rpt As New RPTHistoricalAgedBalance

        Dim dvUsername As New ParameterDiscreteValue
        Dim pUsername As New ParameterField

        pUsername.Name = "paramUsername"
        dvUsername.Value = UserName

        Dim dvDate As New ParameterDiscreteValue
        Dim pDate As New ParameterField

        pDate.Name = "paramUserDate"
        dvDate.Value = UserDate

        pDate.HasCurrentValue = True
        pDate.CurrentValues.Add(dvDate)

        pUsername.HasCurrentValue = True
        pUsername.CurrentValues.Add(dvUsername)

        rpt.SetDataSource(ds)

        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(pDate)
            .ParameterFieldInfo.Add(pUsername)

        End With
    End Sub

    Public Sub LoadDailyCollectionSummary(ByVal datasource As DataTable)
        Me.FormTitle = "Daily Collection Summary"
        Dim rpt As New RPTDailyCollectionSummary

        rpt.SetDataSource(datasource)

        With Me.RPTViewer
            .ReportSource = rpt
        End With
    End Sub

    Public Sub LoadDailyCollectionSummaryCancelled(ByVal datasource As DataTable)
        Me.FormTitle = "Daily Collection Summary"
        Dim rpt As New RPTDailyCollectionSummaryCancelled

        rpt.SetDataSource(datasource)

        With Me.RPTViewer
            .ReportSource = rpt
        End With
    End Sub

    Public Sub LoadMarginCallSummary(ByVal datasource As DataTable)
        Me.FormTitle = "Margin Call Summary"
        Dim rpt As New RPTMarginCallSummary

        rpt.SetDataSource(datasource)

        With Me.RPTViewer
            .ReportSource = rpt
        End With
    End Sub

    Public Sub LoadDrawdown(ByVal datasource As DataTable)
        Me.FormTitle = "Drawdown Notice"
        Dim rpt As New RPTDrawdown

        rpt.SetDataSource(datasource)

        With Me.RPTViewer
            .ReportSource = rpt
        End With
    End Sub

    Public Sub LoadNSSRASummary(ByVal datasource As DataTable)
        Me.FormTitle = "NSSRA Summary"
        Dim rpt As New RPTNSSRASummary

        rpt.SetDataSource(datasource)

        With Me.RPTViewer
            .ReportSource = rpt
        End With
    End Sub

    Public Sub LoadCheckParams(ByVal RepresentativeName As String, ByVal AmountInWords As String, ByVal AmountInFigure As Decimal, ByVal DateRelease As String)
        Me.FormTitle = "Printing of Check"
        Dim rpt As New RPTCheck

        Dim fldName As New ParameterField
        Dim fldAmtWords As New ParameterField
        Dim fldAmtFigure As New ParameterField
        Dim fldDate As New ParameterField

        Dim valName As New ParameterDiscreteValue
        Dim valAmtWords As New ParameterDiscreteValue
        Dim valAmtFigure As New ParameterDiscreteValue
        Dim valDate As New ParameterDiscreteValue

        fldName.Name = "paramName"
        fldAmtWords.Name = "paramAmountInWords"
        fldAmtFigure.Name = "paramAmountInFigures"
        fldDate.Name = "paramDate"

        valName.Value = RepresentativeName
        valAmtWords.Value = AmountInWords
        valAmtFigure.Value = AmountInFigure
        valDate.Value = DateRelease

        fldName.CurrentValues.Add(valName)
        fldAmtFigure.CurrentValues.Add(valAmtFigure)
        fldAmtWords.CurrentValues.Add(valAmtWords)
        fldDate.CurrentValues.Add(valDate)

        fldName.HasCurrentValue = True
        fldAmtWords.HasCurrentValue = True
        fldAmtFigure.HasCurrentValue = True
        fldDate.HasCurrentValue = True

        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(fldName)
            .ParameterFieldInfo.Add(fldAmtWords)
            .ParameterFieldInfo.Add(fldAmtFigure)
            .ParameterFieldInfo.Add(fldDate)
        End With
    End Sub

    Public Sub LoadCashSummaryReport(ByVal ds As DataTable, ByVal DateFrom As Date, ByVal DateTo As Date, ByVal AccountCode As String, _
                                     ByVal AccountDescription As String, ByVal BeginningBalance As Decimal, ByVal SortedBy As String, _
                                     ByVal UserName As String, ByVal UserDate As Date)
        Dim rpt As New RPTCashSummaryReport

        Dim fldDateFrom As New ParameterField
        Dim fldDateTo As New ParameterField
        Dim fldAcctNo As New ParameterField
        Dim fldDescription As New ParameterField
        Dim fldBegBalance As New ParameterField
        Dim fldSortedBy As New ParameterField
        Dim fldUserName As New ParameterField
        Dim fldUserDate As New ParameterField

        Dim valDateFrom As New ParameterDiscreteValue
        Dim valDateTo As New ParameterDiscreteValue
        Dim valAcctNo As New ParameterDiscreteValue
        Dim valDescription As New ParameterDiscreteValue
        Dim valBegBalance As New ParameterDiscreteValue
        Dim valSortedBy As New ParameterDiscreteValue
        Dim valUserName As New ParameterDiscreteValue
        Dim valUserDate As New ParameterDiscreteValue

        fldDateFrom.Name = "paramDateFrom"
        fldDateTo.Name = "paramDateTo"
        fldAcctNo.Name = "paramAcctNo"
        fldDescription.Name = "paramDescription"
        fldBegBalance.Name = "paramBegBalance"
        fldSortedBy.Name = "paramSortedBy"
        fldUserName.Name = "paramUserName"
        fldUserDate.Name = "paramUserDate"

        valDateFrom.Value = DateFrom
        valDateTo.Value = DateTo
        valAcctNo.Value = AccountCode
        valDescription.Value = AccountDescription
        valBegBalance.Value = BeginningBalance
        valSortedBy.Value = SortedBy
        valUserName.Value = UserName
        valUserDate.Value = UserDate

        fldDateFrom.CurrentValues.Add(valDateFrom)
        fldDateTo.CurrentValues.Add(valDateTo)
        fldAcctNo.CurrentValues.Add(valAcctNo)
        fldDescription.CurrentValues.Add(valDescription)
        fldBegBalance.CurrentValues.Add(valBegBalance)
        fldSortedBy.CurrentValues.Add(valSortedBy)
        fldUserDate.CurrentValues.Add(valUserDate)
        fldUserName.CurrentValues.Add(valUserName)

        fldDateFrom.HasCurrentValue = True
        fldDateTo.HasCurrentValue = True
        fldAcctNo.HasCurrentValue = True
        fldDescription.HasCurrentValue = True
        fldBegBalance.HasCurrentValue = True
        fldSortedBy.HasCurrentValue = True
        fldUserDate.HasCurrentValue = True
        fldUserName.HasCurrentValue = True

        rpt.SetDataSource(ds)
        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(fldDateFrom)
            .ParameterFieldInfo.Add(fldDateTo)
            .ParameterFieldInfo.Add(fldAcctNo)
            .ParameterFieldInfo.Add(fldDescription)
            .ParameterFieldInfo.Add(fldBegBalance)
            .ParameterFieldInfo.Add(fldSortedBy)
            .ParameterFieldInfo.Add(fldUserDate)
            .ParameterFieldInfo.Add(fldUserName)
        End With

    End Sub

    Public Sub LoadPaymentORSummary(ByVal datasource As DataTable, ByVal dateFrom As Date, ByVal dateTo As Date)
        Me.FormTitle = "Payment OR Summary"
        Dim rpt As New RPTPaymentORSummary

        Dim fDateFrom As New ParameterDiscreteValue
        Dim fDateTo As New ParameterDiscreteValue

        Dim paramDateFrom As New ParameterField
        Dim paramDateTo As New ParameterField

        paramDateFrom.Name = "DateFrom"
        paramDateTo.Name = "DateTo"

        fDateFrom.Value = dateFrom
        fDateTo.Value = dateTo

        paramDateFrom.CurrentValues.Add(fDateFrom)
        paramDateTo.CurrentValues.Add(fDateTo)

        paramDateFrom.HasCurrentValue = True
        paramDateTo.HasCurrentValue = True

        rpt.SetDataSource(datasource)
        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(paramDateFrom)
            .ParameterFieldInfo.Add(paramDateTo)
        End With

    End Sub

    Public Sub LoadStatementOfAccount(datasource As DataSet)
        Me.FormTitle = "Statement Of Account"
        ' Dim rpt As New CrystalReport1

        Dim fCompanyBDOAcc As New ParameterDiscreteValue
        Dim fCompanyFullName As New ParameterDiscreteValue
        Dim fCompanyAccNum As New ParameterDiscreteValue        

        Dim paramCompanyBDOAcc As New ParameterField
        Dim paramCompanyFullName As New ParameterField
        Dim paramCompanyAccNum As New ParameterField        

        Dim rpt As New RPTCollectionNotice

        Dim paramBIRDateIssuedVal As New ParameterDiscreteValue
        Dim paramBIRDateIssuedField As New ParameterField

        Dim paramBIRValidUntilVal As New ParameterDiscreteValue
        Dim paramBIRValidUntilField As New ParameterField

        Dim paramSeriesNoVal As New ParameterDiscreteValue
        Dim paramSeriesNoField As New ParameterField


        Dim paramBIRPermitNoVal As New ParameterDiscreteValue
        Dim paramBIRPermitNoField As New ParameterField


        paramBIRDateIssuedField.Name = "paramBIRDateIssued"
        paramBIRDateIssuedVal.Value = AMModule.BIRDateIssued

        paramBIRDateIssuedField.CurrentValues.Add(paramBIRDateIssuedVal)
        paramBIRDateIssuedField.HasCurrentValue = True

        paramBIRValidUntilField.Name = "paramBIRValidUntil"
        paramBIRValidUntilVal.Value = AMModule.BIRValidUntil

        paramBIRValidUntilField.CurrentValues.Add(paramBIRValidUntilVal)
        paramBIRValidUntilField.HasCurrentValue = True

        paramSeriesNoField.Name = "paramSeriesNo"
        paramSeriesNoVal.Value = AMModule.SOANumberPrefix.ToString

        paramSeriesNoField.CurrentValues.Add(paramSeriesNoVal)
        paramSeriesNoField.HasCurrentValue = True


        paramBIRPermitNoField.Name = "paramBIRPermit"
        paramBIRPermitNoVal.Value = AMModule.BIRPermitNumber.ToString

        paramBIRPermitNoField.CurrentValues.Add(paramBIRPermitNoVal)
        paramBIRPermitNoField.HasCurrentValue = True

        fCompanyBDOAcc.Value = AMModule.CompanyBDOAccountName.ToString
        fCompanyFullName.Value = AMModule.CompanyFullName.ToString
        fCompanyAccNum.Value = AMModule.CompanyAccountNumber.ToString

        paramCompanyBDOAcc.Name = "paramCompanyBDOAcc"
        paramCompanyFullName.Name = "paramCompanyFullName"
        paramCompanyAccNum.Name = "paramCompanyAccNum"

        paramCompanyBDOAcc.CurrentValues.Add(fCompanyBDOAcc)
        paramCompanyFullName.CurrentValues.Add(fCompanyFullName)
        paramCompanyAccNum.CurrentValues.Add(fCompanyAccNum)

        paramCompanyBDOAcc.HasCurrentValue = True
        paramCompanyFullName.HasCurrentValue = True
        paramCompanyAccNum.HasCurrentValue = True

        rpt.SetDataSource(datasource)
        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(paramCompanyBDOAcc)
            .ParameterFieldInfo.Add(paramCompanyFullName)
            .ParameterFieldInfo.Add(paramCompanyAccNum)
            .ParameterFieldInfo.Add(paramBIRDateIssuedField)
            .ParameterFieldInfo.Add(paramBIRValidUntilField)
            .ParameterFieldInfo.Add(paramSeriesNoField)
            .ParameterFieldInfo.Add(paramBIRPermitNoField)
        End With
    End Sub

    Public Sub LoadDeferredMonitoringReport(ByVal datasource As DataTable)
        Me.FormTitle = "Deferred Payment"
        Dim rpt As New RPTDeferredMonitoring

        rpt.SetDataSource(datasource)
        With Me.RPTViewer
            .ReportSource = rpt

        End With
    End Sub

    Public Sub LoadGLCashInBankSettlement(ByVal datasource As DataTable, AccountNumber As String, AccountName As String, BeginningBalance As Decimal)
        Dim fAccountNumber As New ParameterDiscreteValue
        Dim fAccountName As New ParameterDiscreteValue
        Dim fBeginningBalanceValue As New ParameterDiscreteValue
        Dim paramAccountNumber As New ParameterField
        Dim paramAccountName As New ParameterField
        Dim paramBeginningBalance As New ParameterField

        Me.FormTitle = "General Ledger Cash In Bank Settlement"
        Dim rpt As New RPTGLCashInBankSettlement

        fAccountNumber.Value = AccountNumber
        fAccountName.Value = AccountName
        fBeginningBalanceValue.Value = BeginningBalance

        paramAccountNumber.Name = "ParamAccountNumber"
        paramAccountName.Name = "ParamAccountName"
        paramBeginningBalance.Name = "ParamBeginningBalance"

        paramAccountNumber.CurrentValues.Add(fAccountNumber)
        paramAccountName.CurrentValues.Add(fAccountName)
        paramBeginningBalance.CurrentValues.Add(fBeginningBalanceValue)

        paramAccountNumber.HasCurrentValue = True
        paramAccountName.HasCurrentValue = True
        paramBeginningBalance.HasCurrentValue = True

        rpt.SetDataSource(datasource)
        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(paramAccountNumber)
            .ParameterFieldInfo.Add(paramAccountName)
            .ParameterFieldInfo.Add(paramBeginningBalance)
        End With


    End Sub

    Public Sub LoadGLCashInBankPrudential(ByVal datasource As DataTable, AccountNumber As String, AccountName As String, BeginningBalance As Decimal)
        Dim fAccountNumber As New ParameterDiscreteValue
        Dim fAccountName As New ParameterDiscreteValue
        Dim fBeginningBalanceValue As New ParameterDiscreteValue
        Dim paramAccountNumber As New ParameterField
        Dim paramAccountName As New ParameterField
        Dim paramBeginningBalance As New ParameterField

        Me.FormTitle = "General Ledger Cash In Bank Prudential"
        Dim rpt As New RPTGLCashInBankPrudential

        fAccountNumber.Value = AccountNumber
        fAccountName.Value = AccountName
        fBeginningBalanceValue.Value = BeginningBalance

        paramAccountNumber.Name = "ParamAccountNumber"
        paramAccountName.Name = "ParamAccountName"
        paramBeginningBalance.Name = "ParamBeginningBalance"

        paramAccountNumber.CurrentValues.Add(fAccountNumber)
        paramAccountName.CurrentValues.Add(fAccountName)
        paramBeginningBalance.CurrentValues.Add(fBeginningBalanceValue)

        paramAccountNumber.HasCurrentValue = True
        paramAccountName.HasCurrentValue = True
        paramBeginningBalance.HasCurrentValue = True

        rpt.SetDataSource(datasource)
        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(paramAccountNumber)
            .ParameterFieldInfo.Add(paramAccountName)
            .ParameterFieldInfo.Add(paramBeginningBalance)
        End With


    End Sub

    Public Sub LoadBIRCollectionReport(ByVal dt As DataTable, ByVal colReportType As String)
        If colReportType.Equals("DAILY") Then
            Me.FormTitle = "BIR Collection Report (Daily)"
            Dim rpt As New RPTBIRCollectionReportSummary
            rpt.SetDataSource(dt)
            With Me.RPTViewer
                .ReportSource = rpt
            End With
        Else
            Me.FormTitle = "BIR Collection Report (Monthly)"
            Dim rpt As New RPTBIRCollectionReportSummaryMonthly
            rpt.SetDataSource(dt)
            With Me.RPTViewer
                .ReportSource = rpt
            End With
        End If

    End Sub

    Public Sub LoadGLInterestPayableSettlement(ByVal datasource As DataTable, AccountNumber As String, AccountName As String, BeginningBalance As Decimal)
        Dim fAccountNumber As New ParameterDiscreteValue
        Dim fAccountName As New ParameterDiscreteValue
        Dim fBeginningBalanceValue As New ParameterDiscreteValue
        Dim paramAccountNumber As New ParameterField
        Dim paramAccountName As New ParameterField
        Dim paramBeginningBalance As New ParameterField

        Me.FormTitle = "General Ledger Interest Payable Settlement"
        Dim rpt As New RPTGLInterestPayableSettlement

        fAccountNumber.Value = AccountNumber
        fAccountName.Value = AccountName
        fBeginningBalanceValue.Value = BeginningBalance

        paramAccountNumber.Name = "ParamAccountNumber"
        paramAccountName.Name = "ParamAccountName"
        paramBeginningBalance.Name = "ParamBeginningBalance"

        paramAccountNumber.CurrentValues.Add(fAccountNumber)
        paramAccountName.CurrentValues.Add(fAccountName)
        paramBeginningBalance.CurrentValues.Add(fBeginningBalanceValue)

        paramAccountNumber.HasCurrentValue = True
        paramAccountName.HasCurrentValue = True
        paramBeginningBalance.HasCurrentValue = True

        rpt.SetDataSource(datasource)
        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(paramAccountNumber)
            .ParameterFieldInfo.Add(paramAccountName)
            .ParameterFieldInfo.Add(paramBeginningBalance)
        End With

    End Sub

    Public Sub LoadGLInterestPayablePrudential(ByVal datasource As DataTable, AccountNumber As String, AccountName As String, BeginningBalance As Decimal)
        Dim fAccountNumber As New ParameterDiscreteValue
        Dim fAccountName As New ParameterDiscreteValue
        Dim fBeginningBalanceValue As New ParameterDiscreteValue
        Dim paramAccountNumber As New ParameterField
        Dim paramAccountName As New ParameterField
        Dim paramBeginningBalance As New ParameterField

        Me.FormTitle = "General Ledger Interest Payable Prudential"
        Dim rpt As New RPTGLInterestPayablePrudential

        fAccountNumber.Value = AccountNumber
        fAccountName.Value = AccountName
        fBeginningBalanceValue.Value = BeginningBalance

        paramAccountNumber.Name = "ParamAccountNumber"
        paramAccountName.Name = "ParamAccountName"
        paramBeginningBalance.Name = "ParamBeginningBalance"

        paramAccountNumber.CurrentValues.Add(fAccountNumber)
        paramAccountName.CurrentValues.Add(fAccountName)
        paramBeginningBalance.CurrentValues.Add(fBeginningBalanceValue)

        paramAccountNumber.HasCurrentValue = True
        paramAccountName.HasCurrentValue = True
        paramBeginningBalance.HasCurrentValue = True

        rpt.SetDataSource(datasource)
        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(paramAccountNumber)
            .ParameterFieldInfo.Add(paramAccountName)
            .ParameterFieldInfo.Add(paramBeginningBalance)
        End With


    End Sub

    Public Sub LoadGLPrudentialPerParticipant(ByVal datasource As DataTable)
        Me.FormTitle = "General Ledger Prudential Per Participant"
        Dim rpt As New RPTSLPrudentialPerParticipant

        rpt.SetDataSource(datasource)
        With Me.RPTViewer
            .ReportSource = rpt
        End With

    End Sub

    Public Sub LoadSLAccountsReceivablePerParticipant(ByVal datasource As DataTable, TransactionDate As Date)
        Dim fTransactionDate As New ParameterDiscreteValue
        Dim paramTransactionDate As New ParameterField

        Me.FormTitle = "Subsidiary Ledger Accounts Receivable Per Participant"
        Dim rpt As New RPTSLAccountsReceivablePerParticipant

        fTransactionDate.Value = TransactionDate
        paramTransactionDate.Name = "paramTransactionDate"
        paramTransactionDate.CurrentValues.Add(fTransactionDate)
        paramTransactionDate.HasCurrentValue = True

        rpt.SetDataSource(datasource)
        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(paramTransactionDate)
        End With


    End Sub

    Public Sub LoadSLAccountsPayablePerParticipant(ByVal datasource As DataTable, TransactionDate As Date)
        Dim fTransactionDate As New ParameterDiscreteValue
        Dim paramTransactionDate As New ParameterField

        Me.FormTitle = "Subsidiary Ledger Accounts Payable Per Participant"
        Dim rpt As New RPTSLAccountsPayablePerParticipant

        fTransactionDate.Value = TransactionDate
        paramTransactionDate.Name = "paramTransactionDate"
        paramTransactionDate.CurrentValues.Add(fTransactionDate)
        paramTransactionDate.HasCurrentValue = True

        rpt.SetDataSource(datasource)
        With Me.RPTViewer
            .ReportSource = rpt
            .ParameterFieldInfo.Clear()
            .ParameterFieldInfo.Add(paramTransactionDate)
        End With

    End Sub

    Public Sub LoadSPAPaymentScheduleSummary(ByVal DS As DataSet)
        Me.FormTitle = "Payment Schedule Summary Report"
        Dim rpt As New SPASummaryReport

        rpt.SetDataSource(DS)
        With Me.RPTViewer
            .ReportSource = rpt
        End With
    End Sub

    Private Sub PrintingCounter(sender As System.Object, e As System.EventArgs)
        Dim printDialog As New PrintDialog()
        Dim printName As String = ""
        docNumber = dSource.AsEnumerable().Select(Function(x) x.Field(Of String)("OR_NO")).FirstOrDefault
        Dim docPrintingCount As Integer = wbHelper.GetPrintCount(docNumber)

        'If printDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
        '    printDialog.PrinterSettings.Copies = 2
        '    oRPT.PrintOptions.PrinterName = printDialog.PrinterSettings.PrinterName
        '    oRPT.PrintToPrinter(printDialog.PrinterSettings.Copies, printDialog.PrinterSettings.Collate, printDialog.PrinterSettings.FromPage, printDialog.PrinterSettings.ToPage)
        '    Me.PrintRPT(docPrintingCount)
        '    Me.Close()
        'End If

        If frmListOfPrinter.ShowDialog() = Windows.Forms.DialogResult.OK Then
            printName = CStr(frmListOfPrinter.cbo_PrinterList.SelectedValue)

            oRPT.PrintOptions.PrinterName = printName
            oRPT.PrintToPrinter(2, False, 0, 0)

            Me.PrintRPT(docPrintingCount)
            Me.Close()
        End If
        
    End Sub

    Private Sub PrintRPT(ByVal docPrintcounter As Integer)
        If docPrintcounter = 0 Then
            wbHelper.SaveDocPrintingCounter(docNumber)
        Else
            wbHelper.UpdateDocPrintingCounter(docNumber)
        End If
    End Sub

End Class

