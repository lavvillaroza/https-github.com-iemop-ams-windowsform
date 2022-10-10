'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             OfficialReceiptReportMain
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     November 18,2013
'Development Group:      Software Development and Support Division
'Description:            Class for Collection
'Arguments/Parameters:  
'Files/Database Tables:  NONE
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   November 18, 2012       Vladimir E. Espiritu            Class initialization
'

Public Class OfficialReceiptReportMain

#Region "Initialization/Constructor"
    Public Sub New()
        Me.New(New OfficialReceiptMain, New List(Of OfficialReceiptReportRawDetailsNew), New AMParticipants, 0, "", "", New Collection)
    End Sub

    Public Sub New(ByVal itemofficialreceipt As OfficialReceiptMain, ByVal listofficialreceiptreportrawdetails As List(Of OfficialReceiptReportRawDetailsNew), _
                   ByVal itemparticipant As AMParticipants, ByVal defaultinterestrate As Decimal, ByVal totalpaymentinwords As String, ByVal birpermitnumber As String, _
                   ByVal itemcollection As Collection)
        Me._ItemOfficialReceipt = itemofficialreceipt
        Me._ListOfficialReceiptReportRawDetails = listofficialreceiptreportrawdetails
        Me._ItemParticipant = itemparticipant
        Me._DefaultInterestRate = defaultinterestrate
        Me._TotalPayment = 0
        Me._TotalPaymentInWords = totalpaymentinwords
        Me._BIRPermitNumber = birpermitnumber
        Me._ItemCollection = itemcollection
    End Sub
#End Region


#Region "ItemOfficialReceipt"
    Private _ItemOfficialReceipt As OfficialReceiptMain
    Public Property ItemOfficialReceipt() As OfficialReceiptMain
        Get
            Return _ItemOfficialReceipt
        End Get
        Set(ByVal value As OfficialReceiptMain)
            _ItemOfficialReceipt = value
        End Set
    End Property

#End Region

#Region "ListOfficialReceiptReportRawDetails"
    Private _ListOfficialReceiptReportRawDetails As New List(Of OfficialReceiptReportRawDetailsNew)
    Public Property ListOfficialReceiptReportRawDetails() As List(Of OfficialReceiptReportRawDetailsNew)
        Get
            Return _ListOfficialReceiptReportRawDetails
        End Get
        Set(ByVal value As List(Of OfficialReceiptReportRawDetailsNew))
            _ListOfficialReceiptReportRawDetails = value
        End Set
    End Property

#End Region

#Region "ListOfficialReceiptReportDetails"
    Private _ListOfficialReceiptReportDetails As New List(Of OfficialReceiptReportDetailsNew)
    Public ReadOnly Property ListOfficialReceiptReportDetails() As List(Of OfficialReceiptReportDetailsNew)
        Get
            _ListOfficialReceiptReportDetails = New List(Of OfficialReceiptReportDetailsNew)

            'For Default Interest on Energy and Energy
            Dim listEnergy = (From x In Me.ListOfficialReceiptReportRawDetails _
                             Where x.DocumentNo <> "" And (x.TransactionType = EnumORTransactionType.Energy) _
                             Select x).ToList


            If listEnergy.Count <> 0 Then
                Dim listDocNo = From x In listEnergy _
                                Select x.DocumentNo Distinct

                For Each itemDocNo In listDocNo
                    Dim selectedItem = itemDocNo

                    Dim listItem = From x In listEnergy _
                                   Where x.DocumentNo = selectedItem _
                                   Select x Order By x.TransactionType

                    Dim newItemEnergy As New OfficialReceiptReportDetailsNew
                    For Each item In listItem
                        With newItemEnergy
                            Select Case item.DocumentType
                                Case EnumDocumentType.INV
                                    .DocumentNo = item.DocumentNo
                                Case EnumDocumentType.DMCM
                                    .DocumentNo = EnumDocumentType.DMCM.ToString() & item.DocumentNo
                            End Select

                            .DocumentDate = FormatDateTime(item.DocumentDate, DateFormat.ShortDate)
                            .DueDate = item.DueDate
                            .Amount += item.Amount
                            .VAT += item.Vat
                            .DefaultInterest += item.DefaultInterest
                            .WithholdingTax += item.WithHoldingTax
                            .TransactionType = item.TransactionType
                        End With
                    Next
                    _ListOfficialReceiptReportDetails.Add(newItemEnergy)
                Next
            End If

            'For Market Fees
            Dim listMarketFees = From x In Me.ListOfficialReceiptReportRawDetails _
                                 Where x.DocumentNo <> "" And (x.TransactionType = EnumORTransactionType.MarketFees) _
                                 Select x

            If listMarketFees.Count <> 0 Then
                Dim listDocNo = From x In listMarketFees _
                                Select x.DocumentNo Distinct

                For Each itemDocNo In listDocNo
                    Dim selectedItem = itemDocNo

                    Dim listItem = From x In listMarketFees _
                                   Where x.DocumentNo = selectedItem _
                                   Select x Order By x.TransactionType

                    Dim newItemMarketFees As New OfficialReceiptReportDetailsNew
                    For Each item In listItem
                        With newItemMarketFees

                            Select Case item.DocumentType
                                Case EnumDocumentType.INV
                                    .DocumentNo = item.DocumentNo
                                Case EnumDocumentType.DMCM
                                    .DocumentNo = item.DocumentNo
                            End Select
                            .DocumentDate = item.DocumentDate
                            .DueDate = item.DueDate
                            '.DueDate = item.DueDate
                            .Amount += item.Amount
                            .VAT += item.Vat
                            .DefaultInterest += item.DefaultInterest
                            .TransactionType = item.TransactionType
                            .WithholdingTax += item.WithHoldingTax
                            .WitholdingVAT += item.WithHoldingVat

                        End With
                    Next

                    _ListOfficialReceiptReportDetails.Add(newItemMarketFees)
                Next
            End If

            'For Prudential, Excess Collection and Held Collection
            Dim listExcess = From x In Me.ListOfficialReceiptReportRawDetails _
                             Where (x.TransactionType = EnumCollectionType.TransferToReplenishment Or _
                             x.TransactionType = EnumCollectionType.TransferToExcessCollection _
                             Or x.TransactionType = EnumCollectionType.TransferToHeldCollection Or x.TransactionType = EnumCollectionType.AppliedHeldCollection _
                             Or x.TransactionType = EnumORTransactionType.Replenishment) _
                             Select x

            For Each item In listExcess
                Dim itemExcess As New OfficialReceiptReportDetailsNew

                With itemExcess
                    .Amount = item.Amount                    
                    Select Case item.TransactionType
                        Case EnumCollectionType.TransferToReplenishment
                            .DocumentNo = "Replenishment"

                        Case EnumORTransactionType.Replenishment
                            .DocumentNo = "Replenishment"

                        Case EnumCollectionType.TransferToExcessCollection
                            .DocumentNo = "Excess"

                        Case EnumCollectionType.TransferToHeldCollection
                            .DocumentNo = "Held"

                        Case EnumCollectionType.AppliedHeldCollection
                            .DocumentNo = "Held"

                            'Change the sign if applied held collection
                            .Amount = item.Amount * -1D

                    End Select
                    .TransactionType = 13
                End With

                _ListOfficialReceiptReportDetails.Add(itemExcess)
            Next

            Return _ListOfficialReceiptReportDetails
        End Get
    End Property

#End Region

#Region "ItemParticipant"
    Private _ItemParticipant As AMParticipants
    Public Property ItemParticipant() As AMParticipants
        Get
            Return _ItemParticipant
        End Get
        Set(ByVal value As AMParticipants)
            _ItemParticipant = value
        End Set
    End Property

#End Region

#Region "DefaultInterestRate"
    Private _DefaultInterestRate As Decimal
    Public Property DefaultInterestRate() As Decimal
        Get
            Return _DefaultInterestRate * 100D
        End Get
        Set(ByVal value As Decimal)
            _DefaultInterestRate = value
        End Set
    End Property

#End Region

#Region "TotalPayment"
    Private _TotalPayment As Decimal
    Public Property TotalPayment() As Decimal
        Get
            _TotalPayment = Me.ItemOfficialReceipt.Amount

            Return _TotalPayment
        End Get

        Set(ByVal value As Decimal)
            _DefaultInterestRate = value
        End Set
    End Property

#End Region

#Region "TotalPaymentInWords"
    Private _TotalPaymentInWords As String
    Public Property TotalPaymentInWords() As String
        Get
            Return _TotalPaymentInWords
        End Get
        Set(ByVal value As String)
            _TotalPaymentInWords = value
        End Set
    End Property

#End Region

#Region "BIRPermitNumber"
    Private _BIRPermitNumber As String
    Public Property BIRPermitNumber() As String
        Get
            Return _BIRPermitNumber
        End Get
        Set(ByVal value As String)
            _BIRPermitNumber = value
        End Set
    End Property

#End Region

#Region "ItemCollection"
    Private _ItemCollection As Collection
    Public Property ItemCollection() As Collection
        Get
            Return _ItemCollection
        End Get
        Set(ByVal value As Collection)
            _ItemCollection = value
        End Set
    End Property

#End Region

End Class
