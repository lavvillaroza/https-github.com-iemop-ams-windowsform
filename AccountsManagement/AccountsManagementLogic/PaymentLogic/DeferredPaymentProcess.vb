Imports AccountsManagementObjects
Imports AccountsManagementDataAccess

Public Class DeferredPaymentProcess
    Implements IDisposable

    Public Function GenerateDeferredPaymentsDT(ByVal DeferredDT As DataTable, _
                                               ByVal AllocationDate As Date, _
                                               ByVal CurrentDeferredPaymentList As List(Of DeferredMain), _
                                               ByVal PrevDeferredPaymentList As List(Of DeferredMain), _
                                               ByVal AMParticipantsList As List(Of AMParticipants)) As DataTable

        Dim CurEnergyDeferred = (From x In CurrentDeferredPaymentList _
                                  Where x.ChargeType = EnumChargeType.E _
                                  Select x).ToList()

        Dim PrevEnergyDeferred = (From x In PrevDeferredPaymentList _
                                  Where x.ChargeType = EnumChargeType.E _
                                  Select x).ToList()

        Dim CurVATDeferred = (From x In CurrentDeferredPaymentList _
                               Where x.ChargeType = EnumChargeType.EV _
                               Select x).ToList()

        Dim PrevVATDeferred = (From x In PrevDeferredPaymentList _
                               Where x.ChargeType = EnumChargeType.EV _
                               Select x).ToList()

        Dim GetMPIDs = (From x In CurrentDeferredPaymentList _
                               Select x.IDNumber).Distinct.ToList()

        For Each item In GetMPIDs
            Dim dRow = DeferredDT.NewRow
            Dim ParticipantsInfo As AMParticipants = (From x In AMParticipantsList _
                                                         Where x.IDNumber = item).FirstOrDefault

            Dim GetCurEneDef = (From x In CurEnergyDeferred Where x.IDNumber = item Select x).FirstOrDefault
            Dim GetCurVATDef = (From x In CurVATDeferred Where x.IDNumber = item Select x).FirstOrDefault
            Dim GetPrevEneDef = (From x In PrevEnergyDeferred Where x.IDNumber = item Select x).FirstOrDefault
            Dim GetPrevVATDef = (From x In PrevVATDeferred Where x.IDNumber = item Select x).FirstOrDefault

            dRow("MPID") = ParticipantsInfo.IDNumber
            dRow("MP_NAME") = ParticipantsInfo.FullName
            dRow("ALLOCATE_DATE") = AllocationDate

            If GetPrevEneDef Is Nothing Then
                dRow("OLD_OB_ENERGY") = 0
            Else
                dRow("OLD_OB_ENERGY") = GetPrevEneDef.OutstandingBalanceDeferredPayment
            End If

            If GetPrevVATDef Is Nothing Then
                dRow("OLD_OB_VAT") = 0
            Else
                dRow("OLD_OB_VAT") = GetPrevVATDef.OutstandingBalanceDeferredPayment
            End If
            dRow("OLD_OB_TOTAL") = CDec(dRow("OLD_OB_ENERGY")) + CDec(dRow("OLD_OB_VAT"))

            If Not GetCurEneDef Is Nothing Then
                Select Case GetCurEneDef.DeferredType
                    Case EnumDeferredType.Remitted
                        dRow("REMIT_ENERGY") = GetCurEneDef.DeferredAmount
                        dRow("DEFERRAL_ENERGY") = 0
                        dRow("NEW_OB_ENERGY") = GetCurEneDef.OutstandingBalanceDeferredPayment
                    Case EnumDeferredType.Deferral
                        dRow("REMIT_ENERGY") = 0
                        dRow("DEFERRAL_ENERGY") = GetCurEneDef.DeferredAmount
                        dRow("NEW_OB_ENERGY") = GetCurEneDef.OutstandingBalanceDeferredPayment
                    Case EnumDeferredType.OutstandingBalance
                        dRow("REMIT_ENERGY") = 0
                        dRow("DEFERRAL_ENERGY") = 0
                        dRow("NEW_OB_ENERGY") = GetCurEneDef.OutstandingBalanceDeferredPayment
                End Select
            Else
                dRow("REMIT_ENERGY") = 0
                dRow("DEFERRAL_ENERGY") = 0
                dRow("NEW_OB_ENERGY") = 0
            End If

            If Not GetCurVATDef Is Nothing Then
                Select Case GetCurVATDef.DeferredType
                    Case EnumDeferredType.Remitted
                        dRow("REMIT_VAT") = GetCurVATDef.DeferredAmount
                        dRow("DEFERRAL_VAT") = 0
                        dRow("NEW_OB_VAT") = GetCurVATDef.OutstandingBalanceDeferredPayment
                    Case EnumDeferredType.Deferral
                        dRow("REMIT_VAT") = 0
                        dRow("DEFERRAL_VAT") = GetCurVATDef.DeferredAmount
                        dRow("NEW_OB_VAT") = GetCurVATDef.OutstandingBalanceDeferredPayment
                    Case EnumDeferredType.OutstandingBalance
                        dRow("REMIT_VAT") = 0
                        dRow("DEFERRAL_VAT") = 0
                        dRow("NEW_OB_VAT") = GetCurVATDef.OutstandingBalanceDeferredPayment
                End Select
            Else
                dRow("REMIT_VAT") = 0
                dRow("DEFERRAL_VAT") = 0
                dRow("NEW_OB_VAT") = 0
            End If
            dRow("REMIT_TOTAL") = CDec(dRow("REMIT_ENERGY")) + CDec(dRow("REMIT_VAT"))
            dRow("DEFERRAL_TOTAL") = CDec(dRow("DEFERRAL_VAT")) + CDec(dRow("DEFERRAL_ENERGY"))
            dRow("NEW_OB_TOTAL") = CDec(dRow("NEW_OB_ENERGY")) + CDec(dRow("NEW_OB_VAT"))
            DeferredDT.Rows.Add(dRow)
            DeferredDT.AcceptChanges()
        Next
        Return DeferredDT
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
