Option Explicit On
Option Strict On

Imports AccountsManagementDataAccess
Imports WESMLib.Auth
Public Class AllocationDateContext

    Public Function GetCollectionAllocationDate(DataAccess As DAL) As List(Of AllocationDate)
        Dim result As New List(Of AllocationDate)
        Dim report As New DataReport

        Try            
            Dim SQL As String = "SELECT * FROM (select distinct a.allocation_date, a.is_allocated from am_collection a, am_collection_allocation b " & _
                                                "where a.collection_no = b.collection_no and a.is_allocated = 1" & _
                                                "UNION " & _
                                                "select distinct  a.allocation_date, a.is_allocated from am_collection a, am_collection_monitoring b " & _
                                                "where a.collection_no = b.collection_no and  b.trans_type <> '" & EnumCollectionMonitoringType.TransferToHeldCollection & _
                                                "' and  b.trans_type <> '" & EnumCollectionMonitoringType.TransferToPRReplenishment & "') " & _
                                "ORDER BY ALLOCATION_DATE ASC"

            report = DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            result = Me.GetCollAllocDateList(report.ReturnedIDatareader)
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return result
    End Function

    Private Function GetCollAllocDateList(ByVal reader As IDataReader) As List(Of AllocationDate)
        Dim result As New List(Of AllocationDate)
        Dim cnt As Integer = 0
        Try
            While reader.Read()
                With reader
                    result.Add(New AllocationDate(CDate(FormatDateTime(CDate(.Item("ALLOCATION_DATE")))), CInt(.Item("IS_ALLOCATED"))))
                End With
            End While
        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Finally
            If Not reader.IsClosed Then
                reader.Close()
            End If
        End Try
        Return result
    End Function

End Class
