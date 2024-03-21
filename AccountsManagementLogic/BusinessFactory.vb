'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             BusinessFactory
'Orginal Author:         Vladimir E.Espiritu
'File Creation Date:     February 03, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for re-usable functions
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description
'   February 03, 2012       Vladimir E. Espiritu         Class initialization
'   November 22, 2012       Vladimir E. Espiritu         Added GeneratePrudentialReport function
'   January 28,  2013       Vladimir E. Espiritu         Added GenerateDMCMReport function

Option Explicit On
Option Strict On

Imports System.IO
Imports AccountsManagementObjects
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Configuration
Imports System.Windows.Forms

Public Class BusinessFactory
    Private Enum EnumORRemarks
        Energy
        VATonEnergy
        MarketFees
        VATonMarketFees
        ExcessCollection
        PrudentialReplenishment
        HeldCollection
    End Enum

#Region "Single Instance Code"
    Private Shared m_Instance As BusinessFactory = Nothing
    Public Shared Function GetInstance() As BusinessFactory
        If m_Instance Is Nothing Then
            m_Instance = New BusinessFactory()
        End If
        Return m_Instance
    End Function

#End Region

#Region "Initialization/Constructor"
    Public Sub New()

    End Sub
#End Region

#Region "Compute For Allocation"
    'Percentage of participant's allocation to total amount
    Public Function ComputeAllocation(ByVal ParticipantAP As Decimal, ByVal OutstandingAP As Decimal, ByVal AmountToAllocate As Decimal) As Decimal
        Dim retAllocation As Decimal

        If OutstandingAP = 0 Then
            retAllocation = 0
        Else
            retAllocation = (ParticipantAP / OutstandingAP) * AmountToAllocate
        End If

        Return retAllocation
    End Function
#End Region

#Region "Compute Default Interest"
    Public Function ComputeDefaultInterest(ByVal DueDate As Date, ByVal NewDueDate As Date, ByVal CollectionDate As Date, _
                                           ByVal EndingBalance As Decimal, ByVal InterestRate As Decimal) As Decimal
        Dim DefaultInterest As Decimal = 0D
        Dim outstandingDays As Long = 0

        If CollectionDate < NewDueDate Then
            Return DefaultInterest
        End If

        If DueDate = NewDueDate Then
            If NewDueDate < CollectionDate Then
                outstandingDays = DateDiff(DateInterval.Day, NewDueDate, CollectionDate) + 1
            End If
        Else
            outstandingDays = DateDiff(DateInterval.Day, NewDueDate, CollectionDate)
        End If

        If outstandingDays > 0 Then
            DefaultInterest = Math.Round(EndingBalance * InterestRate * _
                              CDec(outstandingDays / AMModule.DefaultInterestDivisorValue), 2)
        End If

        Return DefaultInterest
    End Function
#End Region

#Region "CheckPrecisionAndScale"
    Public Function CheckPrecisionAndScale(ByVal WholeNumber As Integer, ByVal DecimalPlaces As Integer, _
                                                ByVal value As String) As Boolean
        Dim splitValues() As String
        Dim index = InStr(value, ".")
        splitValues = Split(value, ".")

        If index = 0 Then
            If value.Length > WholeNumber Then
                Exit Function
            End If
        Else
            If splitValues(0).Length > WholeNumber Or splitValues(1).Length > DecimalPlaces Then
                Exit Function
            End If
        End If

        Return True
    End Function
#End Region

#Region "CloneObject"
    Public Function CloneObject(ByVal obj As Object) As Object
        ' Create a memory stream and a formatter.
        Dim ms As New MemoryStream(1000)
        Dim bf As New BinaryFormatter()
        ' Serialize the object into the stream.
        bf.Serialize(ms, obj)
        ' Position streem pointer back to first byte.
        ms.Seek(0, SeekOrigin.Begin)
        ' Deserialize into another object.
        CloneObject = bf.Deserialize(ms)
        ' Release memory.
        ms.Close()
    End Function
#End Region

#Region "Convert Numbers to Words"
    Public Function NumberConvert(ByVal number As Decimal) As String
        Dim nDecim As Decimal
        Dim leadZero As String
        Dim wOne As String = ""
        Dim wTen As String = ""

        Dim wFix As String = ""

        Dim _teen As String = ""
        Dim ctr As Integer = 1
        Dim Multi As Decimal = 1
        Dim _wNum As String = ""
        Dim _pNum As Double = 0
        Dim _nNum As Double = 0
        Dim ctr2 As Integer = 1
        If number > 999999999999999 Then
            Throw New ApplicationException("Invalid Amount")
        End If
        'Try

        number = CDec(FormatNumber(number, 2))
        Dim nCount As Double
        If ctr = 1 Then
            nDecim = (CDec(FormatNumber(number Mod 1, 2))) * 100
            If nDecim <> 0 Then
                leadZero = nDecim.ToString("00")
                If number - nDecim = 0 Then
                    _wNum += leadZero & "/100"
                Else
                    _wNum += "and " & leadZero & "/100"
                End If

                If number < 1 Then
                    _wNum = leadZero & "/100"
                End If

                number = number - nDecim / 100
            Else
                _wNum += "Pesos Only"
            End If
        End If
        Multi = 10
        While ctr2 <= Len(CStr(number)) + 1
            Select Case (((number Mod Multi) - nCount) / Multi * 10)
                Case 1
                    If ctr = 1 Or ctr = 3 Then
                        wOne = "One"
                        If ctr <> 3 Then
                            _pNum = (((number Mod Multi) - nCount) / Multi * 10)
                        End If
                    Else
                        wTen = "Ten"
                        _nNum = (((number Mod Multi) - nCount) / Multi * 10)
                    End If
                Case 2
                    If ctr = 1 Or ctr = 3 Then
                        wOne = "Two"
                        If ctr <> 3 Then
                            _pNum = (((number Mod Multi) - nCount) / Multi * 10)

                        End If
                    Else
                        wTen = "Twenty"
                        _nNum = (((number Mod Multi) - nCount) / Multi * 10)
                    End If
                Case 3
                    If ctr = 1 Or ctr = 3 Then
                        wOne = "Three"
                        If ctr <> 3 Then
                            _pNum = (((number Mod Multi) - nCount) / Multi * 10)

                        End If
                    Else
                        wTen = "Thirty"
                        _nNum = (((number Mod Multi) - nCount) / Multi * 10)
                    End If
                Case 4
                    If ctr = 1 Or ctr = 3 Then
                        wOne = "Four"
                        If ctr <> 3 Then
                            _pNum = (((number Mod Multi) - nCount) / Multi * 10)

                        End If
                    Else
                        wTen = "Forty"
                        _nNum = (((number Mod Multi) - nCount) / Multi * 10)
                    End If
                Case 5
                    If ctr = 1 Or ctr = 3 Then
                        wOne = "Five"
                        If ctr <> 3 Then
                            _pNum = (((number Mod Multi) - nCount) / Multi * 10)

                        End If
                    Else
                        wTen = "Fifty"
                        _nNum = (((number Mod Multi) - nCount) / Multi * 10)
                    End If
                Case 6
                    If ctr = 1 Or ctr = 3 Then
                        wOne = "Six"
                        If ctr <> 3 Then
                            _pNum = (((number Mod Multi) - nCount) / Multi * 10)

                        End If
                    Else
                        wTen = "Sixty"
                        _nNum = (((number Mod Multi) - nCount) / Multi * 10)
                    End If
                Case 7
                    If ctr = 1 Or ctr = 3 Then
                        wOne = "Seven"
                        If ctr <> 3 Then
                            _pNum = (((number Mod Multi) - nCount) / Multi * 10)
                        End If
                    Else
                        wTen = "Seventy"
                        _nNum = (((number Mod Multi) - nCount) / Multi * 10)
                    End If
                Case 8
                    If ctr = 1 Or ctr = 3 Then
                        wOne = "Eight"
                        If ctr <> 3 Then
                            _pNum = (((number Mod Multi) - nCount) / Multi * 10)
                        End If
                    Else
                        wTen = "Eighty"
                        _nNum = (((number Mod Multi) - nCount) / Multi * 10)
                    End If
                Case 9
                    If ctr = 1 Or ctr = 3 Then
                        wOne = "Nine"
                        If ctr <> 3 Then
                            _pNum = (((number Mod Multi) - nCount) / Multi * 10)
                        End If
                    Else
                        wTen = "Ninety"
                        _nNum = (((number Mod Multi) - nCount) / Multi * 10)
                    End If
            End Select

            If _nNum = 1 Then
                Select Case _pNum
                    Case 1
                        _teen = "Eleven"
                    Case 2
                        _teen = "Twelve"
                    Case 3
                        _teen = "Thirteen"
                    Case 4
                        _teen = "Fourteen"
                    Case 5
                        _teen = "Fifteen"
                    Case 6
                        _teen = "Sixteen"
                    Case 7
                        _teen = "Seventeen"
                    Case 8
                        _teen = "Eighteen"
                    Case 9
                        _teen = "Nineteen"
                End Select
                _nNum = 0
                _pNum = 0
            End If

            Select Case Multi
                Case 10
                    'Ones
                Case 100
                    'Tens
                Case 1000
                    wFix = "Hundred"
                Case 10000
                    wFix = "Thousand"
                Case 1000000
                    wFix = "Hundred"
                Case 10000000
                    wFix = "Million"
                Case 1000000000
                    wFix = "Hundred"
                Case 10000000000
                    wFix = "Billion"
                Case 1000000000000
                    wFix = "Hundred"
                Case 10000000000000
                    wFix = "Trillion"
                Case 1000000000000000
                    wFix = "Hundred"
            End Select


            If ctr = 3 Then
                If wOne <> "" Then
                    _wNum = Trim(wTen) & " " & Trim(wOne) & " " & Trim(wFix) & " " & Trim(_wNum)
                    wTen = ""
                    wOne = ""
                    wFix = ""
                Else
                    _wNum = Trim(wTen) & " " & Trim(wOne) & " " & Trim(_wNum)
                    wTen = ""
                    wOne = ""
                End If
                ctr = 0
            ElseIf ctr = 2 Then
                If _teen <> "" Then
                    _wNum = Trim(_teen) & " " & Trim(wFix) & " " & Trim(_wNum)
                    wTen = ""
                    wFix = ""
                    wOne = ""
                    _teen = ""
                Else
                    If wTen <> "" Or wOne <> "" Then
                        _wNum = Trim(wTen) & " " & Trim(wOne) & " " & Trim(wFix) & " " & Trim(_wNum)
                    Else
                        _wNum = Trim(wTen) & " " & Trim(wOne) & " " & Trim(_wNum)
                    End If
                    If (((number Mod Multi * 10) - nCount) / Multi * 100) <> 0 Then
                        If wTen = "" And wOne = "" Then
                            _wNum = Trim(wFix) & " " & Trim(_wNum)
                        End If
                    End If
                    wOne = ""
                    wTen = ""
                    wFix = ""
                    _teen = ""
                End If
            End If

            nCount += (number Mod Multi) - nCount
            Multi *= 10
            ctr2 += 1
            ctr += 1
        End While
        'Catch ex As Exception
        ' Throw New ApplicationException(ex.Message)
        'End Try

        Return Trim(_wNum)
        'return this
        'MsgBox(Replace(Replace(_wNum, "  ", " "), "  ", " "), , "Number to words Conversion")

    End Function
#End Region

#Region "Create DMCM"

    Public Function DMCMMFWTaxWVAT(ByVal DMCMCtr As Long, ByVal MFSummary As WESMBillSummary, ByVal Particulars As String, _
                                   ByVal WHTaxAmount As Decimal, Optional ByVal WHVatAmount As Decimal = 0) As DebitCreditMemo
        Dim retDMCM As New DebitCreditMemo
        With retDMCM
            .BillingPeriod = MFSummary.BillPeriod
            .DueDate = MFSummary.DueDate
            .ChargeType = MFSummary.ChargeType
            .DMCMNumber = DMCMCtr
            .Particulars = Particulars


            .DMCMDetails.Add(New DebitCreditMemoDetails(DMCMCtr, EWTReceivable, WHTaxAmount, 0))


            .DMCMDetails.Add(New DebitCreditMemoDetails(DMCMCtr, EWVReceivableCode, 0, WHVatAmount))
            .DMCMDetails.Add(New DebitCreditMemoDetails(DMCMCtr, DebitCode, 0, WHTaxAmount + WHVatAmount))


        End With
        Return retDMCM
    End Function
#End Region

#Region "Parse String"
    Public Function ParseString(ByVal number As Decimal, ByVal parseValue As List(Of Integer), _
                                ByVal IsCompleteString As Boolean) As List(Of String)
        Dim result As New List(Of String)
        Dim stringLength As Integer = 0
        Dim index As Integer = 1

        Dim value = Me.NumberConvert(number)

        For Each item In parseValue
            If index > value.Length - 1 Then
                Return result
                Exit Function
            End If

            stringLength += item
            Dim finalString As String = ""

            If IsCompleteString Then
                finalString = Mid(value, index, item)
                Dim tmpString = Mid(value, index, item + 1)

                While finalString.Substring(finalString.Length - 1, 1) <> " " _
                    And tmpString.Substring(tmpString.Length - 1, 1) <> " " _
                    And stringLength <= value.Length

                    item -= 1
                    finalString = Mid(value, index, item)
                End While
            Else
                finalString = Mid(value, index, item)
            End If

            index += item

            result.Add(finalString)
        Next

        Return result
    End Function
#End Region

#Region "GenerateCollectionDMCMDrawdown"
    Public Function GenerateDMCMDrawdown(ByRef listCollections As List(Of Collection), ByVal SignatoriesDMCM As DocSignatories, _
                                         ByVal dicDailyInterest As Dictionary(Of Date, Decimal)) As List(Of DebitCreditMemo)
        Dim result As New List(Of DebitCreditMemo)


        For Each item In listCollections
            If item.CollectionCategory = EnumCollectionCategory.Drawdown Then

                'Get the daily interest for the selected collection
                Dim dailyInterest = dicDailyInterest(item.CollectionDate)

                'Create the details
                Dim details As New List(Of DebitCreditMemoDetails)
                details.Add(New DebitCreditMemoDetails(item.DMCMNumber, AMModule.PRWESMCode, Math.Abs(item.CollectedAmount), _
                                                       0, "0", EnumSummaryType.INV, New AMParticipants(item.IDNumber)))

                Dim Others As Decimal = 0
                For Each itemAlloc In item.ListOfCollectionAllocation
                    If itemAlloc.CollectionType = EnumCollectionType.Energy Then
                        'For Energy
                        details.Add(New DebitCreditMemoDetails(item.DMCMNumber, AMModule.CreditCode, 0, Math.Abs(itemAlloc.Amount), _
                                                               itemAlloc.WESMBillSummaryNo.INVDMCMNo, itemAlloc.WESMBillSummaryNo.SummaryType, _
                                                               New AMParticipants(item.IDNumber), EnumDMCMComputed.Compute))
                    Else
                        'For Default Interest on Energy
                        Others += Math.Abs(itemAlloc.Amount)
                        details.Add(New DebitCreditMemoDetails(item.DMCMNumber, AMModule.CreditCode, 0, Math.Abs(itemAlloc.Amount), _
                                                               itemAlloc.WESMBillSummaryNo.INVDMCMNo, itemAlloc.WESMBillSummaryNo.SummaryType, _
                                                               New AMParticipants(item.IDNumber), EnumDMCMComputed.NotCompute))
                    End If
                Next

                Dim itemDMCM As New DebitCreditMemo
                With itemDMCM
                    .DMCMNumber = item.DMCMNumber
                    .Particulars = "Crediting your account through drawdown of prudential security in payment of accounts."
                    .IDNumber = item.IDNumber
                    .ChargeType = EnumChargeType.E
                    .TransType = EnumDMCMTransactionType.CollectionDrawdown
                    .Others = Others
                    .TotalAmountDue = Math.Abs(item.CollectedAmount)
                    .CheckedBy = SignatoriesDMCM.Signatory_1
                    .ApprovedBy = SignatoriesDMCM.Signatory_2
                    .DMCMDetails = details
                End With

                result.Add(itemDMCM)
                result.TrimExcess()
            End If
        Next

        Return result
    End Function
#End Region

#Region "GenerateCollectionDMCM"
    Public Function GenerateCollectionDMCM(ByRef listCollections As List(Of Collection), ByVal SignatoriesDMCM As DocSignatories, _
                                           ByVal dicDailyInterest As Dictionary(Of Date, Decimal)) As List(Of DebitCreditMemo)
        Dim result As New List(Of DebitCreditMemo)
        Dim DMCMNumber As Long = 0

        For Each item In listCollections
            'Get the daily interest for the selected collection
            Dim dailyInterest = dicDailyInterest(item.CollectionDate)


            Dim listWithhold = (From x In item.ListOfCollectionAllocation _
                                Where x.CollectionType = EnumCollectionType.WithholdingTaxOnMF _
                                Or x.CollectionType = EnumCollectionType.WithholdingVatonMF _
                                Select x).ToList()

            'DMCM for Withholding
            If listWithhold.Count > 0 Then
                DMCMNumber += 1
                Dim Total As Decimal = 0, EWT As Decimal = 0, EWV As Decimal = 0
                Dim details As New List(Of DebitCreditMemoDetails)


                For Each itemWithhold In listWithhold
                    If itemWithhold.CollectionType = EnumCollectionType.WithholdingTaxOnMF Then
                        details.Add(New DebitCreditMemoDetails(DMCMNumber, AMModule.EWTReceivable, Math.Abs(itemWithhold.Amount), _
                                                       0, itemWithhold.WESMBillSummaryNo.INVDMCMNo, itemWithhold.WESMBillSummaryNo.SummaryType, _
                                                       New AMParticipants(item.IDNumber)))
                        EWT += Math.Abs(itemWithhold.Amount)
                    Else
                        details.Add(New DebitCreditMemoDetails(DMCMNumber, AMModule.EWVReceivableCode, Math.Abs(itemWithhold.Amount), _
                                                       0, itemWithhold.WESMBillSummaryNo.INVDMCMNo, itemWithhold.WESMBillSummaryNo.SummaryType, _
                                                       New AMParticipants(item.IDNumber)))
                        EWV += Math.Abs(itemWithhold.Amount)
                    End If
                    Total += Math.Abs(itemWithhold.Amount)
                Next

                'Create the AR-WESM
                details.Add(New DebitCreditMemoDetails(DMCMNumber, AMModule.CreditCode, 0, Total, _
                                               "0", EnumSummaryType.INV, New AMParticipants(item.IDNumber)))

                Dim itemDMCM As New DebitCreditMemo
                With itemDMCM
                    .DMCMNumber = DMCMNumber

                    If EWT <> 0 Then
                        .Particulars = "Crediting your account for Withholding Tax "
                    End If

                    If EWV <> 0 Then
                        If .Particulars.Length <> 0 Then
                            .Particulars &= "and Withholding Vat "
                        Else
                            .Particulars = "Crediting your account for Withholding Vat "
                        End If
                    End If

                    .Particulars &= "on Market Fees "

                    If item.ORNo <> 0 Then
                        .Particulars &= "per OR Number " & Me.GenerateBIRDocumentNumber(item.ORNo, BIRDocumentsType.OfficialReceipt) & "."
                    Else
                        .Particulars &= "."
                    End If

                    .IDNumber = item.IDNumber
                    .ChargeType = EnumChargeType.MF
                    .TransType = EnumDMCMTransactionType.CollectionSetupWithholding
                    .EWT = EWT
                    .EWV = EWV
                    .TotalAmountDue = Total
                    .CheckedBy = SignatoriesDMCM.Signatory_1
                    .ApprovedBy = SignatoriesDMCM.Signatory_2
                    .DMCMDetails = details
                End With

                result.Add(itemDMCM)
                result.TrimExcess()

                'Update the DMCM No of Collection Allocation
                For Each itemAlloc In listWithhold
                    itemAlloc.DMCMNumber = DMCMNumber
                Next
            End If

            Dim listDIEW = (From x In item.ListOfCollectionAllocation _
                            Where x.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest _
                            Or x.CollectionType = EnumCollectionType.WithholdingVatonDefaultInterest _
                            Select x).ToList()

            'DMCM for Default Interest on WHTax/WHVAT Entry
            If listDIEW.Count > 0 Then
                DMCMNumber += 1
                Dim Total As Decimal = 0, EWT As Decimal = 0, EWV As Decimal = 0
                Dim details As New List(Of DebitCreditMemoDetails)

                For Each itemDIMF In listDIEW
                    Select Case itemDIMF.CollectionType
                        Case EnumCollectionType.WithholdingTaxOnDefaultInterest
                            details.Add(New DebitCreditMemoDetails(DMCMNumber, AMModule.EWTReceivable, Math.Abs(itemDIMF.Amount), _
                                                                   0, itemDIMF.WESMBillSummaryNo.INVDMCMNo, itemDIMF.WESMBillSummaryNo.SummaryType, _
                                                                   New AMParticipants(item.IDNumber)))
                            EWT += Math.Abs(itemDIMF.Amount)

                        Case EnumCollectionType.WithholdingVatonDefaultInterest
                            details.Add(New DebitCreditMemoDetails(DMCMNumber, AMModule.EWVReceivableCode, Math.Abs(itemDIMF.Amount), _
                                                                   0, itemDIMF.WESMBillSummaryNo.INVDMCMNo, itemDIMF.WESMBillSummaryNo.SummaryType, _
                                                                   New AMParticipants(item.IDNumber)))
                            EWV += Math.Abs(itemDIMF.Amount)
                    End Select
                    Total += Math.Abs(itemDIMF.Amount)
                Next

                'Create the AR-WESM
                details.Add(New DebitCreditMemoDetails(DMCMNumber, AMModule.CreditCode, 0, Total, _
                                               "0", EnumSummaryType.INV, New AMParticipants(item.IDNumber)))

                Dim itemDMCM As New DebitCreditMemo
                With itemDMCM
                    .DMCMNumber = DMCMNumber

                    If EWT <> 0 Then
                        .Particulars = "Crediting your account for Withholding Tax "
                    End If

                    If EWV <> 0 Then
                        If .Particulars.Length <> 0 Then
                            .Particulars &= "and Withholding Vat "
                        Else
                            .Particulars = "Crediting your account for Withholding Vat "
                        End If
                    End If
                    .Particulars &= "on Default Interest on Market Fees "

                    If item.ORNo <> 0 Then
                        .Particulars &= "per OR Number " & Me.GenerateBIRDocumentNumber(item.ORNo, BIRDocumentsType.OfficialReceipt) & "."
                    Else
                        .Particulars &= "."
                    End If
                    .Particulars &= "(Interest rate is " & (dailyInterest * 100).ToString() & "%)."

                    .IDNumber = item.IDNumber
                    .ChargeType = EnumChargeType.MF
                    .TransType = EnumDMCMTransactionType.CollectionSetupWithholdingDefaultInterest
                    .EWT = EWT
                    .EWV = EWV
                    .TotalAmountDue = Total
                    .CheckedBy = SignatoriesDMCM.Signatory_1
                    .ApprovedBy = SignatoriesDMCM.Signatory_2
                    .DMCMDetails = details
                End With

                result.Add(itemDMCM)
                result.TrimExcess()

                'Update the DMCM No of Collection Allocation
                'If there is created DMCM for withholding tax, edit the reference number
                For Each itemAlloc In listDIEW
                    itemAlloc.DMCMNumber = DMCMNumber

                    'Update the DMCM No of Collection Allocation
                    If listWithhold.Count <> 0 Then
                        itemAlloc.ReferenceNumber = CStr(DMCMNumber - 1)
                        itemAlloc.ReferenceType = EnumSummaryType.DMCM
                        itemAlloc.IsDMCMChanged = 1
                    End If
                Next
            End If

            Dim listDIMF = (From x In item.ListOfCollectionAllocation _
                            Where x.CollectionType = EnumCollectionType.DefaultInterestOnMF _
                            Or x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF _
                            Select x).ToList()

            'DMCM for Default Interest on Withholding and MF/MFV
            If listDIMF.Count > 0 Then
                DMCMNumber += 1
                Dim Others As Decimal = 0
                Dim details As New List(Of DebitCreditMemoDetails)

                For Each itemDIMF In listDIMF
                    If itemDIMF.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF Then
                        Continue For
                    End If

                    Select Case itemDIMF.CollectionType
                        Case EnumCollectionType.DefaultInterestOnMF
                            details.Add(New DebitCreditMemoDetails(DMCMNumber, AMModule.DefaultInterestMarketFees, 0, Math.Abs(itemDIMF.Amount), _
                                                                   itemDIMF.WESMBillSummaryNo.INVDMCMNo, itemDIMF.WESMBillSummaryNo.SummaryType, _
                                                                   New AMParticipants(item.IDNumber)))
                            Others += Math.Abs(itemDIMF.Amount)

                        Case EnumCollectionType.DefaultInterestOnVatOnMF
                            details.Add(New DebitCreditMemoDetails(DMCMNumber, AMModule.MarketFeesOutputTaxCode, 0, Math.Abs(itemDIMF.Amount), _
                                                                   itemDIMF.WESMBillSummaryNo.INVDMCMNo, itemDIMF.WESMBillSummaryNo.SummaryType, _
                                                                   New AMParticipants(item.IDNumber)))
                            Others += Math.Abs(itemDIMF.Amount)
                    End Select
                Next

                'Create the AR-WESM
                details.Add(New DebitCreditMemoDetails(DMCMNumber, AMModule.CreditCode, Others, 0, _
                                               "0", EnumSummaryType.INV, New AMParticipants(item.IDNumber)))

                Dim itemDMCM As New DebitCreditMemo
                With itemDMCM
                    .DMCMNumber = DMCMNumber

                    If item.ORNo <> 0 Then
                        .Particulars = "Debiting your account for default interest on Market Fees " & _
                                       "including VAT per OR Number " & Me.GenerateBIRDocumentNumber(item.ORNo, BIRDocumentsType.OfficialReceipt) & _
                                       " (Interest rate is " & (dailyInterest * 100).ToString() & "%)."
                    Else
                        .Particulars = "Debiting your account for default interest on Market Fees " & _
                                       "including VAT per OR Number " & Me.GenerateBIRDocumentNumber(item.ORNo, BIRDocumentsType.OfficialReceipt) & _
                                       " (Interest rate is " & (dailyInterest * 100).ToString() & "%)."
                    End If

                    .IDNumber = item.IDNumber
                    .ChargeType = EnumChargeType.MF
                    .TransType = EnumDMCMTransactionType.CollectionSetupMFandMFVatDefaultInterest
                    .Others = Others
                    .TotalAmountDue = Others
                    .CheckedBy = SignatoriesDMCM.Signatory_1
                    .ApprovedBy = SignatoriesDMCM.Signatory_2
                    .DMCMDetails = details
                End With

                result.Add(itemDMCM)
                result.TrimExcess()

                'Update the DMCM No of Collection Allocation
                For Each itemAlloc In listDIMF
                    itemAlloc.DMCMNumber = DMCMNumber
                Next
            End If

            Dim listDIEnergy = (From x In item.ListOfCollectionAllocation _
                                Where x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy _
                                Select x).ToList()

            'DMCM for Default Interest on Energy
            If listDIEnergy.Count > 0 Then
                DMCMNumber += 1
                Dim Others As Decimal = 0
                Dim details As New List(Of DebitCreditMemoDetails)

                For Each itemDIEnergy In listDIEnergy
                    details.Add(New DebitCreditMemoDetails(DMCMNumber, AMModule.DefaultInterestCode, 0, Math.Abs(itemDIEnergy.Amount), _
                                                           itemDIEnergy.WESMBillSummaryNo.INVDMCMNo, itemDIEnergy.WESMBillSummaryNo.SummaryType, _
                                                           New AMParticipants(item.IDNumber)))
                    Others += Math.Abs(itemDIEnergy.Amount)
                Next

                'Create the AR-WESM
                details.Add(New DebitCreditMemoDetails(DMCMNumber, AMModule.CreditCode, Others, 0, _
                                               "0", EnumSummaryType.INV, New AMParticipants(item.IDNumber)))

                Dim itemDMCM As New DebitCreditMemo
                With itemDMCM
                    .DMCMNumber = DMCMNumber

                    If item.CollectionCategory = EnumCollectionCategory.Cash Then
                        If item.ORNo <> 0 Then
                            .Particulars = "Debiting your account for default interest on Energy " & _
                                           "per OR Number " & Me.GenerateBIRDocumentNumber(item.ORNo, BIRDocumentsType.OfficialReceipt) & _
                                           "(Interest rate is " & (dailyInterest * 100).ToString() & "%)."
                        Else
                            .Particulars = "Debiting your account for default interest on Energy " & _
                                           "(Interest rate is " & (dailyInterest * 100).ToString() & "%)."
                        End If
                    Else
                        .Particulars = "Debiting your account for default interest on Energy " & _
                                       "per DMCM Number " & Me.GenerateBIRDocumentNumber(item.DMCMNumber, BIRDocumentsType.DMCM) & _
                                       "(Interest rate is " & (dailyInterest * 100).ToString() & "%)."
                    End If

                    .IDNumber = item.IDNumber
                    .ChargeType = EnumChargeType.E
                    .TransType = EnumDMCMTransactionType.CollectionSetupEnergyDefaultInterest
                    .Others = Others
                    .TotalAmountDue = Others
                    .CheckedBy = SignatoriesDMCM.Signatory_1
                    .ApprovedBy = SignatoriesDMCM.Signatory_2
                    .DMCMDetails = details
                End With

                result.Add(itemDMCM)
                result.TrimExcess()

                'Update the DMCM No of Collection Allocati  on
                For Each itemAlloc In listDIEnergy
                    itemAlloc.DMCMNumber = DMCMNumber
                Next
            End If
        Next

        Return result
    End Function
#End Region

#Region "GenerateCollectionOR"
    Public Function GenerateCollectionOR(ByVal listCollections As List(Of Collection), _
                                         ByVal listMonitoring As List(Of CollectionMonitoring), _
                                         ByVal listSalesAndPurchases As List(Of WESMBillSalesAndPurchased), _
                                         ByVal ParticipantInfo As AMParticipants) _
                                         As List(Of OfficialReceiptMain)

        Dim result As New List(Of OfficialReceiptMain)
        Dim itemsCollections = (From x In listCollections _
                                Where x.CollectionCategory = EnumCollectionCategory.Cash And x.CollectedAmount <> 0 _
                                Select x).ToList()

        For Each itemCol In itemsCollections
            Dim selectedCol = itemCol

            Dim listORSummary As New List(Of OfficialReceiptSummary)
            Dim listORDetails As New List(Of OfficialReceiptDetails)
            Dim totalAR As Decimal = 0, totalPEMC As Decimal = 0
            Dim VatExempt As Decimal = 0, Vatable As Decimal = 0, VAT As Decimal = 0
            Dim VatZeroRated As Decimal = 0, Others As Decimal = 0, WithholdTax As Decimal = 0, WithholdVat As Decimal = 0

            Dim listMon = (From x In listMonitoring _
                           Where x.CollectionNo = selectedCol.CollectionNumber _
                           Select x).ToList()

            For Each itemMon In listMon
                If itemMon.Amount <> 0 Then
                    With itemMon
                        Select Case itemMon.TransType
                            Case EnumCollectionMonitoringType.TransferToPRReplenishment
                                listORSummary.Add(New OfficialReceiptSummary(.ORNo, 0, Nothing, Math.Abs(.Amount), EnumCollectionType.TransferToReplenishment))

                                listORDetails.Add(New OfficialReceiptDetails(.ORNo, AMModule.PRWESMCode, _
                                                                EnumCollectionMonitoringType.TransferToPRReplenishment.ToString(), _
                                                                0, Math.Abs(.Amount)))
                                totalAR += Math.Abs(.Amount)
                                VatExempt += Math.Abs(.Amount)

                            Case EnumCollectionMonitoringType.TransferToPEMCAccount
                                listORDetails.Add(New OfficialReceiptDetails(.ORNo, AMModule.UnappliedcollectionCode, _
                                                                EnumCollectionMonitoringType.TransferToPEMCAccount.ToString(), _
                                                                0, Math.Abs(.Amount)))
                                totalPEMC = Math.Abs(.Amount)
                                totalAR += Math.Abs(.Amount)

                            Case EnumCollectionMonitoringType.TransferToExcessCollection
                                listORSummary.Add(New OfficialReceiptSummary(.ORNo, 0, Nothing, Math.Abs(.Amount), EnumCollectionType.TransferToExcessCollection))

                                listORDetails.Add(New OfficialReceiptDetails(.ORNo, AMModule.UnappliedcollectionCode, _
                                                               EnumCollectionMonitoringType.TransferToExcessCollection.ToString(), _
                                                               0, Math.Abs(.Amount)))
                                totalAR += Math.Abs(.Amount)
                                Others += Math.Abs(.Amount)

                            Case EnumCollectionMonitoringType.TransferToHeldCollection
                                listORSummary.Add(New OfficialReceiptSummary(.ORNo, 0, Nothing, Math.Abs(.Amount), EnumCollectionType.TransferToHeldCollection))

                                listORDetails.Add(New OfficialReceiptDetails(.ORNo, AMModule.UnappliedcollectionCode, _
                                                               EnumCollectionMonitoringType.TransferToHeldCollection.ToString(), _
                                                               0, Math.Abs(.Amount)))
                                totalAR += Math.Abs(.Amount)
                                Others += Math.Abs(.Amount)
                        End Select
                    End With
                End If
            Next

            For Each item In itemCol.ListOfCollectionAllocation
                Dim selectedItem = item

                If item.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF Then
                    Continue For
                End If

                Select Case item.CollectionType
                    Case EnumCollectionType.WithholdingTaxOnMF
                        WithholdTax += selectedItem.Amount
                    Case EnumCollectionType.WithholdingTaxOnDefaultInterest                        
                        Others += selectedItem.Amount
                    Case EnumCollectionType.WithholdingVatOnMF, EnumCollectionType.WithholdingVatOnDefaultInterest
                        WithholdVat += selectedItem.Amount
                    Case EnumCollectionType.DefaultInterestOnEnergy, EnumCollectionType.DefaultInterestOnMF
                        Others += selectedItem.Amount
                    Case EnumCollectionType.MarketFees
                        If ParticipantInfo.ZeroRatedMarketFees = True Then
                            VatZeroRated += selectedItem.Amount
                        Else
                            Vatable += selectedItem.Amount
                        End If
                    Case EnumCollectionType.VatOnMarketFees, EnumCollectionType.DefaultInterestOnVatOnMF
                        VAT += selectedItem.Amount
                    Case EnumCollectionType.Energy
                        Try
                            If selectedItem.WESMBillSummaryNo.SummaryType = EnumSummaryType.INV Then
                                If Left(selectedItem.ReferenceNumber, 3) = "INV" Or Left(selectedItem.ReferenceNumber, 3) = "FSW" Or Left(selectedItem.ReferenceNumber, 3) = "TS-" _
                                    Or (selectedItem.ReferenceNumber.Contains("FS-W") And selectedItem.ReferenceNumber.Contains("-ADJ")) Then
                                    Vatable = selectedItem.Amount
                                Else

                                    Dim itemCount = (From x In listSalesAndPurchases _
                                                             Where x.InvoiceNumber = selectedItem.WESMBillSummaryNo.INVDMCMNo _
                                                             Select x).Count()

                                    If itemCount = 0 Then
                                        Throw New ApplicationException("No Sales and Purchased")
                                    End If

                                    Dim itemSalesAndPurchases = (From x In listSalesAndPurchases _
                                                                Where x.InvoiceNumber = selectedItem.WESMBillSummaryNo.INVDMCMNo _
                                                                Select x).First()



                                    Dim tempVatable = Math.Round(selectedItem.Amount * Math.Abs(itemSalesAndPurchases.VatableRatio), 2)
                                    Dim tempVatZeroRated = Math.Round(selectedItem.Amount * Math.Abs(itemSalesAndPurchases.ZeroRatedRatio), 2)

                                    If tempVatable + tempVatZeroRated <> selectedItem.Amount Then
                                        If tempVatable <> 0 Then
                                            Vatable += tempVatable - (tempVatable + tempVatZeroRated - selectedItem.Amount)
                                            VatZeroRated += tempVatZeroRated
                                        Else
                                            Vatable += tempVatable
                                            VatZeroRated += tempVatZeroRated - (tempVatable + tempVatZeroRated - selectedItem.Amount)
                                        End If
                                    Else
                                        Vatable += tempVatable
                                        VatZeroRated += tempVatZeroRated
                                    End If
                                End If
                            Else
                                Vatable = selectedItem.Amount
                            End If

                        Catch ex As Exception
                            Throw New Exception(ex.Message.ToString & ". " & selectedItem.WESMBillSummaryNo.INVDMCMNo.ToString())
                        End Try

                    Case EnumCollectionType.VatOnEnergy
                        VAT += selectedItem.Amount

                    Case EnumCollectionType.WithholdingTaxonEnergy
                        WithholdTax += Math.Abs(selectedItem.Amount)
                End Select

                'For Official Receipt Summary
                listORSummary.Add(New OfficialReceiptSummary(selectedCol.ORNo, selectedItem.WESMBillSummaryNo.WESMBillSummaryNo, selectedItem.DueDate, _
                                                             selectedItem.Amount, selectedItem.CollectionType))
            Next

            'For Cash
            listORDetails.Add(New OfficialReceiptDetails(selectedCol.ORNo, AMModule.ClearingAccountCode, _
                                                         "Clearing", Math.Abs(selectedCol.CollectedAmount), 0))
            'For Held Collection
            If selectedCol.CollectedHeld <> 0 Then
                listORSummary.Add(New OfficialReceiptSummary(selectedCol.ORNo, 0, Nothing, Math.Abs(selectedCol.CollectedHeld), EnumCollectionType.AppliedHeldCollection))

                listORDetails.Add(New OfficialReceiptDetails(selectedCol.ORNo, AMModule.UnappliedcollectionCode, _
                                                             EnumCollectionMonitoringType.TransferToHeldCollection.ToString(), _
                                                             Math.Abs(selectedCol.CollectedHeld), 0))
                Dim HeldCollection As Decimal = selectedCol.CollectedHeld

                'Check where applied held collection will be deducted
                If Vatable <> 0 Then
                    If HeldCollection >= Vatable Then
                        HeldCollection -= Vatable
                        Vatable = 0
                    Else
                        Vatable -= HeldCollection
                        HeldCollection = 0
                    End If
                End If

                If VAT <> 0 And HeldCollection <> 0 Then
                    If HeldCollection >= VAT Then
                        HeldCollection -= VAT
                        VAT = 0
                    Else
                        VAT -= HeldCollection
                        HeldCollection = 0
                    End If
                End If

                If Others <> 0 And HeldCollection <> 0 Then
                    If HeldCollection >= Others Then
                        HeldCollection -= Others
                        Others = 0
                    Else
                        Others -= HeldCollection
                        HeldCollection = 0
                    End If
                End If
            End If

            'For AR-WESM
            listORDetails.Add(New OfficialReceiptDetails(selectedCol.ORNo, AMModule.CreditCode, _
                                                         "AR-WESM", 0, (Math.Abs(selectedCol.CollectedAmount) + Math.Abs(selectedCol.CollectedHeld) - totalAR)))

            With selectedCol
                result.Add(New OfficialReceiptMain(.ORNo, .CollectionDate, .IDNumber, Math.Abs(.CollectedAmount) - totalPEMC, _
                                                    1, listORDetails, listORSummary, "", "", EnumORTransactionType.Collection, VatExempt, Vatable, _
                                                    VAT, VatZeroRated, Others, WithholdTax, WithholdVat))
                result.TrimExcess()
            End With
        Next

        Return result
    End Function
#End Region

#Region "GenerateCollectionEFT"
    Public Function GenerateCollectionEFT(ByVal listMonitoring As List(Of CollectionMonitoring)) As List(Of EFT)
        Dim result As New List(Of EFT)

        Dim listItem = From x In listMonitoring _
                       Where x.TransType = EnumCollectionMonitoringType.TransferToExcessCollection _
                       And x.Amount <> 0 _
                       Select x

        For Each item In listItem
            With item
                result.Add(New EFT(.AllocationDate, .IDNumber, .IDNumber.PaymentType, .Amount))
            End With
        Next
        result.TrimExcess()

        Return result
    End Function
#End Region

#Region "GenerateCollectionFTF"
    Public Function GenerateCollectionFTF(ByVal AllocationDate As Date, ByVal listCollectionMonitoring As List(Of CollectionMonitoring), _
                                           ByVal listCollections As List(Of Collection), ByVal SignatoriesFTF As DocSignatories) _
                                           As List(Of FundTransferFormMain)
        Dim result As New List(Of FundTransferFormMain)
        Dim listDetails As New List(Of FundTransferFormDetails)

        'Get the replenishment
        Dim listReplenishment = From x In listCollectionMonitoring _
                                Where x.TransType = EnumCollectionMonitoringType.TransferToPRReplenishment _
                                Select x
        'Get the drawdown
        Dim listDrawdown = From x In listCollectionMonitoring _
                           Where x.TransType = EnumCollectionMonitoringType.TransferToPRDrawdown _
                           Select x

        'Get the Trasfer to PEMC Account
        Dim listPEMCAccount = From x In listCollectionMonitoring _
                              Where x.TransType = EnumCollectionMonitoringType.TransferToPEMCAccount _
                              Select x

        If listReplenishment.Count > 0 Then
            Dim listFTFParticipants As New List(Of FundTransferFormParticipant)
            Dim listFTFDetails As New List(Of FundTransferFormDetails)
            Dim totalReplenishment As Decimal = 0
            For Each item In listReplenishment
                listFTFParticipants.Add(New FundTransferFormParticipant(0, item.IDNumber, item.Amount))
                totalReplenishment += item.Amount
            Next

            listFTFDetails.Add(New FundTransferFormDetails(0, AMModule.CashinBankPrudentialCode, AMModule.PrudentialBankAccountNo, _
                                                           totalReplenishment, 0))

            listFTFDetails.Add(New FundTransferFormDetails(0, AMModule.CashInbankSettlementcode, AMModule.SettlementBankAccountNo, _
                                                           0, totalReplenishment))

            result.Add(New FundTransferFormMain(AllocationDate, totalReplenishment, EnumFTFTransType.Replenishment, SignatoriesFTF.Signatory_1, _
                                                SignatoriesFTF.Signatory_2, listFTFParticipants, listFTFDetails))
            result.TrimExcess()
        End If

        If listDrawdown.Count > 0 Then
            Dim listFTFParticipants As New List(Of FundTransferFormParticipant)
            Dim listFTFDetails As New List(Of FundTransferFormDetails)

            Dim totalDrawdown As Decimal = 0
            For Each item In listDrawdown
                listFTFParticipants.Add(New FundTransferFormParticipant(0, item.IDNumber, item.Amount))
                totalDrawdown += item.Amount
            Next

            listFTFDetails.Add(New FundTransferFormDetails(0, AMModule.CashinBankPrudentialCode, AMModule.PrudentialBankAccountNo, _
                                                           0, totalDrawdown))

            listFTFDetails.Add(New FundTransferFormDetails(0, AMModule.CashInbankSettlementcode, AMModule.SettlementBankAccountNo, _
                                                           totalDrawdown, 0))

            result.Add(New FundTransferFormMain(AllocationDate, totalDrawdown, EnumFTFTransType.DrawDown, SignatoriesFTF.Signatory_1, _
                                                SignatoriesFTF.Signatory_2, listFTFParticipants, listFTFDetails))
            result.TrimExcess()
        End If

        If listPEMCAccount.Count > 0 Then
            Dim listFTFParticipants As New List(Of FundTransferFormParticipant)
            Dim listFTFDetails As New List(Of FundTransferFormDetails)

            Dim totalPEMCAccount As Decimal = 0
            For Each item In listPEMCAccount
                listFTFParticipants.Add(New FundTransferFormParticipant(0, item.IDNumber, item.Amount))
                totalPEMCAccount += item.Amount
            Next

            listFTFDetails.Add(New FundTransferFormDetails(0, AMModule.CashinBankPEMCCode, AMModule.PemcBankAccountNo, _
                                                           totalPEMCAccount, 0))

            listFTFDetails.Add(New FundTransferFormDetails(0, AMModule.CashInbankSettlementcode, AMModule.SettlementBankAccountNo, _
                                                           0, totalPEMCAccount))

            result.Add(New FundTransferFormMain(AllocationDate, totalPEMCAccount, EnumFTFTransType.TransferPEMCAccount, SignatoriesFTF.Signatory_1, _
                                                SignatoriesFTF.Signatory_2, listFTFParticipants, listFTFDetails))
            result.TrimExcess()
        End If


        ''For Market Fees, Vat on Market Fees and Default Interest in Market Fees
        'Dim dicMF As New Dictionary(Of String, Decimal)

        'For Each item In listCollections
        '    Dim totalMF = (From x In item.ListOfCollectionAllocation _
        '                   Where x.CollectionType = EnumCollectionType.DefaultInterestOnMF Or x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF _
        '                   Or x.CollectionType = EnumCollectionType.MarketFees Or x.CollectionType = EnumCollectionType.VatOnMarketFees _
        '                   Or x.CollectionType = EnumCollectionType.WithholdingTaxOnMF Or x.CollectionType = EnumCollectionType.WithholdingVatonMF _
        '                   Or x.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest Or x.CollectionType = EnumCollectionType.WithholdingVatOnDefaultInterest _
        '                   Select x.Amount).Sum()

        '    If totalMF <> 0 Then
        '        If Not dicMF.ContainsKey(item.IDNumber) Then
        '            dicMF.Add(item.IDNumber, Math.Abs(totalMF))
        '        Else
        '            dicMF(item.IDNumber) += Math.Abs(totalMF)
        '        End If
        '    End If
        'Next

        'If dicMF.Count > 0 Then
        '    Dim listFTFParticipants As New List(Of FundTransferFormParticipant)
        '    Dim listFTFDetails As New List(Of FundTransferFormDetails)

        '    Dim totalMF As Decimal = 0
        '    For Each item In dicMF
        '        listFTFParticipants.Add(New FundTransferFormParticipant(0, New AMParticipants(item.Key), item.Value))
        '        totalMF += item.Value
        '    Next

        '    listFTFDetails.Add(New FundTransferFormDetails(0, AMModule.CashinBankPEMCCode, AMModule.PemcBankAccountNo, _
        '                                                   totalMF, 0))

        '    listFTFDetails.Add(New FundTransferFormDetails(0, AMModule.CashInbankSettlementcode, AMModule.SettlementBankAccountNo, _
        '                                                   0, totalMF))

        '    result.Add(New FundTransferFormMain(AllocationDate, totalMF, EnumFTFTransType.TransferMarketFeesToPEMC, SignatoriesFTF.Signatory_1, _
        '                                        SignatoriesFTF.Signatory_2, listFTFParticipants, listFTFDetails))
        '    result.TrimExcess()
        'End If

        Return result
    End Function
#End Region

#Region "GenerateCollectionJournalVoucher"
    Public Function GenerateCollectionJournalVoucher(ByVal listDMCMSetup As List(Of DebitCreditMemo), _
                                                     ByVal listDMCMDrawdown As List(Of DebitCreditMemo), _
                                                     ByVal listOR As List(Of OfficialReceiptMain), _
                                                     ByVal listFTF As List(Of FundTransferFormMain), _
                                                     ByVal SignatoriesJV As DocSignatories, _
                                                     ByVal JVDate As Date) As JournalVoucher
        Dim result As New JournalVoucher
        Dim jvDetails As New List(Of JournalVoucherDetails)

        '***************** Collection Allocation Set-up ***********************
        If listDMCMSetup.Count > 0 Then
            Dim listDMCMDetails As New List(Of DebitCreditMemoDetails)
            For Each item In listDMCMSetup
                listDMCMDetails.AddRange(item.DMCMDetails)
                listDMCMDetails.TrimExcess()
            Next

            'Get the Withholding Tax on MF
            Dim totalEWT = (From x In listDMCMDetails _
                            Where x.AccountCode = AMModule.EWTReceivable _
                            Select x.Debit).Sum()

            If totalEWT > 0 Then
                jvDetails.Add(New JournalVoucherDetails(0, AMModule.EWTReceivable, totalEWT, 0))
            End If

            'Get the Withholding Vat on Vat on MF
            Dim totalEWV = (From x In listDMCMDetails _
                            Where x.AccountCode = AMModule.EWVReceivableCode _
                            Select x.Debit).Sum()

            If totalEWV > 0 Then
                jvDetails.Add(New JournalVoucherDetails(0, AMModule.EWVReceivableCode, totalEWV, 0))
            End If

            'Get the AR-WESM
            Dim totalARCredit = (From x In listDMCMDetails _
                                 Where x.AccountCode = AMModule.CreditCode _
                                 Select x.Credit).Sum()

            If totalARCredit > 0 Then
                jvDetails.Add(New JournalVoucherDetails(0, AMModule.CreditCode, 0, totalARCredit))
            End If

            'Get the default interest on MF
            Dim totalMF = (From x In listDMCMDetails _
                            Where x.AccountCode = AMModule.DefaultInterestMarketFees _
                            Select x.Credit).Sum()
            If totalMF > 0 Then
                jvDetails.Add(New JournalVoucherDetails(0, AMModule.DefaultInterestMarketFees, 0, totalMF))
            End If

            'Get the default interest on Vat on MF
            Dim totalMFVat = (From x In listDMCMDetails _
                              Where x.AccountCode = AMModule.MarketFeesOutputTaxCode _
                              Select x.Credit).Sum()
            If totalMFVat > 0 Then
                jvDetails.Add(New JournalVoucherDetails(0, AMModule.MarketFeesOutputTaxCode, 0, totalMFVat))
            End If

            'Get the AR-WESM
            Dim totalARDebit = (From x In listDMCMDetails _
                                Where x.AccountCode = AMModule.CreditCode _
                                Select x.Debit).Sum()
            If totalARDebit > 0 Then
                jvDetails.Add(New JournalVoucherDetails(0, AMModule.CreditCode, totalARDebit, 0))
            End If

            'Get the default interest on energy
            Dim totalDIEnergy = (From x In listDMCMDetails _
                                 Where x.AccountCode = AMModule.DefaultInterestCode _
                                 Select x.Credit).Sum()
            If totalDIEnergy > 0 Then
                jvDetails.Add(New JournalVoucherDetails(0, AMModule.DefaultInterestCode, 0, totalDIEnergy))
            End If
        End If

        '***************** Drawdown ***********************
        If listDMCMDrawdown.Count > 0 Then
            Dim listDMCMDetails As New List(Of DebitCreditMemoDetails)
            For Each item In listDMCMDrawdown
                listDMCMDetails.AddRange(item.DMCMDetails)
                listDMCMDetails.TrimExcess()
            Next

            'Get the prudential requirement
            Dim totalPR = (From x In listDMCMDetails _
                           Where x.AccountCode = AMModule.PRWESMCode _
                           Select x.Debit).Sum()

            If totalPR > 0 Then
                jvDetails.Add(New JournalVoucherDetails(0, AMModule.PRWESMCode, totalPR, 0))
            End If

            'Get the AR-WESM
            Dim totalAR = (From x In listDMCMDetails _
                           Where x.AccountCode = AMModule.CreditCode _
                           Select x.Credit).Sum()
            If totalAR > 0 Then
                jvDetails.Add(New JournalVoucherDetails(0, AMModule.CreditCode, 0, totalAR))
            End If
        End If

        '***************** Official Receipt ***********************
        If listOR.Count > 0 Then
            Dim listORDetails As New List(Of OfficialReceiptDetails)
            For Each item In listOR
                listORDetails.AddRange(item.ListORDetails)
                listORDetails.TrimExcess()
            Next

            'Get the prudential requirement
            Dim totalPR = (From x In listORDetails _
                           Where x.AccountCode = AMModule.PRWESMCode _
                           Select x.Credit).Sum()
            If totalPR > 0 Then
                jvDetails.Add(New JournalVoucherDetails(0, AMModule.PRWESMCode, 0, totalPR))
            End If

            'Get the transfer to PEMC account, excess collection, held collection
            Dim totalUnApplied = (From x In listORDetails _
                                  Where x.AccountCode = AMModule.UnappliedcollectionCode _
                                  Select x.Credit).Sum()
            If totalUnApplied > 0 Then
                jvDetails.Add(New JournalVoucherDetails(0, AMModule.UnappliedcollectionCode, 0, totalUnApplied))
            End If

            'Get the applied held collection
            Dim totalAppliedHeld = (From x In listORDetails _
                                    Where x.AccountCode = AMModule.UnappliedcollectionCode _
                                    Select x.Debit).Sum()
            If totalAppliedHeld > 0 Then
                jvDetails.Add(New JournalVoucherDetails(0, AMModule.UnappliedcollectionCode, totalAppliedHeld, 0))
            End If

            'Get the Withholding tax on MF
            Dim totalWithTax = (From x In listORDetails _
                                Where x.AccountCode = AMModule.EWTReceivable _
                                Select x.Debit).Sum()
            If totalWithTax > 0 Then
                jvDetails.Add(New JournalVoucherDetails(0, AMModule.EWTReceivable, totalWithTax, 0))
            End If

            'Get the Withholding Vat on MF
            Dim totalWithVat = (From x In listORDetails _
                                Where x.AccountCode = AMModule.EWVReceivableCode _
                                Select x.Debit).Sum()
            If totalWithVat > 0 Then
                jvDetails.Add(New JournalVoucherDetails(0, AMModule.EWVReceivableCode, totalWithVat, 0))
            End If

            'Get the AR-WESM
            Dim totalAR = (From x In listORDetails _
                           Where x.AccountCode = AMModule.CreditCode _
                           Select x.Credit).Sum()
            If totalAR > 0 Then
                jvDetails.Add(New JournalVoucherDetails(0, AMModule.CreditCode, 0, totalAR))
            End If

            'Get the cash settlement
            Dim totalClearing = (From x In listORDetails _
                                 Where x.AccountCode = AMModule.ClearingAccountCode _
                                 Select x.Debit).Sum()
            If totalClearing > 0 Then
                jvDetails.Add(New JournalVoucherDetails(0, AMModule.ClearingAccountCode, totalClearing, 0))
            End If
        End If

        '***************** Fund Transfer Form ***********************
        If listFTF.Count <> 0 Then
            For Each item In listFTF
                'Payment allocation will be used in the entries of Market Fees to PEMC Account
                If item.TransType <> EnumFTFTransType.TransferMarketFeesToPEMC Then
                    For Each itemDetails In item.ListOfFTFDetails
                        jvDetails.Add(New JournalVoucherDetails(0, itemDetails.AccountCode, itemDetails.Debit, itemDetails.Credit))
                    Next
                End If
            Next
        End If

        'For Journal Voucher
        With result
            .JVNumber = 0
            .Status = 1
            .JVDate = JVDate
            .PreparedBy = AMModule.FullName
            .CheckedBy = SignatoriesJV.Signatory_1
            .ApprovedBy = SignatoriesJV.Signatory_2
            .PostedType = EnumPostedType.C.ToString()
            .JVDetails = jvDetails
            .Remarks = "Collection summary for batch C-XXX"
        End With

        Return result
    End Function
#End Region

#Region "GenerateCollectionGPPosted"
    Public Function GenerateCollectionGPPosted(ByVal jv As JournalVoucher) As WESMBillGPPosted
        Dim result As New WESMBillGPPosted

        Dim totalDebit = (From x In jv.JVDetails _
                          Select x.Debit).Sum()

        Dim totalCredit = (From x In jv.JVDetails _
                           Select x.Credit).Sum()

        If totalDebit <> totalCredit Then
            Throw New ApplicationException("Debit amount and credit amount are not equal!")
        End If

        With result
            .PostType = EnumPostedType.C.ToString()
            .DocumentAmount = totalDebit
        End With

        Return result
    End Function
#End Region

#Region "GenerateDailyCollectionJournalVoucher"
    Public Function GenerateDailyCollectionJournalVoucher(ByVal listCollections As List(Of Collection), _
                                                          ByVal SignatoriesJV As DocSignatories, ByVal JVDate As Date) As JournalVoucher
        Dim result As New JournalVoucher
        Dim jvDetails As New List(Of JournalVoucherDetails)

        Dim total = (From x In listCollections _
                     Select x.CollectedAmount).Sum()

        Dim CollDate As Date = (From x In listCollections Select x.CollectionDate).Distinct.FirstOrDefault

        If IsDBNull(CollDate) Then
            CollDate = JVDate
        End If

        jvDetails.Add(New JournalVoucherDetails(0, AMModule.CashInbankSettlementcode, Math.Abs(total), 0))
        jvDetails.Add(New JournalVoucherDetails(0, AMModule.ClearingAccountCode, 0, Math.Abs(total)))

        'For Journal Voucher
        With result
            .JVNumber = 0
            .Status = 1
            .JVDate = CollDate
            .PreparedBy = AMModule.FullName
            .CheckedBy = SignatoriesJV.Signatory_1
            .ApprovedBy = SignatoriesJV.Signatory_2
            .PostedType = EnumPostedType.DC.ToString()
            .JVDetails = jvDetails
            .Remarks = "Daily collection summary for batch C-XXX."
        End With

        Return result
    End Function
#End Region

#Region "GenerateDailyCollectionGPPosted"
    Public Function GenerateDailyCollectionGPPosted(ByVal jv As JournalVoucher) As WESMBillGPPosted
        Dim result As New WESMBillGPPosted

        Dim totalDebit = (From x In jv.JVDetails _
                          Select x.Debit).Sum()

        Dim totalCredit = (From x In jv.JVDetails _
                           Select x.Credit).Sum()

        If totalDebit <> totalCredit Then
            Throw New ApplicationException("Debit amount and credit amount are not equal!")
        End If

        With result
            .PostType = EnumPostedType.DC.ToString()
            .DocumentAmount = totalDebit
        End With

        Return result
    End Function
#End Region

#Region "GenerateJournalVoucherReport"
    Public Function GenerateJournalVoucherReport(ByVal ListAccountingCodes As List(Of AccountingCode), _
                                                 ByVal itemJV As JournalVoucher, ByVal dtJV As DataTable, _
                                                 ByVal dtJVDetails As DataTable, ByVal dtAcct As DataTable) As DataSet
        Dim ds As New DataSet

        Dim rowJV = dtJV.NewRow
        With itemJV
            rowJV("JV_NO") = .JVNumber
            rowJV("JV_DATE") = .JVDate
            rowJV("BATCHCODE") = .BatchCode.ToString
            rowJV("STATUS") = .Status
            rowJV("PREPAREDBY") = .PreparedBy
            rowJV("CHECKEDBY") = .CheckedBy
            rowJV("APPROVEDBY") = .ApprovedBy
            rowJV("UPDATEDBY") = .UpdatedBy
            rowJV("UPDATEDDATE") = .UpdatedDate
            rowJV("GPREF_NO") = .GPRefNo
            rowJV("REMARKS") = .Remarks
        End With
        dtJV.Rows.Add(rowJV)
        dtJV.AcceptChanges()

        For Each item In itemJV.JVDetails
            Dim rowJVDetails = dtJVDetails.NewRow()

            With item
                rowJVDetails("JV_NO") = .JVNumber
                rowJVDetails("ACCOUNTCODE") = .AccountCode
                rowJVDetails("CREDIT") = Math.Abs(.Credit)
                rowJVDetails("DEBIT") = Math.Abs(.Debit)
                rowJVDetails("UPDATEDBY") = .UpdatedBy
                rowJVDetails("UPDATEDDATE") = .UpdatedDate
            End With
            dtJVDetails.Rows.Add(rowJVDetails)
        Next
        dtJVDetails.AcceptChanges()

        For Each item In ListAccountingCodes
            Dim rowAcct = dtAcct.NewRow()

            With item
                rowAcct("ACCT_CODE") = .AccountCode
                rowAcct("DESCRIPTION") = .Description
            End With
            dtAcct.Rows.Add(rowAcct)
        Next
        dtAcct.AcceptChanges()

        With ds.Tables
            .Add(dtJV)
            .Add(dtJVDetails)
            .Add(dtAcct)
        End With

        Return ds
    End Function
#End Region

#Region "GenerateCollectionSummary"
    Public Function GenerateCollectionSummary(ByVal BatchCode As String, ByVal JVNo As Long, ByVal ListCollections As List(Of Collection), _
                                              ByVal ListCollectionMonitoring As List(Of CollectionMonitoring), _
                                              ByVal ListParticipants As List(Of AMParticipants), ByVal ListDMCM As List(Of DebitCreditMemo), _
                                              ByVal Signatories As DocSignatories, _
                                              ByRef TotalCash As Decimal, ByRef TotalDrawdown As Decimal, _
                                              ByVal dt As DataTable, ByVal DocumentDate As String) As DataTable

        Dim listCollectionFinal = From x In ListCollections Join y In ListParticipants _
                                  On x.IDNumber Equals y.IDNumber _
                                  Select x, y.ParticipantID

        For Each item In listCollectionFinal
            If item.x.CollectionCategory = EnumCollectionCategory.Cash Then
                TotalCash += item.x.CollectedAmount
            Else
                TotalDrawdown += item.x.CollectedAmount
            End If

            For Each itemAlloc In item.x.ListOfCollectionAllocation
                Dim selectedAlloc = itemAlloc
                Dim row = dt.NewRow()
                With item
                    row("JVNo") = JVNo
                    row("BatchCode") = BatchCode
                    row("ParticipantID") = .ParticipantID
                    row("IDNumber") = .x.IDNumber
                    row("Type") = .x.CollectionCategory.ToString()

                    If .x.CollectionCategory = EnumCollectionCategory.Cash Then
                        row("RefNo") = Me.GenerateBIRDocumentNumber(.x.ORNo, BIRDocumentsType.OfficialReceipt)
                    Else
                        row("RefNo") = Me.GenerateBIRDocumentNumber(.x.DMCMNumber, BIRDocumentsType.DMCM)
                    End If

                    row("CollectionDate") = .x.CollectionDate.ToString("MM/dd/yyyy")
                    row("Amount") = .x.CollectedAmount
                    row("AllocationDate") = .x.AllocationDate.ToString("MM/dd/yyyy")
                    If itemAlloc.ReferenceNumber.Length >= 26 Then
                        row("DocumentNo") = If(itemAlloc.ReferenceType = EnumSummaryType.DMCM, itemAlloc.ReferenceNumber, itemAlloc.ReferenceNumber)
                    Else
                        row("DocumentNo") = If(itemAlloc.ReferenceType = EnumSummaryType.DMCM, Me.GenerateBIRDocumentNumber(CLng(itemAlloc.ReferenceNumber), BIRDocumentsType.DMCM), itemAlloc.ReferenceNumber)
                    End If


                    If selectedAlloc.DMCMNumber <> 0 Then
                        Try
                            row("DocDate") = (From x In ListDMCM _
                                              Where x.DMCMNumber = selectedAlloc.DMCMNumber _
                                              Select x.UpdatedDate.ToString("MM/dd/yyyy")).First()
                        Catch ex As Exception
                            row("DocDate") = DocumentDate
                        End Try
                    End If

                    row("DueDate") = itemAlloc.DueDate.ToString("MM/dd/yyyy")
                    row("AmountApplied") = itemAlloc.Amount
                    row("CollecTypeCode") = itemAlloc.CollectionTypeCode.ToString()
                    row("Signatory1") = Signatories.Signatory_1
                    row("Position1") = Signatories.Position_1
                    row("Signatory2") = Signatories.Signatory_2
                    row("Position2") = Signatories.Position_2
                    row("Signatory3") = Signatories.Signatory_3
                    row("Position3") = Signatories.Position_3

                    If itemAlloc.DMCMNumber <> 0 Then
                        row("CreatedDocumentNo") = Me.GenerateBIRDocumentNumber(itemAlloc.DMCMNumber, BIRDocumentsType.DMCM)
                    Else
                        row("CreatedDocumentNo") = ""
                    End If

                    dt.Rows.Add(row)
                End With
            Next

            Dim colNumber = item.x.CollectionNumber


            'For Transfer Held 
            Dim totalTransferHeld = (From x In ListCollectionMonitoring _
                                     Where x.CollectionNo = colNumber _
                                     And x.TransType = EnumCollectionMonitoringType.TransferToHeldCollection _
                                     Select x.Amount).Sum

            If totalTransferHeld <> 0 Then
                Dim row = dt.NewRow()

                With item
                    row("JVNo") = JVNo
                    row("BatchCode") = BatchCode
                    row("ParticipantID") = .ParticipantID
                    row("IDNumber") = .x.IDNumber
                    row("Type") = .x.CollectionCategory.ToString()
                    row("RefNo") = Me.GenerateBIRDocumentNumber(.x.ORNo, BIRDocumentsType.OfficialReceipt)
                    row("CollectionDate") = .x.CollectionDate.ToString("MM/dd/yyyy")
                    row("Amount") = .x.CollectedAmount
                    row("AllocationDate") = .x.AllocationDate.ToString("MM/dd/yyyy")
                    row("DocumentNo") = "TransferHeld"
                    row("AmountApplied") = totalTransferHeld
                    row("Signatory1") = Signatories.Signatory_1
                    row("Position1") = Signatories.Position_1
                    row("Signatory2") = Signatories.Signatory_2
                    row("Position2") = Signatories.Position_2
                    row("Signatory3") = Signatories.Signatory_3
                    row("Position3") = Signatories.Position_3

                    dt.Rows.Add(row)
                End With
            End If

            'For Escess 
            Dim totalExcess = (From x In ListCollectionMonitoring _
                               Where x.CollectionNo = colNumber _
                               And x.TransType = EnumCollectionMonitoringType.TransferToExcessCollection _
                               Select x.Amount).Sum

            If totalExcess <> 0 Then
                Dim row = dt.NewRow()

                With item
                    row("JVNo") = JVNo
                    row("BatchCode") = BatchCode
                    row("ParticipantID") = .ParticipantID
                    row("IDNumber") = .x.IDNumber
                    row("Type") = .x.CollectionCategory.ToString()
                    row("RefNo") = Me.GenerateBIRDocumentNumber(.x.ORNo, BIRDocumentsType.OfficialReceipt)
                    row("CollectionDate") = .x.CollectionDate.ToString("MM/dd/yyyy")
                    row("Amount") = .x.CollectedAmount
                    row("AllocationDate") = .x.AllocationDate.ToString("MM/dd/yyyy")
                    row("DocumentNo") = "Excess"
                    row("AmountApplied") = totalExcess
                    row("Signatory1") = Signatories.Signatory_1
                    row("Position1") = Signatories.Position_1
                    row("Signatory2") = Signatories.Signatory_2
                    row("Position2") = Signatories.Position_2
                    row("Signatory3") = Signatories.Signatory_3
                    row("Position3") = Signatories.Position_3

                    dt.Rows.Add(row)
                End With
            End If

            'For Replenishment
            Dim totalReplenishment = (From x In ListCollectionMonitoring _
                                      Where x.CollectionNo = colNumber _
                                      And x.TransType = EnumCollectionMonitoringType.TransferToPRReplenishment _
                                      Select x.Amount).Sum()

            If totalReplenishment <> 0 Then
                Dim row = dt.NewRow()

                With item
                    row("JVNo") = JVNo
                    row("BatchCode") = BatchCode
                    row("ParticipantID") = .ParticipantID
                    row("IDNumber") = .x.IDNumber
                    row("Type") = .x.CollectionCategory.ToString()
                    row("RefNo") = Me.GenerateBIRDocumentNumber(.x.ORNo, BIRDocumentsType.OfficialReceipt)
                    row("CollectionDate") = .x.CollectionDate.ToString("MM/dd/yyyy")
                    row("Amount") = .x.CollectedAmount
                    row("AllocationDate") = .x.AllocationDate.ToString("MM/dd/yyyy")
                    row("DocumentNo") = "Replenishment"
                    row("AmountApplied") = totalReplenishment
                    row("Signatory1") = Signatories.Signatory_1
                    row("Position1") = Signatories.Position_1
                    row("Signatory2") = Signatories.Signatory_2
                    row("Position2") = Signatories.Position_2
                    row("Signatory3") = Signatories.Signatory_3
                    row("Position3") = Signatories.Position_3

                    dt.Rows.Add(row)
                End With
            End If

            'For Applied Held 
            If item.x.CollectedHeld <> 0 Then
                Dim row = dt.NewRow()

                With item
                    row("JVNo") = JVNo
                    row("BatchCode") = BatchCode
                    row("ParticipantID") = .ParticipantID
                    row("IDNumber") = .x.IDNumber
                    row("Type") = .x.CollectionCategory.ToString()
                    row("RefNo") = Me.GenerateBIRDocumentNumber(.x.ORNo, BIRDocumentsType.OfficialReceipt)
                    row("CollectionDate") = .x.CollectionDate.ToString("MM/dd/yyyy")
                    row("Amount") = .x.CollectedAmount
                    row("AllocationDate") = .x.AllocationDate.ToString("MM/dd/yyyy")
                    row("DocumentNo") = "AppliedHeld"
                    row("AmountApplied") = item.x.CollectedHeld * -1
                    row("Signatory1") = Signatories.Signatory_1
                    row("Position1") = Signatories.Position_1
                    row("Signatory2") = Signatories.Signatory_2
                    row("Position2") = Signatories.Position_2
                    row("Signatory3") = Signatories.Signatory_3
                    row("Position3") = Signatories.Position_3

                    dt.Rows.Add(row)
                End With
            End If
        Next

        Return dt
    End Function
#End Region

#Region "GenerateWESMInvoice"
    Public Function GenerateWESMInvoice(ByVal DatatableInvoiceMain As DataTable, ByVal DatatableInvoiceDetails As DataTable, _
                                         ByVal ListWESMInvoice As List(Of WESMInvoice), ByVal ListWESMSalesAndPurchased As List(Of WESMBillSalesAndPurchased), _
                                         ByVal ListParticipants As List(Of AMParticipants), ByVal ListCharges As List(Of ChargeId), _
                                         ByVal ListCalendar As List(Of CalendarBillingPeriod), _
                                         ByVal ListSelectedParticipants As List(Of String), _
                                         ByVal Signatory As DocSignatories, _
                                         ByVal FileType As EnumFileType) As DataSet

        Dim ds As New DataSet

        Try
            For Each item In ListSelectedParticipants
                Dim participant = item                
                'Get the distinct invoices
                Dim listInvoice = From x In ListWESMInvoice Join y In ListParticipants _
                                  On x.IDNumber Equals y.IDNumber _
                                  Select x.InvoiceNumber Distinct                

                For Each itemInvoice In listInvoice
                    Dim selectedInvoice = itemInvoice
                    Dim billedTo As String, billingDate As String
                    Dim SalesVatable As Decimal = 0, SalesZeroRated As Decimal = 0, SalesZeroRatedEcoZone As Decimal = 0, VatOnSales As Decimal = 0
                    Dim PurchasesVatable As Decimal = 0, PurchasesZeroRated As Decimal = 0, PurchasesZeroRatedEcoZone As Decimal = 0, VatOnPurchases As Decimal = 0
                    Dim GMRValue As Decimal = 0D, NSSRA As Decimal = 0, WTAX As Decimal = 0

                    Dim listData = From w In ListWESMInvoice Join x In ListParticipants On _
                              w.IDNumber Equals x.IDNumber Join y In ListCharges On _
                              w.ChargeID Equals y.ChargeId Join z In ListCalendar On _
                              w.BillingPeriod Equals z.BillingPeriod Join v In ListParticipants On _
                              w.RegistrationID Equals v.IDNumber _
                              Where x.ParticipantID = participant And w.InvoiceNumber = selectedInvoice _
                              Select New With {.IDNumber = w.IDNumber, .RegistrationID = w.RegistrationID, .ParticipantID = x.ParticipantID, .BillingAddress = x.BillingAddress, _
                                               .ParticipantName = x.FullName, .ForTheAccountName = v.FullName, .GenLoad = x.GenLoad, _
                                               .InvoiceNumber = w.InvoiceNumber, .InvoiceDate = w.InvoiceDate, .StartDate = z.StartDate, _
                                               .EndDate = z.EndDate, .DueDate = w.DueDate, .ChargeId = y.ChargeId, .Description = y.Description, _
                                               .cIDType = y.cIDType, .Quantity = w.Quantity, .Amount = w.Amount, .MarketFeesRate = w.MarketFeesRate, _
                                               .Remarks = w.Remarks, .BusinessStyle = x.BusinessStyle}
                    If listData.Count = 0 Then
                        Continue For
                    End If

                    'For Energy Bill
                    If FileType = EnumFileType.Energy Then

                        Dim itemWESMBillSalesAndPurchases As New WESMBillSalesAndPurchased
                        Try
                            itemWESMBillSalesAndPurchases = (From x In ListWESMSalesAndPurchased _
                                                             Where x.InvoiceNumber = listData.First.InvoiceNumber _
                                                             Select x).First()
                        Catch ex As Exception
                            Throw New ApplicationException("Cannot find the sales and purchases of invoice number " & listData.First.InvoiceNumber)
                        End Try
                        'should be edited by lance
                        With itemWESMBillSalesAndPurchases
                            SalesVatable = .VatableSales '.ComputedVatableSales
                            SalesZeroRated = .ZeroRatedSales '.ComputedZeroRatedSales
                            SalesZeroRatedEcoZone = .ZeroRatedEcozone '.ComputedZeroRatedEcozone
                            VatOnSales = .VATonSales

                            PurchasesVatable = .VatablePurchases '.ComputedVatablePurchases
                            PurchasesZeroRated = .ZeroRatedPurchases '.ComputedZeroRatedPurchases
                            PurchasesZeroRatedEcoZone = 0
                            VatOnPurchases = .VATonPurchases
                            WTAX = .WithholdingTAX

                            NSSRA = .NSSRA
                            GMRValue = .GMR
                        End With
                        'end
                    End If

                    Dim rowMain = DatatableInvoiceMain.NewRow()
                    With listData.First
                        If .IDNumber = .RegistrationID Then
                            billedTo = .IDNumber & vbCrLf & .ParticipantName & vbCrLf & .BillingAddress
                        Else
                            billedTo = .IDNumber & vbCrLf & .ParticipantName & vbCrLf & .BillingAddress & vbCrLf & _
                                  vbCrLf & "For the account of:" & vbCrLf & .ForTheAccountName
                        End If

                        billingDate = .StartDate.ToString("MMM dd") & " - " & _
                                      .EndDate.ToString("MMM dd, yyyy")

                        rowMain("ID_NUMBER") = .IDNumber
                        rowMain("BUSINESS_STYLE") = .BusinessStyle
                        rowMain("BILLED_TO") = billedTo
                        rowMain("BILL_NUMBER") = .InvoiceNumber
                        rowMain("INVOICE_DATE") = .InvoiceDate
                        rowMain("BILLING_PERIOD") = billingDate
                        rowMain("DUE_DATE") = .DueDate
                        rowMain("PARTICIPANT_ID") = .ParticipantID
                        rowMain("SALES_VATABLE") = SalesVatable
                        rowMain("SALES_ZERO_RATED") = SalesZeroRated
                        rowMain("SALES_VAT_ON_ENERGY") = VatOnSales
                        rowMain("PURCHASES_VATABLE") = PurchasesVatable
                        rowMain("PURCHASES_ZERO_RATED") = PurchasesZeroRated
                        rowMain("PURCHASES_VAT_ON_ENERGY") = VatOnPurchases
                        rowMain("GMR") = GMRValue
                        rowMain("MARKET_FEE_RATE") = .MarketFeesRate
                        rowMain("REMARKS") = .Remarks
                        rowMain("SIGNATORY1") = Signatory.Signatory_1
                        rowMain("POSITION1") = Signatory.Position_1
                        rowMain("SIGNATORY2") = Signatory.Signatory_2
                        rowMain("POSITION2") = Signatory.Position_2
                        rowMain("SIGNATORY3") = Signatory.Signatory_3
                        rowMain("POSITION3") = Signatory.Position_3
                        rowMain("NSSRA") = NSSRA
                        rowMain("SALES_ZERO_RATED_ECOZONE") = SalesZeroRatedEcoZone
                        rowMain("PURCHASES_ZERO_RATED_ECOZONE") = PurchasesZeroRatedEcoZone
                        rowMain("REPORT_BIR") = AMModule.BIRPermitNumber
                        rowMain("WTAX") = WTAX
                        DatatableInvoiceMain.Rows.Add(rowMain)
                        DatatableInvoiceMain.AcceptChanges()
                    End With

                    For Each i In listData
                        Dim rowDetails = DatatableInvoiceDetails.NewRow()
                        rowDetails("BILL_NUMBER") = i.InvoiceNumber
                        rowDetails("DESCRIPTION") = i.Description
                        rowDetails("QUANTITY") = i.Quantity
                        rowDetails("AMOUNT") = i.Amount

                        DatatableInvoiceDetails.Rows.Add(rowDetails)
                        DatatableInvoiceDetails.AcceptChanges()
                    Next
                Next
            Next

            ds.Tables.Add(DatatableInvoiceMain)
            ds.Tables.Add(DatatableInvoiceDetails)
            ds.AcceptChanges()

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return ds
    End Function
#End Region

#Region "GenerateWESMInvoicePrelim"
    Public Function GenerateWESMInvoicePrelim(ByVal DatatableInvoiceMain As DataTable, ByVal DatatableInvoiceDetails As DataTable,
                                         ByVal ListWESMInvoice As List(Of WESMInvoice),
                                         ByVal ListParticipants As List(Of AMParticipants), ByVal ListCharges As List(Of ChargeId),
                                         ByVal ListCalendar As List(Of CalendarBillingPeriod),
                                         ByVal ListSelectedParticipants As List(Of String),
                                         ByVal Signatory As DocSignatories,
                                         ByVal FileType As EnumFileType) As DataSet

        Dim ds As New DataSet

        Try
            For Each item In ListSelectedParticipants
                Dim participant = item
                'Get the distinct invoices
                Dim listInvoice = From x In ListWESMInvoice Join y In ListParticipants
                                  On x.IDNumber Equals y.IDNumber
                                  Select x.InvoiceNumber Distinct

                For Each itemInvoice In listInvoice
                    Dim selectedInvoice = itemInvoice
                    Dim billedTo As String, billingDate As String
                    Dim SalesVatable As Decimal = 0, SalesZeroRated As Decimal = 0, SalesZeroRatedEcoZone As Decimal = 0, VatOnSales As Decimal = 0
                    Dim PurchasesVatable As Decimal = 0, PurchasesZeroRated As Decimal = 0, PurchasesZeroRatedEcoZone As Decimal = 0, VatOnPurchases As Decimal = 0
                    Dim GMRValue As Decimal = 0D, NSSRA As Decimal = 0, WTAX As Decimal = 0

                    Dim listData = From w In ListWESMInvoice Join x In ListParticipants On
                              w.IDNumber Equals x.IDNumber Join y In ListCharges On
                              w.ChargeID Equals y.ChargeId Join z In ListCalendar On
                              w.BillingPeriod Equals z.BillingPeriod Join v In ListParticipants On
                              w.RegistrationID Equals v.IDNumber
                                   Where x.ParticipantID = participant And w.InvoiceNumber = selectedInvoice
                                   Select New With {.IDNumber = w.IDNumber, .RegistrationID = w.RegistrationID, .ParticipantID = x.ParticipantID, .BillingAddress = x.BillingAddress,
                                               .ParticipantName = x.FullName, .ForTheAccountName = v.FullName, .GenLoad = x.GenLoad,
                                               .InvoiceNumber = w.InvoiceNumber, .InvoiceDate = w.InvoiceDate, .StartDate = z.StartDate,
                                               .EndDate = z.EndDate, .DueDate = w.DueDate, .ChargeId = y.ChargeId, .Description = y.Description,
                                               .cIDType = y.cIDType, .Quantity = w.Quantity, .Amount = w.Amount, .MarketFeesRate = w.MarketFeesRate,
                                               .Remarks = w.Remarks, .BusinessStyle = x.BusinessStyle}
                    If listData.Count = 0 Then
                        Continue For
                    End If



                    Dim rowMain = DatatableInvoiceMain.NewRow()
                    With listData.First
                        If .IDNumber = .RegistrationID Then
                            billedTo = .IDNumber & vbCrLf & .ParticipantName & vbCrLf & .BillingAddress
                        Else
                            billedTo = .IDNumber & vbCrLf & .ParticipantName & vbCrLf & .BillingAddress & vbCrLf &
                                  vbCrLf & "For the account of:" & vbCrLf & .ForTheAccountName
                        End If

                        billingDate = .StartDate.ToString("MMM dd") & " - " &
                                      .EndDate.ToString("MMM dd, yyyy")

                        rowMain("ID_NUMBER") = .IDNumber
                        rowMain("BUSINESS_STYLE") = .BusinessStyle
                        rowMain("BILLED_TO") = billedTo
                        rowMain("BILL_NUMBER") = .InvoiceNumber
                        rowMain("INVOICE_DATE") = .InvoiceDate
                        rowMain("BILLING_PERIOD") = billingDate
                        rowMain("DUE_DATE") = .DueDate
                        rowMain("PARTICIPANT_ID") = .ParticipantID
                        rowMain("SALES_VATABLE") = SalesVatable
                        rowMain("SALES_ZERO_RATED") = SalesZeroRated
                        rowMain("SALES_VAT_ON_ENERGY") = VatOnSales
                        rowMain("PURCHASES_VATABLE") = PurchasesVatable
                        rowMain("PURCHASES_ZERO_RATED") = PurchasesZeroRated
                        rowMain("PURCHASES_VAT_ON_ENERGY") = VatOnPurchases
                        rowMain("GMR") = GMRValue
                        rowMain("MARKET_FEE_RATE") = .MarketFeesRate
                        rowMain("REMARKS") = .Remarks
                        rowMain("SIGNATORY1") = Signatory.Signatory_1
                        rowMain("POSITION1") = Signatory.Position_1
                        rowMain("SIGNATORY2") = Signatory.Signatory_2
                        rowMain("POSITION2") = Signatory.Position_2
                        rowMain("SIGNATORY3") = Signatory.Signatory_3
                        rowMain("POSITION3") = Signatory.Position_3
                        rowMain("NSSRA") = NSSRA
                        rowMain("SALES_ZERO_RATED_ECOZONE") = SalesZeroRatedEcoZone
                        rowMain("PURCHASES_ZERO_RATED_ECOZONE") = PurchasesZeroRatedEcoZone
                        rowMain("REPORT_BIR") = AMModule.BIRPermitNumber
                        rowMain("WTAX") = WTAX
                        DatatableInvoiceMain.Rows.Add(rowMain)
                        DatatableInvoiceMain.AcceptChanges()
                    End With

                    For Each i In listData
                        Dim rowDetails = DatatableInvoiceDetails.NewRow()
                        rowDetails("BILL_NUMBER") = i.InvoiceNumber
                        rowDetails("DESCRIPTION") = i.Description
                        rowDetails("QUANTITY") = i.Quantity
                        rowDetails("AMOUNT") = i.Amount

                        DatatableInvoiceDetails.Rows.Add(rowDetails)
                        DatatableInvoiceDetails.AcceptChanges()
                    Next
                Next
            Next

            ds.Tables.Add(DatatableInvoiceMain)
            ds.Tables.Add(DatatableInvoiceDetails)
            ds.AcceptChanges()

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return ds
    End Function
#End Region

#Region "GeneratePrudentialReport"
    Public Function GeneratePrudentialReport(ByVal ListPrudentialHistory As List(Of PrudentialHistory), ByVal JVNumber As Long, _
                                             ByVal Signatory As DocSignatories, ByVal dt As DataTable) As DataTable

        For Each item In ListPrudentialHistory
            Dim row = dt.NewRow()
            Dim refno As String
            With item
                If .ORNo <> 0 Then
                    refno = Me.GenerateBIRDocumentNumber(.ORNo, BIRDocumentsType.OfficialReceipt)
                ElseIf .DMCMNumber <> 0 Then
                    refno = Me.GenerateBIRDocumentNumber(.DMCMNumber, BIRDocumentsType.DMCM)
                Else
                    refno = "NA"
                End If
                row("JV_NUMBER") = Me.GenerateBIRDocumentNumber(JVNumber, BIRDocumentsType.JournalVoucher)
                row("BATCH_CODE") = .BatchCode
                row("TRANSACTION_DATE") = .TransDate
                row("ID_NUMBER") = .IDNumber.IDNumber
                row("PARTICIPANT_ID") = .IDNumber.ParticipantID
                row("FULL_NAME") = .IDNumber.FullName
                row("REF_NO") = refno
                row("AMOUNT") = .Amount
                row("PREPARED_BY") = AMModule.FullName
                row("POSITION") = AMModule.Position
                row("SIGNATORY_1") = Signatory.Signatory_1
                row("POSITION_1") = Signatory.Position_1
            End With
            dt.Rows.Add(row)
        Next
        dt.AcceptChanges()

        Return dt
    End Function
#End Region

#Region "LoadSettingsValues"
    Public Sub LoadSettingsValues(ByVal dicSettings As Dictionary(Of String, AdminSettings))

        Try
            'Cash Accounting Codes
            AMModule.CashinBankPEMCCode = dicSettings("CashInBankPEMCCode").Value
            AMModule.CashinBankPrudentialCode = dicSettings("CashInBankPrudentialCode").Value
            AMModule.CashinBankAccountSurplusCode = dicSettings("CashInBankAccountSurplusCode").Value
            AMModule.CashInbankSettlementcode = dicSettings("CashInBankSettlementCode").Value

            'AR Accounting Code
            AMModule.CreditCode = dicSettings("CreditCode").Value
            AMModule.EWTReceivable = dicSettings("EWTReceivable").Value
            AMModule.EWVReceivableCode = dicSettings("EWVReceivableCode").Value
            AMModule.DefaultInterestCode = dicSettings("DefaultInterestCode").Value

            'AP Accounting Code
            AMModule.DebitCodeNonWESM = dicSettings("DebitCodeNonWESM").Value
            AMModule.DebitCode = dicSettings("DebitCode").Value
            AMModule.APStaleChecksCode = dicSettings("APStaleChecksCode").Value
            AMModule.APUnreleasedChecksCode = dicSettings("APUnreleasedChecksCode").Value
            AMModule.DeferredPaymentCode = dicSettings("DeferredPaymentCode").Value
            AMModule.UnappliedcollectionCode = dicSettings("UnappliedCollectionCode").Value
            AMModule.EWTPayable = dicSettings("EWTPayable").Value            

            'PR/STL/NSS Accounting Code
            AMModule.PRWESMCode = dicSettings("PRWESMCode").Value
            AMModule.NSSCode = dicSettings("NSSCode").Value
            AMModule.NSSVATCode = dicSettings("NSSVATCode").Value
            AMModule.InterestPayableNSSCode = dicSettings("InterestPayableNSSCode").Value
            AMModule.InterestPayablePRCode = dicSettings("InterestPayablePRCode").Value
            AMModule.InterestPayableSTLCode = dicSettings("InterestPayableSTLCode").Value

            'Clearing Account Code
            AMModule.ClearingAccountCode = dicSettings("ClearingAccountCode").Value

            'Market Fees Accounting Code
            AMModule.MarketTransFeesCode = dicSettings("MarketTransFeesCode").Value
            AMModule.MarketFeesOutputTaxCode = dicSettings("MarketFeesOutputTaxCode").Value
            AMModule.DefaultInterestMarketFees = dicSettings("DefaultInterestMarketFees").Value

            'Others
            AMModule.MinimumAmountToBeOffset = CDec(dicSettings("MinimumAmountToBeOffset").Value)
            AMModule.NSSRACode = dicSettings("NSSRACode").Value
            AMModule.VatValue = CDec(dicSettings("VatValue").Value)
            AMModule.VatCode = dicSettings("VatCode").Value
            AMModule.WESMBillNumberPrefix = dicSettings("WESMBillNumberPrefix").Value

            'Bank Account Numbers
            AMModule.PrudentialBankAccountNo = dicSettings("PrudentialBankAccountNo").Value
            AMModule.SettlementBankAccountNo = dicSettings("SettlementBankAccountNo").Value
            AMModule.PemcBankAccountNo = dicSettings("PemcBankAccountNo").Value
            AMModule.NSSBankAccountNo = dicSettings("NSSBankAccountNo").Value

            'Margin Call
            AMModule.MNEMultiplier = CDec(dicSettings("MNEDays").Value)
            AMModule.TradingLimitMultiplier = CDec(dicSettings("TLValue").Value)

            AMModule.DeferredPayment = CDec(dicSettings("DeferredPayment").Value)

            'Manual DMCM
            AMModule.ManualDMCMMarketFees = CStr(dicSettings("ManualDMCMMF").Value)
            AMModule.ManualDMCMVATonMarketFees = CStr(dicSettings("ManualDMCMVMF").Value)
            AMModule.ManualDMCMEnergy = CStr(dicSettings("ManualDMCME").Value)
            AMModule.ManualDMCMVATonEnergy = CStr(dicSettings("ManualDMCMEV").Value)

            'BIR in Final Statement, OR, DMCM, JV
            AMModule.BIRPermitNumber = "BIR Permit Number " & dicSettings("BIRPermitNumber").Value
            AMModule.BIRCASPermit = dicSettings("BIRCASPermit").Value
            AMModule.BIRDateIssued = dicSettings("BIRDateIssued").Value
            AMModule.BIRValidUntil = dicSettings("BIRValidUntil").Value

            AMModule.FSNumberPrefix = dicSettings("FSNumberPrefix").Value
            AMModule.DMCMNumberPrefix = dicSettings("DMCMNumberPrefix").Value
            AMModule.ORNumberPrefix = dicSettings("ORNumberPrefix").Value
            AMModule.JVNumberPrefix = dicSettings("JVNumberPrefix").Value
            AMModule.FTFNumberPrefix = dicSettings("FTFNumberPrefix").Value
            AMModule.RFPNumberPrefix = "RFP"
            AMModule.CheckNumberPrefix = "Check"
            AMModule.SOANumberPrefix = dicSettings("SOANumberPrefix").Value
            AMModule.CVNumberPrefix = dicSettings("CVNumberPrefix").Value

            'EWT Payable for BIR Report            
            AMModule.CompanyShortName = CStr(dicSettings("CompanyShortName").Value)
            AMModule.CompanyFullName = dicSettings("CompanyFullName").Value
            AMModule.CompanyAddress = dicSettings("CompanyAddress").Value
            AMModule.CompanyTinNumber = dicSettings("CompanyTinNumber").Value
            AMModule.CompanyTinExt = dicSettings("CompanyTinExt").Value
            AMModule.CompanyRDONumber = dicSettings("CompanyRDONumber").Value
            AMModule.AlphaTypeHeader = dicSettings("AlphaTypeHeader").Value
            AMModule.AlphaTypeDetails = dicSettings("AlphaTypeDetails").Value
            AMModule.AlphaTypeControl = dicSettings("AlphaTypeControl").Value
            AMModule.FormTypeCodeHeader = dicSettings("FormTypeCodeHeader").Value
            AMModule.FormTypeCodeDetails = dicSettings("FormTypeCodeDetails").Value
            AMModule.FormTypeCodeControl = dicSettings("FormTypeCodeControl").Value
            AMModule.FormTypeCodeBaseNumber = dicSettings("FormTypeCodeBaseNumber").Value
            AMModule.BIRWHTAgent = dicSettings("BIRWHTaxAgent").Value 'Bool True or False
            AMModule.CompanyBDOAccountName = dicSettings("CompanyBDOAccountName").Value
            AMModule.CompanyAccountNumber = dicSettings("CompanyAccNumber").Value

            'Transco
            AMModule.ParentFitID = dicSettings("ParentFITIDNo").Value
            AMModule.t13DigitDebitACNo = dicSettings("t13DigitDebitACNo").Value

            'WESM Bill Offset Date
            AMModule.WESMBillOffsetFromDate = dicSettings("WESMBillOffsetFromDate").Value
            AMModule.WESMBillOffsetToDate = dicSettings("WESMBillOffsetToDate").Value

            AMModule.OffsettingLimit = CInt(dicSettings("OffsettingLimit").Value) 'Offsetting Loop Limit
            AMModule.DateOfNoSP = CDate(dicSettings("DateOfNoSP").Value) 'Date Of Invoice does have no SP

            AMModule.FITParticipantCode = CStr(dicSettings("FITParticipantCode").Value) 'FIT participant

            AMModule.DefaultInterestDivisorValue = CDec(dicSettings("DefaultInterestDivisorValue").Value)            
            AMModule.OldWESMBILL = CLng(dicSettings("OldWESMBILL").Value)

            AMModule.SystemVersion = CStr(dicSettings("SystemVersion").Value)

            AMModule.PrintingMaxLimit = CInt(dicSettings("PrintingMaxLimit").Value)

            AMModule.RegionType = CStr(dicSettings("AMSRegion").Value)

            'BIR Ruling COnfig
            AMModule.BIRRulingPrefix = CStr(dicSettings("BIRRulingPrefix").Value)
            AMModule.BRImplementedBPNo = CLng(dicSettings("BRImplementedBPNo").Value)
            AMModule.AmountDiffValue = CDec(dicSettings("AmountDiffValue").Value)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub
#End Region

#Region "GeneratePrudentialMarginCall"
    Public Function GeneratePrudentialMarginCall(ByVal listMarginCall As List(Of PrudentialMarginCall), _
                                                 ByVal ListBP As List(Of CalendarBillingPeriod), _
                                                 ByVal Signatory As DocSignatories, ByVal dt As DataTable) As DataTable

        Try
            For Each item In listMarginCall
                Dim refNo = item.IDNumber.ParticipantID.ToString() & "-" & item.TransactionDate.ToString("MMddyyyy")

                Dim listPreviousWESMBil = (From x In item.ListOfPreviousWESMBill Join y In ListBP On _
                                           x.BillingPeriod Equals y.BillingPeriod Select x, y _
                                           Order By x.InvoiceDate).ToList()

                For Each itemPrevious In listPreviousWESMBil
                    Dim rowPrevious = dt.NewRow()

                    With item
                        rowPrevious("REF_NO") = refNo
                        rowPrevious("ID_NUMBER") = .IDNumber.IDNumber
                        rowPrevious("PARTICIPANT_ID") = .IDNumber.ParticipantID
                        rowPrevious("REP_NAME") = .IDNumber.Representative.GetFullName
                        rowPrevious("REP_POSITION") = .IDNumber.Representative.Position
                        rowPrevious("PARTICIPANT_NAME") = .IDNumber.FullName
                        rowPrevious("ADDRESS") = .IDNumber.ParticipantAddress
                        rowPrevious("TRANSACTION_DATE") = .TransactionDate.ToString("dd MMMM yyyy")
                        rowPrevious("MARGIN_CALL_DATE") = .MarginCallDate.ToString("dd MMMM yyyy")
                        rowPrevious("BILLING_PERIOD") = itemPrevious.x.InvoiceDate.ToString("MMM. yyyy")
                        rowPrevious("BILLING_MONTH") = itemPrevious.y.StartDate.ToString("dd MMM. yyyy") & " to " & _
                                                        itemPrevious.y.EndDate.ToString("dd MMM. yyyy") & "; " & _
                                                        itemPrevious.x.Remarks
                        rowPrevious("BILL_NUMBER") = "INV-" & itemPrevious.x.InvoiceNumber
                        rowPrevious("BILL_AMOUNT") = itemPrevious.x.Amount
                        rowPrevious("NO_OF_MONTHS") = .NoOfMonths
                        rowPrevious("TOTAL_ENERGY") = .Total
                        rowPrevious("MONTHLY_AVERAGE") = .MonthlyAverage
                        rowPrevious("DAILY_AVERAGE") = .DailyAverage
                        rowPrevious("MAXIMUM_NET_EXPOSURE") = .MaximumNetExposure
                        rowPrevious("ADVANCE_PAYMENT") = .AdvancePayment + .OutstandingBalance
                        rowPrevious("ACTUAL_NET_EXPOSURE") = .ActualNetExposure
                        rowPrevious("PRUDENTIAL_AMOUNT") = .PrudentialDeposit
                        rowPrevious("PRUDENTIAL_INTEREST") = .PrudentialInterest
                        rowPrevious("TOTAL_PRUDENTIAL") = .TotalPrudential
                        rowPrevious("TRADING_LIMIT") = .TradingLimit
                        rowPrevious("MARGIN_CALL_AMOUNT") = .MarginCallAmount
                        rowPrevious("MNE_VAR") = AMModule.MNEMultiplier
                        rowPrevious("TRADING_LIMIT_VAR") = AMModule.TradingLimitMultiplier
                        rowPrevious("PREPARED_BY") = AMModule.FullName
                        rowPrevious("PREPARED_BY_POSITION") = AMModule.Position
                        rowPrevious("SIGNATORY1") = Signatory.Signatory_1
                        rowPrevious("SIGNATORY1_POSITION") = Signatory.Position_1
                        rowPrevious("SIGNATORY2") = Signatory.Signatory_2
                        rowPrevious("SIGNATORY2_POSITION") = Signatory.Position_2
                        rowPrevious("SIGNATORY3") = Signatory.Signatory_3
                        rowPrevious("SIGNATORY3_POSITION") = Signatory.Position_3
                    End With
                    dt.Rows.Add(rowPrevious)
                Next

                Dim listCurrentWESMBil = (From x In item.ListOfWESMBills Join y In ListBP On _
                                          x.BillingPeriod Equals y.BillingPeriod Select x, y _
                                          Order By x.InvoiceDate).ToList()

                For Each itemCurrent In listCurrentWESMBil
                    Dim row = dt.NewRow()
                    With item
                        row("REF_NO") = refNo
                        row("ID_NUMBER") = .IDNumber.IDNumber
                        row("PARTICIPANT_ID") = .IDNumber.ParticipantID
                        row("REP_NAME") = .IDNumber.Representative.GetFullName
                        row("REP_POSITION") = .IDNumber.Representative.Position
                        row("PARTICIPANT_NAME") = .IDNumber.FullName
                        row("ADDRESS") = .IDNumber.ParticipantAddress
                        row("TRANSACTION_DATE") = .TransactionDate.ToString("dd MMMM yyyy")
                        row("MARGIN_CALL_DATE") = .MarginCallDate.ToString("dd MMMM yyyy")
                        row("BILLING_PERIOD") = itemCurrent.x.InvoiceDate.ToString("MMM. yyyy")
                        row("BILLING_MONTH") = itemCurrent.y.StartDate.ToString("dd MMM. yyyy") & " to " & _
                                               itemCurrent.y.EndDate.ToString("dd MMM. yyyy") & "; " & _
                                               itemCurrent.x.Remarks

                        row("BILL_NUMBER") = "INV-" & itemCurrent.x.InvoiceNumber
                        row("BILL_AMOUNT") = itemCurrent.x.Amount
                        row("NO_OF_MONTHS") = .NoOfMonths
                        row("TOTAL_ENERGY") = .Total
                        row("MONTHLY_AVERAGE") = .MonthlyAverage
                        row("DAILY_AVERAGE") = .DailyAverage
                        row("MAXIMUM_NET_EXPOSURE") = .MaximumNetExposure
                        row("ADVANCE_PAYMENT") = .AdvancePayment + .OutstandingBalance
                        row("ACTUAL_NET_EXPOSURE") = .ActualNetExposure
                        row("PRUDENTIAL_AMOUNT") = .PrudentialDeposit
                        row("PRUDENTIAL_INTEREST") = .PrudentialInterest
                        row("TOTAL_PRUDENTIAL") = .TotalPrudential
                        row("TRADING_LIMIT") = .TradingLimit
                        row("MARGIN_CALL_AMOUNT") = .MarginCallAmount
                        row("BILL_AMOUNT") = .TotalCurrentBill
                        row("MNE_VAR") = AMModule.MNEMultiplier
                        row("TRADING_LIMIT_VAR") = AMModule.TradingLimitMultiplier
                        row("PREPARED_BY") = AMModule.FullName
                        row("PREPARED_BY_POSITION") = AMModule.Position
                        row("SIGNATORY1") = Signatory.Signatory_1
                        row("SIGNATORY1_POSITION") = Signatory.Position_1
                        row("SIGNATORY2") = Signatory.Signatory_2
                        row("SIGNATORY2_POSITION") = Signatory.Position_2
                        row("SIGNATORY3") = Signatory.Signatory_3
                        row("SIGNATORY3_POSITION") = Signatory.Position_3
                    End With
                    dt.Rows.Add(row)
                Next

            Next

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return dt
    End Function

#End Region

#Region "GenerateDailyCollectionSummary"
    Public Function GenerateDailyCollectionSummary(ByVal BatchCode As String, ByVal JVNo As Long, ByVal ListCollections As List(Of Collection), _
                                                   ByVal ListParticipants As List(Of AMParticipants), ByVal Signatories As DocSignatories, _
                                                   ByVal dt As DataTable) As DataTable

        Dim listCollectionFinal = From x In ListCollections Join y In ListParticipants _
                                  On x.IDNumber Equals y.IDNumber _
                                  Select x, y.ParticipantID

        For Each item In listCollectionFinal
            Dim row = dt.NewRow()
            With item
                row("JV_NO") = Me.GenerateBIRDocumentNumber(JVNo, BIRDocumentsType.JournalVoucher)
                row("BATCH_CODE") = BatchCode
                row("ID_NUMBER") = .x.IDNumber
                row("PARTICIPANT_ID") = .ParticipantID
                row("COLLECTION_DATE") = .x.CollectionDate.ToString("MM/dd/yyyy")
                row("OR_NO") = Me.GenerateBIRDocumentNumber(.x.ORNo, BIRDocumentsType.OfficialReceipt)
                row("AMOUNT") = .x.CollectedAmount
                row("PREPARED_BY") = AMModule.FullName
                row("PREPARED_BY_POSITION") = AMModule.Position
                row("SIGNATORY_1") = Signatories.Signatory_1
                row("SIGNATORY_1_POSTION") = Signatories.Position_1
                row("SIGNATORY_2") = Signatories.Signatory_2
                row("SIGNATORY_2_POSITION") = Signatories.Position_2
                row("SIGNATORY_3") = Signatories.Signatory_3
                row("SIGNATORY_3_POSITION") = Signatories.Position_3
                dt.Rows.Add(row)
            End With
        Next

        Return dt
    End Function
#End Region

#Region "GenerateManualDMCMJournalVoucher"
    Public Function GenerateManualDMCMJournalVoucher(ByVal itemDMCM As DebitCreditMemo, _
                                                     ByVal SignatoriesJV As DocSignatories) As JournalVoucher
        Dim result As New JournalVoucher
        Dim jvDetails As New List(Of JournalVoucherDetails)

        For Each item In itemDMCM.DMCMDetails
            jvDetails.Add(New JournalVoucherDetails(1, item.AccountCode, item.Debit, item.Credit))
        Next

        'For Journal Voucher
        With result
            .JVNumber = 1
            .Status = 1
            .JVDate = Now()
            .PreparedBy = AMModule.FullName
            .CheckedBy = SignatoriesJV.Signatory_1
            .ApprovedBy = SignatoriesJV.Signatory_2
            .PostedType = EnumPostedType.MDMCM.ToString()
            .JVDetails = jvDetails
        End With

        Return result
    End Function
#End Region

#Region "GenerateManualDMCMGPPosted"
    Public Function GenerateManualDMCMGPPosted(ByVal jv As JournalVoucher, ByVal itemDMCM As DebitCreditMemo) As WESMBillGPPosted
        Dim result As New WESMBillGPPosted

        Dim totalDebit = (From x In jv.JVDetails _
                          Select x.Debit).Sum()

        Dim totalCredit = (From x In jv.JVDetails _
                           Select x.Credit).Sum()

        If totalDebit <> totalCredit Then
            Throw New ApplicationException("Debit amount and credit amount are not equal!")
        End If

        With result
            .BillingPeriod = itemDMCM.BillingPeriod
            .DueDate = itemDMCM.DueDate
            .Charge = itemDMCM.ChargeType
            .PostType = EnumPostedType.MDMCM.ToString()
            .DocumentAmount = totalDebit
        End With

        Return result
    End Function
#End Region

#Region "GenerateDMCMReport"
    Public Function GenerateDMCMReport(ByVal itemsDMCM As List(Of Long), ByVal dt As DataTable, _
                                       ByVal listDMCM As List(Of DebitCreditMemo), _
                                       ByVal listAccountCode As List(Of AccountingCode), _
                                       ByVal listParticipants As List(Of AMParticipants), _
                                       ByVal Signatory As DocSignatories) As DataTable
        Dim itemDetails As New List(Of DebitCreditMemoDetails)

        For Each item In listDMCM
            itemDetails.AddRange(item.DMCMDetails)
        Next
        itemDetails.TrimExcess()


        Dim items = (From w In listDMCM Join x In listParticipants On w.IDNumber Equals x.IDNumber _
                     Join y In itemDetails On w.DMCMNumber Equals y.DMCMNumber Join z In listAccountCode _
                     On y.AccountCode Equals z.AccountCode Join v In itemsDMCM On v Equals w.DMCMNumber _
                     Join u In listParticipants On y.IDNumber.IDNumber Equals u.IDNumber _
                     Select w.IDNumber, w.TransType, ParentID = x.ParticipantID, w.DMCMNumber, w.JVNumber, w.Particulars, _
                     w.ChargeType, x.ParticipantAddress, x.FullName, u.ParticipantID, y.AccountCode, _
                     w.EWT, w.EWV, w.Vatable, w.VAT, w.VATExempt, w.VatZeroRated, w.TotalAmountDue, _
                     y.InvDMCMNo, y.SummaryType, y.Debit, y.Credit, z.Description, w.UpdatedDate).ToList()

        For Each item In items
            With item
                Dim row As DataRow = dt.NewRow()
                
                row("ID_NUMBER") = .IDNumber.ToString() & " / " & .ParentID
                row("ADDRESS") = .ParticipantAddress
                row("DMCM_NO") = Me.GenerateBIRDocumentNumber(.DMCMNumber, BIRDocumentsType.DMCM)
                row("JV_NO") = Me.GenerateBIRDocumentNumber(.JVNumber, BIRDocumentsType.JournalVoucher)
                row("PARTICULARS") = .Particulars
                row("ACCOUNT_CODE") = .AccountCode

                If .InvDMCMNo <> "" Then
                    row("DESCRIPTION") = "(" & .SummaryType.ToString() & "-" & .InvDMCMNo.ToString() & ") " & .Description
                Else
                    row("DESCRIPTION") = .Description
                End If

                row("PREPARED_BY") = AMModule.FullName
                row("CHECKED_BY") = Signatory.Signatory_1
                row("APPROVED_BY") = Signatory.Signatory_2
                row("PARTICIPANT_NAME") = .FullName
                row("DR_AMOUNT") = .Debit
                row("CR_AMOUNT") = .Credit
                row("PARTICIPANT_ID") = .ParticipantID
                row("EWT") = .EWT
                row("EWV") = .EWV
                row("VATABLE") = .Vatable
                row("VAT") = .VAT
                row("VAT_EXEMPT_SALE") = .VATExempt
                row("VAT_ZERO") = .VatZeroRated
                row("TOTAL_AMOUNT_DUE") = .TotalAmountDue
                row("UPDATED_DATE") = .UpdatedDate
                dt.Rows.Add(row)
            End With
        Next
        dt.AcceptChanges()

        Return dt
    End Function
#End Region

#Region "GeneratePrudentialMarginCall"
    Public Function GeneratePrudentialMarginCallSummaryReport(ByVal listMarginCall As List(Of PrudentialMarginCallSummary), _
                                                              ByVal Signatory As DocSignatories, ByVal dt As DataTable) As DataTable

        Try
            For Each item In listMarginCall
                Dim row = dt.NewRow()
                With item
                    row("ID_NUMBER") = .IDNumber.IDNumber
                    row("PARTICIPANT_ID") = .IDNumber.ParticipantID
                    row("PARTICIPANT_NAME") = .IDNumber.FullName
                    row("TRANSACTION_DATE") = .TransactionDate.ToString("dd MMMM yyyy")
                    row("MARGIN_CALL_DATE") = .MarginCallDate.ToString("dd MMMM yyyy")
                    row("NO_OF_MONTHS") = .NoOfMonths
                    row("TOTAL") = .Total
                    row("MONTHLY_AVERAGE") = .MonthlyAverage
                    row("DAILY_AVERAGE") = .DailyAverage
                    row("MAXIMUM_NET_EXPOSURE") = .MaximumNetExposure * -1D
                    row("ADVANCE_PAYMENT") = .AdvancePayment
                    row("ACTUAL_NET_EXPOSURE") = .ActualNetExposure
                    row("PRUDENTIAL_AMOUNT") = .PrudentialDeposit
                    row("PRUDENTIAL_INTEREST") = .PrudentialInterest
                    row("TOTAL_PRUDENTIAL") = .TotalPrudential
                    row("TRADING_LIMIT") = .TradingLimit
                    row("MARGIN_CALL_AMOUNT") = .MarginCallAmount
                    row("PREPARED_BY") = AMModule.FullName
                    row("PREPARED_BY_POSITION") = AMModule.Position
                    row("SIGNATORY1") = Signatory.Signatory_1
                    row("SIGNATORY1_POSITION") = Signatory.Position_1
                    row("SIGNATORY2") = Signatory.Signatory_2
                    row("SIGNATORY2_POSITION") = Signatory.Position_2
                    row("SIGNATORY3") = Signatory.Signatory_3
                    row("SIGNATORY3_POSITION") = Signatory.Position_3
                End With
                dt.Rows.Add(row)

            Next

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return dt
    End Function

#End Region

#Region "GeneratePrudentialDrawdownReport"
    Public Function GeneratePrudentialDrawdownReport(ByVal listDrawdown As List(Of PrudentialDrawdown), _
                                                     ByVal Signatory As DocSignatories, ByVal dt As DataTable) As DataTable

        Try
            For Each item In listDrawdown
                Dim row = dt.NewRow()
                With item
                    row("DRAWDOWN_DATE") = .TransactionDate.ToString("dd MMMM yyyy")
                    row("ID_NUMBER") = .IDNumber.IDNumber
                    row("PARTICIPANT_ID") = .IDNumber.ParticipantID
                    row("PARTICIPANT_NAME") = .IDNumber.FullName
                    row("ADDRESS") = .IDNumber.ParticipantAddress
                    row("REP_NAME") = .IDNumber.Representative.GetFullName
                    row("REP_POSITION") = .IDNumber.Representative.Position
                    row("DRAWDOWN_AMOUNT") = .DrawdownAmount * -1D
                    row("PRUDENTIAL_AMOUNT") = .RemaningPrudential
                    row("DUE_DATE") = .DueDate.ToString("dd MMMM yyyy")
                    row("PREPARED_BY") = AMModule.FullName
                    row("PREPARED_BY_POSITION") = AMModule.Position
                    row("SIGNATORY1") = Signatory.Signatory_1
                    row("SIGNATORY1_POSITION") = Signatory.Position_1
                    row("SIGNATORY2") = Signatory.Signatory_2
                    row("SIGNATORY2_POSITION") = Signatory.Position_2
                    row("SIGNATORY3") = Signatory.Signatory_3
                    row("SIGNATORY3_POSITION") = Signatory.Position_3
                End With
                dt.Rows.Add(row)

            Next

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return dt
    End Function

#End Region

#Region "Remove Comma for CSV Export - Datatable input"
    Public Function RemoveCommaForCSVExport(ByVal dt As DataTable) As DataTable
        For Each _row As DataRow In dt.Rows
            For Each _col As DataColumn In dt.Columns
                If InStrRev(_row(_col.ColumnName).ToString, ",") > 0 Then
                    _row(_col.ColumnName) = Replace(_row(_col.ColumnName).ToString, ",", "")
                End If
            Next
        Next
        Return dt
    End Function

    Public Function PlaceCommaForDisplay(ByVal dt As DataTable) As DataTable
        For Each _row As DataRow In dt.Rows
            For Each _col As DataColumn In dt.Columns
                If _col.ColumnName.Contains("Number") = False And _col.ColumnName.Contains("Period") And _col.ColumnName.Contains("Date") Then
                    If IsNumeric(_row(_col.ColumnName).ToString) Then
                        _row(_col.ColumnName) = FormatNumber(_row(_col.ColumnName), 2, TriState.True, TriState.True)
                    End If
                End If
            Next
        Next
        Return dt
    End Function

#End Region

#Region "GenerateNetSettlementSurplusAdjustmentReport"
    Public Function GenerateNetSettlementSurplusAdjustmentReport(ByVal BillingPeriodMain As String, _
                                                                 ByVal BillingPeriodValue1 As String, _
                                                                 ByVal BillingPeriodValue2 As String, _
                                                                 ByVal BillingPeriodValue3 As String, _
                                                                 ByVal ListNSSRA As List(Of NetSettlementSurplusAdjustmentReport), _
                                                                 ByVal dt As DataTable) As DataTable
        For Each item In ListNSSRA
            Dim row = dt.NewRow()

            With item
                row("BILLING_PERIOD_VALUE1") = BillingPeriodValue1
                row("BILLING_PERIOD_VALUE2") = BillingPeriodValue2
                row("BILLING_PERIOD_VALUE3") = BillingPeriodValue3
                row("BILLING_PERIOD_MAIN") = BillingPeriodMain
                row("ID_NUMBER") = .IDNumber.IDNumber
                row("PARTICIPANT_ID") = .IDNumber.ParticipantID
                row("BILLING_PERIOD1") = .BillingPeriod1
                row("BILLING_PERIOD2") = .BillingPeriod2
                row("BILLING_PERIOD3") = .BillingPeriod3
                row("INVOICE_NUMBER_NSS1") = .InvoiceNumberNSS1
                row("INVOICE_NUMBER_NSS2") = .InvoiceNumberNSS2
                row("INVOICE_NUMBER_NSS3") = .InvoiceNumberNSS3
                row("NSS_AMOUNT1") = .NSSAmount1
                row("NSS_AMOUNT2") = .NSSAmount2
                row("NSS_AMOUNT3") = .NSSAmount3
                row("INVOICE_NUMBER_NSSRA1") = .InvoiceNumberNSSRA1
                row("INVOICE_NUMBER_NSSRA2") = .InvoiceNumberNSSRA2
                row("INVOICE_NUMBER_NSSRA3") = .InvoiceNumberNSSRA3
                row("NSSRA_AMOUNT1") = .NSSRAAmount1
                row("NSSRA_AMOUNT2") = .NSSRAAmount2
                row("NSSRA_AMOUNT3") = .NSSRAAmount3
                row("CATEGORY") = .CategoryType
            End With
            dt.Rows.Add(row)

        Next
        dt.AcceptChanges()

        Return dt
    End Function
#End Region

#Region "GenerateOfficialReceiptReportForCollection"
    Public Function GenerateOfficialReceiptReportForCollection(ByVal itemCollection As Collection, _
                                                               ByVal listMonitoring As List(Of CollectionMonitoring), _
                                                               ByVal itemOR As OfficialReceiptMain, _
                                                               ByVal listParticipants As List(Of AMParticipants), _
                                                               ByVal dt As DataTable) As DataTable

        Dim EWT As Decimal = 0, Vatable As Decimal = 0, Vat As Decimal = 0
        Dim VatExempt As Decimal = 0, Total As Decimal = 0, EWV As Decimal = 0, totalPEMC As Decimal = 0

        itemCollection.CollectedAmount *= -1D

        For Each item In itemCollection.ListOfCollectionAllocation
            Select Case item.CollectionType
                Case EnumCollectionType.WithholdingTaxOnMF
                    EWT += item.Amount

                Case EnumCollectionType.WithholdingTaxOnDefaultInterest
                    EWT += item.Amount

                Case EnumCollectionType.WithholdingVatonMF
                    EWV += item.Amount

                Case EnumCollectionType.WithholdingVatonDefaultInterest
                    EWV += item.Amount

                Case EnumCollectionType.MarketFees, EnumCollectionType.DefaultInterestOnMF
                    Vatable += Math.Abs(item.Amount)

                Case EnumCollectionType.VatOnMarketFees, EnumCollectionType.DefaultInterestOnVatOnMF
                    Vat += Math.Abs(item.Amount)

                Case EnumCollectionType.Energy, EnumCollectionType.DefaultInterestOnEnergy, EnumCollectionType.VatOnEnergy
                    VatExempt += Math.Abs(item.Amount)

            End Select
        Next

        'Get the  Replenishment, Excess Collection, Held Collection, Transfer to PEMC Account
        VatExempt += (From x In listMonitoring _
                      Where x.TransType <> EnumCollectionMonitoringType.TransferToPRDrawdown _
                      And x.TransType <> EnumCollectionMonitoringType.TransferToPEMCAccount _
                      Select x.Amount).Sum()

        'Get the Total Tranfer to PEMC Account
        totalPEMC = (From x In listMonitoring _
                     Where x.TransType = EnumCollectionMonitoringType.TransferToPEMCAccount _
                     Select x.Amount).Sum()

        'Deduct the HeldCollection in Vat Exempt
        If VatExempt <> 0 Then
            VatExempt -= itemCollection.CollectedHeld
        End If

        'Get the participant
        Dim itemParticipant = (From x In listParticipants _
                               Where x.IDNumber = itemCollection.IDNumber _
                               Select x).First()
        Dim row = dt.NewRow()

        row("OR_NO") = itemOR.ORNo
        row("OR_DATE") = itemOR.ORDate
        row("RECEIVED_FROM") = itemParticipant.FullName
        row("ADDRESS") = itemParticipant.ParticipantAddress
        row("AMOUNT") = Total
        row("REMARKS") = itemOR.Remarks
        If EWT <> 0 Then
            row("REF1") = "EWT"
            row("REF1_AMOUNT") = EWT * -1D
        End If

        If EWV <> 0 Then
            row("REF2") = "WVAT"
            row("REF2_AMOUNT") = EWV * -1D
        End If

        row("VATABLE") = Vatable
        row("VAT_EXEMPT_SALE") = VatExempt
        row("VAT") = Vat
        row("TOTAL_PAYMENT") = itemCollection.CollectedAmount - totalPEMC

        Dim listParseValues As Integer() = {80, 80, 80, 80}
        Dim listAmountWords = Me.ParseString(itemCollection.CollectedAmount - totalPEMC, listParseValues.ToList(), True)

        For index As Integer = 0 To listAmountWords.Count - 1
            row("AMOUNT_WORDS" & (index + 1).ToString()) = listAmountWords(index)
        Next

        dt.Rows.Add(row)
        dt.AcceptChanges()

        Return dt
    End Function
#End Region

#Region "GenerateDeleteORJournalVoucher"
    Public Function GenerateDeleteORJournalVoucher(ByVal itemCollection As Collection, _
                                                   ByVal SignatoriesJV As DocSignatories) As JournalVoucher
        Dim result As New JournalVoucher
        Dim jvDetails As New List(Of JournalVoucherDetails)

        jvDetails.Add(New JournalVoucherDetails(1, AMModule.CashInbankSettlementcode, _
                                                0, Math.Abs(itemCollection.CollectedAmount)))
        jvDetails.Add(New JournalVoucherDetails(1, AMModule.ClearingAccountCode, _
                                                Math.Abs(itemCollection.CollectedAmount), 0))

        'For Journal Voucher
        With result
            .JVNumber = 1
            .Status = 1
            .JVDate = Now()
            .PreparedBy = AMModule.FullName
            .CheckedBy = SignatoriesJV.Signatory_1
            .ApprovedBy = SignatoriesJV.Signatory_2
            .PostedType = EnumPostedType.DCC.ToString()
            .JVDetails = jvDetails
        End With

        Return result
    End Function
#End Region

#Region "GenerateDeleteORGPPosted"
    Public Function GenerateDeleteORGPPosted(ByVal jv As JournalVoucher) As WESMBillGPPosted
        Dim result As New WESMBillGPPosted

        Dim totalDebit = (From x In jv.JVDetails _
                          Select x.Debit).Sum()

        Dim totalCredit = (From x In jv.JVDetails _
                           Select x.Credit).Sum()

        If totalDebit <> totalCredit Then
            Throw New ApplicationException("Debit amount and credit amount are not equal!")
        End If

        With result
            .PostType = EnumPostedType.DCC.ToString()
            .DocumentAmount = totalDebit
        End With

        Return result
    End Function
#End Region

#Region "GenerateOfficialReceiptReport"
    Public Function GenerateOfficialReceiptReport(ByVal OrPEMCHeader As Integer, ByVal itemOR As OfficialReceiptReportMain, itemParticipant As AMParticipants, ByVal dt As DataTable) As DataTable
        If itemOR.ListOfficialReceiptReportDetails.Count = 0 Then
            Dim row = dt.NewRow()
            row("OR_PEMC_HEADER") = OrPEMCHeader
            row("OR_NO") = Me.GenerateBIRDocumentNumber(itemOR.ItemOfficialReceipt.ORNo, BIRDocumentsType.OfficialReceipt) 'Format(itemOR.ItemOfficialReceipt.ORNo, "0000000") '9/4/2014 changed by lance
            row("OR_DATE") = itemOR.ItemOfficialReceipt.ORDate.ToShortDateString
            row("RECEIVED_FROM") = itemParticipant.FullName
            row("BUSINESS_STYLE") = itemParticipant.BusinessStyle
            row("ADDRESS") = itemParticipant.ParticipantAddress
            row("TIN") = itemParticipant.TIN
            row("TOTAL_AMOUNT") = itemOR.TotalPayment
            row("AMOUNT_WORDS") = itemOR.TotalPaymentInWords.Trim()
            row("DEFAULT_INTEREST_RATE") = itemOR.DefaultInterestRate
            row("VAT_EXEMPT") = itemOR.ItemOfficialReceipt.VATExempt
            row("VATABLE") = Math.Abs(itemOR.ItemOfficialReceipt.Vatable)
            row("VAT") = Math.Abs(itemOR.ItemOfficialReceipt.VAT)
            row("VAT_ZERO_RATED") = itemOR.ItemOfficialReceipt.VATZeroRated
            row("OTHERS") = itemOR.ItemOfficialReceipt.Others
            row("WITHHOLDING_TAX") = itemOR.ItemOfficialReceipt.WithholdingTax 'IIf(itemOR.ItemOfficialReceipt.WithholdingTax < 0, itemOR.ItemOfficialReceipt.WithholdingTax, itemOR.ItemOfficialReceipt.WithholdingTax * -1)
            row("WITHHOLDING_VAT") = itemOR.ItemOfficialReceipt.WithholdingVAT 'IIf(itemOR.ItemOfficialReceipt.WithholdingVAT < 0, itemOR.ItemOfficialReceipt.WithholdingVAT, itemOR.ItemOfficialReceipt.WithholdingVAT * -1)
            row("TRANSACTION_TYPE") = itemOR.ItemOfficialReceipt.TransactionType
            row("BIR_PERMIT") = AMModule.BIRPermitNumber
            row("OR_PREFIX") = AMModule.ORNumberPrefix
            row("GEN_LOAD") = itemParticipant.GenLoad
            row("AMOUNT_DETAILS") = 0
            row("VAT_DETAILS") = 0
            row("DEFAULT_INTEREST_AMOUNT_DETAILS") = 0
            row("WITHHOLDING_TAX_AMOUNT_DETAILS") = 0
            row("WITHHOLDING_VAT_AMOUNT_DETAILS") = 0
            row("TOTAL_AMOUNT_DETAILS") = Math.Abs(0)

            dt.Rows.Add(row)
        Else
            For Each item In itemOR.ListOfficialReceiptReportDetails
                Dim row = dt.NewRow()
                row("OR_PEMC_HEADER") = OrPEMCHeader
                row("OR_NO") = Me.GenerateBIRDocumentNumber(itemOR.ItemOfficialReceipt.ORNo, BIRDocumentsType.OfficialReceipt) ' Format(itemOR.ItemOfficialReceipt.ORNo, "0000000") '9/4/2014 changed by lance
                row("OR_DATE") = itemOR.ItemOfficialReceipt.ORDate.ToShortDateString
                row("RECEIVED_FROM") = itemParticipant.FullName
                row("BUSINESS_STYLE") = itemParticipant.BusinessStyle
                row("ADDRESS") = itemParticipant.ParticipantAddress
                row("TIN") = itemParticipant.TIN
                row("TOTAL_AMOUNT") = itemOR.TotalPayment
                row("AMOUNT_WORDS") = itemOR.TotalPaymentInWords
                row("DEFAULT_INTEREST_RATE") = itemOR.DefaultInterestRate
                row("VAT_EXEMPT") = itemOR.ItemOfficialReceipt.VATExempt
                row("VATABLE") = Math.Abs(itemOR.ItemOfficialReceipt.Vatable)
                row("VAT") = Math.Abs(itemOR.ItemOfficialReceipt.VAT)
                row("VAT_ZERO_RATED") = itemOR.ItemOfficialReceipt.VATZeroRated
                row("OTHERS") = itemOR.ItemOfficialReceipt.Others
                row("WITHHOLDING_TAX") = itemOR.ItemOfficialReceipt.WithholdingTax 'IIf(itemOR.ItemOfficialReceipt.WithholdingTax < 0, itemOR.ItemOfficialReceipt.WithholdingTax * -1, itemOR.ItemOfficialReceipt.WithholdingTax)
                row("WITHHOLDING_VAT") = itemOR.ItemOfficialReceipt.WithholdingVAT 'IIf(itemOR.ItemOfficialReceipt.WithholdingVAT < 0, itemOR.ItemOfficialReceipt.WithholdingVAT * -1, itemOR.ItemOfficialReceipt.WithholdingVAT)
                row("DOC_NO") = item.DocumentNo
                row("DOC_DATE") = item.DocumentDate
                row("DUE_DATE") = item.DueDate
                row("TRANSACTION_TYPE") = item.TransactionType
                row("AMOUNT_DETAILS") = item.Amount
                row("VAT_DETAILS") = item.VAT
                row("DEFAULT_INTEREST_AMOUNT_DETAILS") = item.DefaultInterest
                row("WITHHOLDING_TAX_AMOUNT_DETAILS") = item.WithholdingTax
                row("WITHHOLDING_VAT_AMOUNT_DETAILS") = item.WitholdingVAT
                row("TOTAL_AMOUNT_DETAILS") = Math.Abs(item.Total)
                row("BIR_PERMIT") = AMModule.BIRPermitNumber
                row("OR_PREFIX") = AMModule.ORNumberPrefix
                'row("TRANSACTION_TYPE") = itemOR.ItemOfficialReceipt.TransactionType
                row("GEN_LOAD") = itemParticipant.GenLoad

                dt.Rows.Add(row)
            Next

        End If

        Return dt
    End Function
#End Region

#Region "GenerateORDetails - Payment"
    Function GeneratePaymentMainORDetails(ByVal lstWESMSalesAndPurchased As List(Of WESMBillSalesAndPurchased), _
                                   ByVal lstWESMSummary As List(Of WESMBillSummary), _
                                   ByVal lstPaymentPerAccount As List(Of PaymentAllocationAccount), _
                                   ByVal ORForUpdate As OfficialReceiptMain) As OfficialReceiptMain
        Dim totAmount As Decimal = 0
        Dim MFWtax As Decimal = 0
        Dim MFWVat As Decimal = 0
        Dim others As Decimal = 0
        Dim Vatable As Decimal = 0
        Dim VAT As Decimal = 0
        Dim VATZeroRated As Decimal = 0
        Dim CollectionType As New EnumCollectionType
        'Dim ORMain As New OfficialReceiptMain
        Dim lstORSummary As New List(Of OfficialReceiptSummary)


        For Each itmPerAccount In lstPaymentPerAccount
            Dim _itmPerAccount = itmPerAccount
            Select Case itmPerAccount.PaymentType

                Case EnumPaymentType.UnpaidMFWHTax, EnumPaymentType.WHTaxDefault
                    MFWtax += itmPerAccount.PaymentAmount
                    CollectionType = EnumCollectionType.WithholdingTaxOnMF
                Case EnumPaymentType.UnpaidMFWHVAT, EnumPaymentType.WHVATDefault
                    MFWVat += itmPerAccount.PaymentAmount
                    CollectionType = EnumCollectionType.WithholdingVatonMF
                Case EnumPaymentType.UnpaidMFDefault, EnumPaymentType.UnpaidMFVDefault, _
                EnumPaymentType.OffsetToCurrentReceivableEnergyDefault, EnumPaymentType.OffsetOfPreviousReceivableEnergyDefault

                    others += itmPerAccount.PaymentAmount
                    CollectionType = EnumCollectionType.DefaultInterestOnEnergy
                Case EnumPaymentType.UnpaidMF
                    Vatable += itmPerAccount.PaymentAmount
                    CollectionType = EnumCollectionType.MarketFees
                Case EnumPaymentType.UnpaidMFV
                    VAT += itmPerAccount.PaymentAmount
                    CollectionType = EnumCollectionType.VatOnMarketFees
                Case EnumPaymentType.OffsetOfPreviousReceivableEnergy, EnumPaymentType.OffsetToCurrentReceivableEnergy
                    If _itmPerAccount.WESMBillSummary.SummaryType = EnumSummaryType.INV Then
                        Dim InvSalesAndPurchase = (From x In lstWESMSalesAndPurchased _
                                               Where x.InvoiceNumber = _itmPerAccount.WESMBillSummary.INVDMCMNo _
                                               Select x).FirstOrDefault()

                        Dim tempVatable As Decimal = 0
                        Dim tempVatZeroRated As Decimal = 0

                        If tempVatable + tempVatZeroRated <> _itmPerAccount.PaymentAmount Then
                            If tempVatable <> 0 Then
                                Vatable += tempVatable - (tempVatable + tempVatZeroRated - _itmPerAccount.PaymentAmount)
                            Else
                                VATZeroRated += tempVatZeroRated - (tempVatable + tempVatZeroRated - _itmPerAccount.PaymentAmount)
                            End If
                        Else
                            Vatable += tempVatable
                            VATZeroRated += tempVatZeroRated
                        End If
                        CollectionType = EnumCollectionType.Energy
                    End If
                Case EnumPaymentType.OffsetOfPreviousReceivableVAT, EnumPaymentType.OffsetToCurrentReceivableVAT
                    VAT += _itmPerAccount.PaymentAmount
                    CollectionType = EnumCollectionType.VatOnEnergy
            End Select

            lstORSummary.Add(New OfficialReceiptSummary(ORForUpdate.ORNo, _itmPerAccount.WESMBillSummary.WESMBillSummaryNo, _
                                                                     _itmPerAccount.WESMBillSummary.NewDueDate, _itmPerAccount.PaymentAmount, _
                                                                    CollectionType))
        Next

        With ORForUpdate
            .VAT += Math.Abs(VAT)
            .Vatable += Math.Abs(Vatable)
            .Others += Math.Abs(others)
            .WithholdingTax += Math.Abs(MFWtax) * -1D
            .WithholdingVAT += Math.Abs(MFWVat) * -1D
            .VATZeroRated += Math.Abs(VATZeroRated)
            '.Amount = .VAT + .Vatable + .Others + .WithholdingTax + .WithholdingVAT + .VATZeroRated
        End With

        'ORMain = ORForUpdate
        With ORForUpdate
            .ListORSummary = New List(Of OfficialReceiptSummary)
            .ListORSummary.AddRange(lstORSummary)
        End With
        'ORMain.ListORSummary.AddRange(lstORSummary)

        Return ORForUpdate
    End Function
#End Region

#Region "GenerateORDetails - Payment New"
    Function GeneratePaymentNewORMainDetails(ByVal WESMSummary As List(Of WESMBillSummary), _
                                            ByVal PaymentPerAccount As List(Of ARCollection)) As OfficialReceiptMain
        Dim TotalAmount As Decimal = 0
        Dim MFWTAX As Decimal = 0
        Dim MFWVAT As Decimal = 0
        Dim Others As Decimal = 0
        Dim Vatable As Decimal = 0
        Dim VAT As Decimal = 0
        Dim VATZeroRated As Decimal = 0




        Return Nothing
    End Function
#End Region

#Region "GenerateDMCMReport1"
    Public Function GenerateDMCMReport1(ByVal itemsDMCM As List(Of Long), ByVal dt As DataTable, _
                                        ByVal listDMCM As List(Of DebitCreditMemo), _
                                        ByVal listAccountCode As List(Of AccountingCode), _
                                        ByVal listParticipants As List(Of AMParticipants), _
                                        ByVal Signatory As DocSignatories, _
                                        ByVal listWESMBillSalesAndPurchase As List(Of WESMBillSalesAndPurchased)) As DataTable

        Dim itemDetails As New List(Of DebitCreditMemoDetails)
        For Each item In listDMCM
            If item.ChargeType = EnumChargeType.E Then

                If item.TransType = EnumDMCMTransactionType.WESMBillP2PC2COffsetting _
                    Or item.TransType = EnumDMCMTransactionType.WESMBillP2COffsetting _
                    Or item.TransType = EnumDMCMTransactionType.CollectionDrawdown _
                    Or item.TransType = EnumDMCMTransactionType.PaymentChildToParentOffsetting _
                    Or item.TransType = EnumDMCMTransactionType.PaymentOffsettingOfReceivableEnergyClearing _
                    Or item.TransType = EnumDMCMTransactionType.ManualDMCM Then

                    Dim Vatable As Decimal = 0, VATZeroRated As Decimal = 0

                    For Each itemDMCMDetails In item.DMCMDetails
                        Dim selectedDMCMDetails = itemDMCMDetails

                        If selectedDMCMDetails.InvDMCMNo <> "" And selectedDMCMDetails.SummaryType = EnumSummaryType.INV _
                            And selectedDMCMDetails.IsComputed = EnumDMCMComputed.Compute Then

                            Try
                                'Get the sales and purchase
                                Dim itemSalesAndPurchases = (From x In listWESMBillSalesAndPurchase _
                                                             Where x.InvoiceNumber = selectedDMCMDetails.InvDMCMNo _
                                                             Select x).First()

                                Dim tempVatable = Math.Round((selectedDMCMDetails.Debit + selectedDMCMDetails.Credit) * Math.Abs(itemSalesAndPurchases.VatableRatio), 2)
                                Dim tempVatZeroRated = Math.Round((selectedDMCMDetails.Debit + selectedDMCMDetails.Credit) * Math.Abs(itemSalesAndPurchases.ZeroRatedRatio), 2)

                                If tempVatable + tempVatZeroRated <> (selectedDMCMDetails.Debit + selectedDMCMDetails.Credit) Then
                                    If tempVatable <> 0 Then
                                        Vatable += tempVatable - (tempVatable + tempVatZeroRated - (selectedDMCMDetails.Debit + selectedDMCMDetails.Credit))
                                        VATZeroRated += tempVatZeroRated
                                    Else
                                        Vatable += tempVatable
                                        VATZeroRated += tempVatZeroRated - (tempVatable + tempVatZeroRated - (selectedDMCMDetails.Debit + selectedDMCMDetails.Credit))
                                    End If
                                Else
                                    Vatable += tempVatable
                                    VATZeroRated += tempVatZeroRated
                                End If
                            Catch ex As Exception
                                Vatable += (selectedDMCMDetails.Debit + selectedDMCMDetails.Credit)
                            End Try
                        End If
                    Next

                    item.Vatable = Vatable
                    item.VatZeroRated = VATZeroRated
                End If
            End If
        Next


        For Each item In listDMCM
            itemDetails.AddRange(item.DMCMDetails)
        Next
        itemDetails.TrimExcess()


        'Dim items = (From w In listDMCM Join x In listParticipants On w.IDNumber Equals x.IDNumber _
        '             Join y In itemDetails On w.DMCMNumber Equals y.DMCMNumber Join z In listAccountCode _
        '             On y.AccountCode Equals z.AccountCode Join v In itemsDMCM On v Equals w.DMCMNumber _
        '             Join u In listParticipants On y.IDNumber.IDNumber Equals u.IDNumber _
        '             Select w.IDNumber, w.TransType, x.TIN, ParentID = x.ParticipantID, w.DMCMNumber, w.JVNumber, w.Particulars, _
        '             w.ChargeType, x.ParticipantAddress, x.FullName, u.ParticipantID, y.AccountCode, _
        '             w.EWT, w.EWV, w.Vatable, w.VAT, w.VATExempt, w.VatZeroRated, w.Others, w.TotalAmountDue, _
        '             y.InvDMCMNo, y.SummaryType, y.Debit, y.Credit, z.Description).ToList()

        Dim items = (From w In listDMCM Join x In listParticipants On w.IDNumber Equals x.IDNumber _
                     Join y In itemDetails On w.DMCMNumber Equals y.DMCMNumber Join z In listAccountCode _
                     On y.AccountCode Equals z.AccountCode Join v In itemsDMCM On v Equals w.DMCMNumber _
                     Select w.IDNumber, w.TransType, x.TIN, ParentID = x.ParticipantID, w.DMCMNumber, w.JVNumber, w.Particulars, _
                     w.ChargeType, x.ParticipantAddress, x.FullName, y.IDNumber.ParticipantID, y.AccountCode, _
                     w.EWT, w.EWV, w.Vatable, w.VAT, w.VATExempt, w.VatZeroRated, w.Others, w.TotalAmountDue, _
                     y.InvDMCMNo, y.SummaryType, y.Debit, y.Credit, z.Description, w.UpdatedDate, x.BusinessStyle).ToList()

        For Each item In items
            With item
                Dim row As DataRow = dt.NewRow()
                row("ID_NUMBER") = .IDNumber & " / " & .ParentID
                row("BUSINESS_STYLE") = .BusinessStyle
                row("ADDRESS") = .ParticipantAddress
                row("PARTICIPANT_TIN") = .TIN
                row("DMCM_NO") = Me.GenerateBIRDocumentNumber(.DMCMNumber, BIRDocumentsType.DMCM)
                row("JV_NO") = Me.GenerateBIRDocumentNumber(.JVNumber, BIRDocumentsType.JournalVoucher)
                row("PARTICULARS") = .Particulars
                row("ACCOUNT_CODE") = .AccountCode

                If .InvDMCMNo <> "0" And Trim(.InvDMCMNo) <> "" Then
                    row("DESCRIPTION") = "(" & If(.SummaryType = EnumSummaryType.DMCM, Me.GenerateBIRDocumentNumber(CLng(.InvDMCMNo), BIRDocumentsType.DMCM) & .InvDMCMNo, .InvDMCMNo) & ") " & .Description
                Else
                    row("DESCRIPTION") = .Description
                End If

                row("PREPARED_BY") = AMModule.FullName
                row("CHECKED_BY") = Signatory.Signatory_1
                row("APPROVED_BY") = Signatory.Signatory_2
                row("POSITION1") = AMModule.Position
                row("POSITION2") = Signatory.Position_1
                row("POSITION3") = Signatory.Position_2
                row("PARTICIPANT_NAME") = .FullName
                row("DR_AMOUNT") = .Debit
                row("CR_AMOUNT") = .Credit
                row("PARTICIPANT_ID") = .ParticipantID
                row("EWT") = .EWT
                row("EWV") = .EWV
                row("VATABLE") = .Vatable
                row("VAT") = .VAT
                row("VAT_EXEMPT_SALE") = .VATExempt
                row("VAT_ZERO") = .VatZeroRated
                row("OTHERS") = .Others
                row("TOTAL_AMOUNT_DUE") = .TotalAmountDue
                row("BIR_VALUE") = AMModule.BIRPermitNumber
                row("UPDATED_DATE") = .UpdatedDate
                dt.Rows.Add(row)
            End With
        Next
        dt.AcceptChanges()

        Return dt

        Return dt
    End Function
#End Region

#Region "SaveLogFile"
    Public Function SaveLogFile(ByVal action As String) As Boolean
        Dim Path = My.Application.Info.DirectoryPath & "\logs " & _
                                                   CDate(FormatDateTime(Now(), DateFormat.ShortDate)).ToString("MMddyyyy") & ".csv"
        Dim sw As StreamWriter = Nothing
        Try
            sw = New StreamWriter(Path, True)
        Catch ex As Exception
            Console.Write("Error in saving log file: " & ex.Message)
        End Try

        If Not Convert.IsDBNull(action) Then
            Try
                sw.Write(sw.NewLine)
                sw.Write(action)
            Catch ex As Exception
                Console.Write("Error in saving log file: " & ex.Message)
                Return False
            End Try
        End If
        sw.Close()
        Return True
    End Function
#End Region

#Region "GeneratePaymentORMain"
    Public Sub GeneratePaymentORMain(ByRef itemOfficialReceiptMain As OfficialReceiptMain, _
                                     ByVal listOfficialReceiptSummary As List(Of OfficialReceiptReportRawDetailsNew), _
                                     ByVal listSalesAndPurchases As List(Of WESMBillSalesAndPurchased))

        Dim result As New List(Of OfficialReceiptMain)

        Dim VatExempt As Decimal = 0, Vatable As Decimal = 0, VAT As Decimal = 0
        Dim VatZeroRated As Decimal = 0, Others As Decimal = 0, WithholdTax As Decimal = 0, WithholdVat As Decimal = 0


        For Each item In listOfficialReceiptSummary
            Dim selectedItem = item
            WithholdTax += item.WithHoldingTax
            WithholdVat += item.WithHoldingVat
            Others += item.DefaultInterest
            VAT += item.Vat


            If item.TransactionType = EnumORTransactionType.MarketFees Then
                Vatable += item.Amount
            ElseIf item.TransactionType = EnumORTransactionType.Energy Then
                Try
                    If selectedItem.DocumentType = EnumDocumentType.INV Then
                        Dim itemSalesAndPurchases = (From x In listSalesAndPurchases _
                                                     Where x.InvoiceNumber = selectedItem.DocumentNo _
                                                     Select x).First()

                        Dim tempVatable = Math.Round(selectedItem.Amount * Math.Abs(itemSalesAndPurchases.VatableRatio), 2)
                        Dim tempVatZeroRated = Math.Round(selectedItem.Amount * Math.Abs(itemSalesAndPurchases.ZeroRatedRatio), 2)

                        If tempVatable + tempVatZeroRated <> selectedItem.Amount Then
                            If tempVatable <> 0 Then
                                Vatable += tempVatable - (tempVatable + tempVatZeroRated - selectedItem.Amount)
                                VatZeroRated += tempVatZeroRated
                            Else
                                Vatable += tempVatable
                                VatZeroRated += tempVatZeroRated - (tempVatable + tempVatZeroRated - selectedItem.Amount)
                            End If
                        Else
                            Vatable += tempVatable
                            VatZeroRated += tempVatZeroRated
                        End If
                    Else
                        Vatable = selectedItem.Amount
                    End If

                Catch ex As Exception
                    VatExempt += selectedItem.Amount
                End Try
            Else
                Vatable += item.Amount
            End If
        Next

        'Update the OR Category Amount
        With itemOfficialReceiptMain
            .VAT = VAT
            .Vatable = Vatable
            .VATExempt = VatExempt
            .VATZeroRated = VatZeroRated
            .WithholdingTax = WithholdTax
            .WithholdingVAT = WithholdVat
            .Others = Others
        End With

    End Sub
#End Region

#Region "GenerateDocumentNumber"
    Public Function GenerateBIRDocumentNumber(ReferenceNumber As Long, BIRDocument As BIRDocumentsType) As String
        Dim ReferenceCode As String

        ReferenceCode = "0000000" & ReferenceNumber.ToString()
        ReferenceCode = Mid(ReferenceCode, Len(ReferenceNumber.ToString()) + 1, 7)

        Select Case BIRDocument
            Case BIRDocumentsType.FinalStatement
                ReferenceCode = AMModule.FSNumberPrefix & ReferenceCode
            Case BIRDocumentsType.DMCM
                ReferenceCode = AMModule.DMCMNumberPrefix & ReferenceCode
            Case BIRDocumentsType.OfficialReceipt
                ReferenceCode = AMModule.ORNumberPrefix & ReferenceCode
            Case BIRDocumentsType.JournalVoucher
                ReferenceCode = AMModule.JVNumberPrefix & ReferenceCode
            Case BIRDocumentsType.FTF
                ReferenceCode = AMModule.FTFNumberPrefix & ReferenceCode
            Case BIRDocumentsType.RFP
                ReferenceCode = AMModule.RFPNumberPrefix & ReferenceCode
            Case BIRDocumentsType.CHECK
                ReferenceCode = AMModule.CheckNumberPrefix & "-" & ReferenceNumber.ToString()
            Case BIRDocumentsType.EFT
                ReferenceCode = "EFT" & ReferenceCode
            Case BIRDocumentsType.SOA
                ReferenceCode = AMModule.SOANumberPrefix & ReferenceCode
            Case BIRDocumentsType.CV
                ReferenceCode = AMModule.CVNumberPrefix & ReferenceCode
        End Select

        Return ReferenceCode
    End Function
#End Region

#Region "GenerateDeferredPaymentReport"
    Public Function GenerateDeferredMonitoringReport(ByVal dt As DataTable, ByVal listDeferred As List(Of DeferredMonitoringReport)) As DataTable
        For Each item In listDeferred
            Dim row = dt.NewRow()

            With item
                row("MPID") = .IDNumber.IDNumber
                row("MP_NAME") = .IDNumber.ParticipantID
                row("OLD_OB_ENERGY") = .OldEnergy
                row("OLD_OB_VAT") = .OldVAT
                row("REMIT_ENERGY") = .RemittanceEnergy
                row("REMIT_VAT") = .RemittanceVAT
                row("DEFERRAL_ENERGY") = .DeferralEnergy
                row("DEFERRAL_VAT") = .DeferralVAT
                row("NEW_OB_ENERGY") = .CurrentEnergy
                row("NEW_OB_VAT") = .CurrentVAT
                row("ALLOCATE_DATE") = .TransactionDate
                row("OLD_OB_TOTAL") = .OldTotal
                row("REMIT_TOTAL") = .RemittanceTotal
                row("DEFERRAL_TOTAL") = .DeferralTotal
                row("NEW_OB_TOTAL") = .CurrentTotal
            End With
            dt.Rows.Add(row)

        Next
        Return dt
    End Function
#End Region

#Region "GenerateGeneralLedgerCashInBankSettlement"
    Public Function GenerateGeneralLedgerCashInBankSettlement(StartDate As Date, EndDate As Date, _
                                                              listGeneralLedgerCashInBankSettlement As List(Of GeneralLedgerCashInBankSettlement), _
                                                              dt As DataTable) As DataTable
        For Each item In listGeneralLedgerCashInBankSettlement
            With item
                Dim row = dt.NewRow()
                row("TRANS_DATE") = .TransactionDate
                row("JOURNAL_NUMBER") = Me.GenerateBIRDocumentNumber(.JournalNumber, BIRDocumentsType.JournalVoucher)

                'If Equal, EFT
                If .ReferenceNumber = 0 Then
                    ' row("REFERENCE_NUMBER") = "For EFT/CHECK"
                    row("REFERENCE_NUMBER") = Me.GenerateBIRDocumentNumber(.JournalNumber, BIRDocumentsType.JournalVoucher)
                Else
                    row("REFERENCE_NUMBER") = Me.GenerateBIRDocumentNumber(.ReferenceNumber, .TransactionType)
                End If

                row("DESCRIPTION") = .ParticipantName
                row("DEBIT") = .Debit
                row("CREDIT") = .Credit
                row("FROM_DATE") = StartDate
                row("TO_DATE") = EndDate

                dt.Rows.Add(row)
            End With
        Next

        Return dt
    End Function
#End Region

#Region "GenerateGeneralLedgerCashInBankPrudential"
    Public Function GenerateGeneralLedgerCashInBankPrudential(StartDate As Date, EndDate As Date, _
                                                              listGeneralLedgerCashInBankPrudential As List(Of GeneralLedgerCashInBankPrudential), _
                                                              dt As DataTable) As DataTable
        For Each item In listGeneralLedgerCashInBankPrudential
            With item
                Dim row = dt.NewRow()
                If .ReferenceNumber = 0 And .JournalNumber = 0 Then
                    row("JOURNAL_NUMBER") = .Description
                    row("REFERENCE_NUMBER") = "Beginning Balance"
                ElseIf .ReferenceNumber = 0 Then
                    row("REFERENCE_NUMBER") = Me.GenerateBIRDocumentNumber(.JournalNumber, BIRDocumentsType.JournalVoucher)
                    row("JOURNAL_NUMBER") = Me.GenerateBIRDocumentNumber(.JournalNumber, BIRDocumentsType.JournalVoucher)
                Else
                    row("JOURNAL_NUMBER") = Me.GenerateBIRDocumentNumber(.JournalNumber, BIRDocumentsType.JournalVoucher)
                    row("REFERENCE_NUMBER") = Me.GenerateBIRDocumentNumber(.ReferenceNumber, .TransactionType)
                End If

                row("TRANS_DATE") = .TransactionDate

                row("DESCRIPTION") = .ParticipantName
                row("DEBIT") = .Debit
                row("CREDIT") = .Credit
                row("FROM_DATE") = StartDate
                row("TO_DATE") = EndDate

                dt.Rows.Add(row)
            End With
        Next

        Return dt
    End Function
#End Region

#Region "GenerateGeneralLedgerInterestPayableSettlement"
    Public Function GenerateGeneralLedgerInterestPayableSettlement(StartDate As Date, EndDate As Date, _
                                                              listGeneralLedgerInterestPayableSettlement As List(Of GeneralLedgerInterestPayableSettlement), _
                                                              dt As DataTable) As DataTable
        For Each item In listGeneralLedgerInterestPayableSettlement
            With item
                Dim row = dt.NewRow()
                row("TRANS_DATE") = .TransactionDate
                row("JOURNAL_NUMBER") = Me.GenerateBIRDocumentNumber(.JournalNumber, BIRDocumentsType.JournalVoucher)
                row("DESCRIPTION") = .Description
                row("DEBIT") = .Debit
                row("CREDIT") = .Credit
                row("FROM_DATE") = StartDate
                row("TO_DATE") = EndDate

                dt.Rows.Add(row)
            End With
        Next

        Return dt
    End Function
#End Region

#Region "GenerateGeneralLedgerInterestPayablePrudential"
    Public Function GenerateGeneralLedgerInterestPayablePrudential(StartDate As Date, EndDate As Date, _
                                                              listGeneralLedgerInterestPayablePrudential As List(Of GeneralLedgerInterestPayablePrudential), _
                                                              dt As DataTable) As DataTable
        For Each item In listGeneralLedgerInterestPayablePrudential
            With item
                Dim row = dt.NewRow()

                If .JournalNumber = 0 And .ReferenceNumber = 0 Then
                    row("JOURNAL_NUMBER") = "None"
                    row("REFERENCE_NUMBER") = "PR Interest Beginning"
                Else
                    row("JOURNAL_NUMBER") = Me.GenerateBIRDocumentNumber(.JournalNumber, BIRDocumentsType.JournalVoucher)
                    row("REFERENCE_NUMBER") = Me.GenerateBIRDocumentNumber(.ReferenceNumber, .TransactionType)
                End If

                row("TRANS_DATE") = .TransactionDate
                row("DESCRIPTION") = .ParticipantName
                row("DEBIT") = .Debit
                row("CREDIT") = .Credit
                row("FROM_DATE") = StartDate
                row("TO_DATE") = EndDate

                dt.Rows.Add(row)
            End With
        Next

        Return dt
    End Function
#End Region

#Region "GenerateGeneralLedgerPrudentialPerParticipant"
    Public Function GenerateGeneralLedgerPrudentialPerParticipant(StartDate As Date, EndDate As Date, _
                                                              listGeneralLedgerPrudentialPerParticipant As List(Of GeneralLedgerPrudentialPerParticipant), _
                                                              dt As DataTable) As DataTable
        For Each item In listGeneralLedgerPrudentialPerParticipant
            With item
                Dim row = dt.NewRow()

                row("TRANS_DATE") = .TransactionDate

                If .PRTransType = EnumPrudentialTransType.PRBeginningBalance Then
                    row("JOURNAL_NUMBER") = "None"
                    row("REFERENCE_NUMBER") = "PR Beginning"
                ElseIf .PRTransType = EnumPrudentialTransType.PRIntBegginingBalance Then
                    row("JOURNAL_NUMBER") = "None"
                    row("REFERENCE_NUMBER") = "PR Interest Beginning"
                Else
                    row("JOURNAL_NUMBER") = Me.GenerateBIRDocumentNumber(.JournalNumber, BIRDocumentsType.JournalVoucher)
                    row("REFERENCE_NUMBER") = Me.GenerateBIRDocumentNumber(.ReferenceNumber, .TransactionType)
                End If
                
                row("START_DATE") = StartDate
                row("END_DATE") = EndDate
                row("ID_NUMBER") = .IDNumber.IDNumber
                row("FULL_NAME") = .IDNumber.FullName
                row("AMOUNT") = .Amount
                row("CURRENT_AMOUNT") = .CurrentAmount
                row("AMOUNT31TO60") = .Amount31to60
                row("AMOUNT61TO90") = .Amount61to90
                row("AMOUNT91OVER") = .Amount91over

                dt.Rows.Add(row)
            End With
        Next

        Return dt
    End Function
#End Region

#Region "GenerateSubsidiaryLedgerAccountsReceivablePerParticipant"
    Public Sub GenerateSubsidiaryLedgerAccountsReceivablePerParticipant(IDNumber As AMParticipants, listSubsidiaryLedgerAR As List(Of SubsidiaryLedgerAR), _
                                                                        dt As DataTable)
        For Each item In listSubsidiaryLedgerAR
            With item
                Dim row = dt.NewRow()
                row("TRANS_DATE") = .TransactionDate

                Select Case .TransactionType
                    Case BIRDocumentsType.DMCM
                        row("REFERENCE_NUMBER") = Me.GenerateBIRDocumentNumber(CLng(.ReferenceNumber), .TransactionType)
                    Case BIRDocumentsType.OfficialReceipt
                        row("REFERENCE_NUMBER") = Me.GenerateBIRDocumentNumber(CLng(.ReferenceNumber), .TransactionType)    'Concatenate space so that DMCM and OR has the same width in Report
                    Case BIRDocumentsType.JournalVoucher
                        row("REFERENCE_NUMBER") = Me.GenerateBIRDocumentNumber(CLng(.ReferenceNumber), .TransactionType)
                    Case Else
                        row("REFERENCE_NUMBER") = .ReferenceNumber
                End Select

                row("CHARGE_TYPE") = .ChargeTypeValue
                row("ID_NUMBER") = IDNumber.IDNumber
                row("FULL_NAME") = IDNumber.FullName
                row("AMOUNT") = .Amount
                row("CURRENT_AMOUNT") = .CurrentAmount
                row("AMOUNT31TO60") = .Amount31to60
                row("AMOUNT61TO90") = .Amount61to90
                row("AMOUNT91OVER") = .Amount91over

                dt.Rows.Add(row)
            End With
        Next

    End Sub
#End Region

#Region "GenerateSubsidiaryLedgerAccountsPayablePerParticipant"
    Public Sub GenerateSubsidiaryLedgerAccountsPayablePerParticipant(IDNumber As AMParticipants, listSubsidiaryLedgerAP As List(Of SubsidiaryLedgerAP), _
                                                                          dt As DataTable)
        For Each item In listSubsidiaryLedgerAP
            With item
                Dim row = dt.NewRow()
                row("TRANS_DATE") = .TransactionDate
                Select Case .TransactionType
                    Case BIRDocumentsType.JournalVoucher
                        row("REFERENCE_NUMBER") = Me.GenerateBIRDocumentNumber(CLng(.ReferenceNumber), .TransactionType)
                    Case BIRDocumentsType.DMCM
                        row("REFERENCE_NUMBER") = Me.GenerateBIRDocumentNumber(CLng(.ReferenceNumber), .TransactionType)
                    Case Else
                        row("REFERENCE_NUMBER") = .ReferenceNumber
                End Select
                row("CHARGE_TYPE") = .ChargeTypeValue
                row("ID_NUMBER") = IDNumber.IDNumber
                row("FULL_NAME") = IDNumber.FullName
                row("AMOUNT") = .Amount
                row("CURRENT_AMOUNT") = .CurrentAmount
                row("AMOUNT31TO60") = .Amount31to60
                row("AMOUNT61TO90") = .Amount61to90
                row("AMOUNT91OVER") = .Amount91over

                dt.Rows.Add(row)
            End With
        Next
    End Sub
#End Region

#Region "GenerateDeferredPaymentReport"
    Public Function GenerateFundTransferFormReport(ByVal dtMain As DataTable, dtParticipant As DataTable, _
                                                   dtDetails As DataTable, itemFTF As FundTransferFormMain, itemSignatory As DocSignatories) As DataSet
        Dim ds As New DataSet

        With itemFTF
            Dim row = dtMain.NewRow()
            row("REF_NO") = .RefNo
            row("DR_DATE") = .DRDate.ToString("MMMM dd, yyyy")
            row("CR_DATE") = .CRDate.ToString("MMMM dd, yyyy")
            Select Case itemFTF.TransType
                Case EnumFTFTransType.DrawDown
                    row("REMARKS") = "Drawdown of Prudential Security Deposit     P" & FormatNumber(.TotalAmount, 2)
                Case EnumFTFTransType.Replenishment
                    row("REMARKS") = "Replenishment of Prudential Security Deposit      P" & FormatNumber(.TotalAmount, 2)
                Case EnumFTFTransType.TransferPEMCAccount
                    row("REMARKS") = "Fund Transfer From Settlement to IEMOP Account    P" & FormatNumber(.TotalAmount, 2)
                Case EnumFTFTransType.TransferSTLToNSS
                    row("REMARKS") = "Fund Transfer From Settlement to NSS Account     P" & FormatNumber(.TotalAmount, 2)
                Case EnumFTFTransType.TransferNSSToSTL
                    row("REMARKS") = "Transfer of NSS to Settlement Account     P" & FormatNumber(.TotalAmount, 2)
                Case EnumFTFTransType.TransferMarketFeesToPEMC
                    row("REMARKS") = "Transfer of Market Fees to IEMOP Account     P" & FormatNumber(.TotalAmount, 2)
                Case EnumFTFTransType.TransferMarketFeesToSTL
                    row("REMARKS") = "Transfer of Market Fees to Settlement from IEMOP Account     P" & FormatNumber(.TotalAmount, 2)
            End Select

            row("TOTAL_AMOUNT") = .TotalAmount
            row("PREPARED_BY") = AMModule.FullName
            row("POSITION") = AMModule.Position
            row("REQUESTING_APPROVAL") = itemSignatory.Signatory_1
            row("POSITION1") = itemSignatory.Position_1
            row("APPROVED_BY") = itemSignatory.Signatory_2
            row("POSITION2") = itemSignatory.Position_2

            If itemFTF.TransType = EnumFTFTransType.DrawDown Then
                row("NOTED_BY") = itemSignatory.Signatory_3
                row("POSITION3") = itemSignatory.Position_3
            End If

            dtMain.Rows.Add(row)
        End With
        dtMain.AcceptChanges()

        For Each item In itemFTF.ListOfFTFParticipants
            Dim ptr As Integer = 0
            For Each tRow As DataRow In dtParticipant.Rows
                If CStr(tRow("ID_NUMBER")) = item.IDNumber.IDNumber Then
                    tRow("AMOUNT") = CDec(tRow("AMOUNT")) + item.Amount
                    ptr = 1
                End If
            Next
            If ptr = 0 Then
                Dim row = dtParticipant.NewRow()
                With item
                    row("REF_NO") = .RefNo
                    row("ID_NUMBER") = .IDNumber.IDNumber
                    row("PARTICPANT_ID") = .IDNumber.ParticipantID
                    row("AMOUNT") = .Amount
                    dtParticipant.Rows.Add(row)
                End With
            End If            
        Next
        dtParticipant.AcceptChanges()

        For Each item In itemFTF.ListOfFTFDetails
            Dim row = dtDetails.NewRow()

            With item
                row("REF_NO") = .RefNo
                row("BANK_ACCT_NO") = .BankAccountNo
                
                If .BankAccountNo.Contains("Pru") Then
                    If .Debit <> 0 Then
                        row("RECEIVING_BANK") = "SCB Prudential"
                    ElseIf .Credit <> 0 Then
                        row("ISSUING_BANK") = "SCB Prudential"
                    End If
                ElseIf .BankAccountNo.Contains("PEM") Then
                    If .Debit <> 0 Then
                        row("RECEIVING_BANK") = "BPI"
                    ElseIf .Credit <> 0 Then
                        row("ISSUING_BANK") = "BPI"
                    End If
                ElseIf .BankAccountNo.Contains("Setl") Then
                    If .Debit <> 0 Then
                        row("RECEIVING_BANK") = "SCB Settlement"
                    ElseIf .Credit <> 0 Then
                        row("ISSUING_BANK") = "SCB Settlement"
                    End If
                End If

                'If FTF.TransType = EnumFTFTransType.TransferMarketFeesToPEMC Or FTF.TransType = EnumFTFTransType.TransferPEMCAccount Then
                'row("AMOUNT") = (.Debit - .Credit) * -1
                'Else
                row("AMOUNT") = .Debit - .Credit
                'End If
                dtDetails.Rows.Add(row)
            End With
        Next
        dtDetails.AcceptChanges()

        With ds.Tables
            .Add(dtMain)
            .Add(dtParticipant)
            .Add(dtDetails)
        End With
        ds.AcceptChanges()

        Return ds
    End Function
#End Region

End Class
