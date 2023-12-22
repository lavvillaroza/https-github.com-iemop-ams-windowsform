'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             DAL
'Orginal Author:         Vladimir E.Espiritu
'File Creation Date:     August 22, 2011
'Development Group:      Software Development and Support Division
'Description:            Layer for database connection and execution of SQL scripts
'Arguments/Parameters:  
'Files/Database Tables: 
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		 Programmer		 Description
'   August 22, 2011      Vloody          Added functions that will execute SQL scripts.
'	

Option Explicit On
Option Strict On

Imports System.Data
Imports System.Text
Imports System.Exception
Imports System.Configuration
Imports AccountsManagementObjects
Imports Oracle.ManagedDataAccess.Client
Imports System.Threading

Public Class DAL

#Region "Member Variables"
    Private _conn As OracleConnection
    Private _myAdapter As OracleDataAdapter
    Private _myCommand As OracleCommand
    Private _myReader As OracleDataReader
#End Region

#Region "Single Instance Code"
    ' This variable stores the reference of the single instance    
    Private Shared m_Instance As DAL = Nothing
    Public Shared Function GetInstance() As DAL
        If m_Instance Is Nothing Then
            m_Instance = New DAL()
        End If
        Return m_Instance
    End Function

    Private Sub New()
        Me._ConnectionString = ""
    End Sub

    Private _DALAcess As DAL
    ' gets the DataAccessLayer
    Public ReadOnly Property DALAcess() As DAL
        Get
            Return Me._DALAcess
        End Get
    End Property

    Private _ConnectionString As String
    ' gets or sets the database connection string    
    Public Property ConnectionString() As String
        Get
            Return _ConnectionString
        End Get
        Set(ByVal value As String)
            Me._ConnectionString = value
        End Set
    End Property
#End Region

#Region "Open Connection"
    Private Sub OpenConnection()
        'Set the Connection String
        Me._conn = New OracleConnection(Me.ConnectionString)

        'Open the Connection
        Try
            If Me._conn.State = ConnectionState.Open Then
                Me._conn.Close()
                Me._conn.Open()
            Else
                Me._conn.Open()
            End If
        Catch ex As OracleException
            Throw New ApplicationException("The connection is closed or cannot connect to the database")
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

    End Sub
#End Region

#Region "Test Connection"
    Public Function VerifyConnection(ByVal constr As String) As Boolean
        Using oraconn As New OracleConnection(constr)
            Try
                oraconn.Open()
                VerifyConnection = True
            Catch ex As OracleException
                VerifyConnection = False
                Exit Function
            Finally
                oraconn.Close()
            End Try
        End Using
    End Function
#End Region

#Region "ExecuteSelectQueryReturningTable"
    Public Function ExecuteSelectQueryReturningTable(ByVal _SQL As String) As DataReport
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim report As New DataReport

        Try
            Me.OpenConnection()
            Me._myAdapter = New OracleDataAdapter(_SQL, Me._conn)
            Me._myAdapter.Fill(ds)
            dt = ds.Tables(0)
            report.ReturnedTable = dt

        Catch ex As ApplicationException
            report.ErrorMessage = ex.Message
        Finally
            If Me._conn.State <> ConnectionState.Closed Then
                Me._conn.Close()
            End If

            Me._myAdapter.Dispose()
            Me._conn.Dispose()
        End Try

        Return report
    End Function

#End Region

#Region "ExecuteSelectQueryReturningDataReader"
    Public Function ExecuteSelectQueryReturningDataReader(ByVal _SQL As String) As DataReport
        Dim report As New DataReport
        Me._myReader = Nothing
        Try
            Me.OpenConnection()
            Try
                Me._myCommand = New OracleCommand(_SQL, _conn)
                Me._myReader = Me._myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                report.ReturnedIDatareader = Me._myReader
            Catch ex As Exception
                report.ErrorMessage = ex.Message
                If Me._conn.State <> ConnectionState.Closed Then
                    Me._conn.Close()
                End If
                Me._myCommand.Dispose()
            End Try
        Catch ex As Exception
            report.ErrorMessage = ex.Message
            If Me._conn.State <> ConnectionState.Closed Then
                Me._conn.Close()
            End If
        End Try
        Return report
    End Function
#End Region

#Region "ExecuteSelectQueryReturningDataReader"
    Public Async Function ExecuteSelectQueryReturningDataReaderAsync(ByVal _SQL As String) As Tasks.Task(Of DataReport)
        Dim report As New DataReport
        Me._myReader = Nothing
        Try
            Me.OpenConnection()
            Try
                Me._myCommand = New OracleCommand(_SQL, _conn)
                Me._myReader = CType(Await Me._myCommand.ExecuteReaderAsync(CommandBehavior.CloseConnection), OracleDataReader)
                report.ReturnedIDatareader = Me._myReader
            Catch ex As Exception
                report.ErrorMessage = ex.Message
                If Me._conn.State <> ConnectionState.Closed Then
                    Me._conn.Close()
                End If
                Me._myCommand.Dispose()
            End Try
        Catch ex As Exception
            report.ErrorMessage = ex.Message
            If Me._conn.State <> ConnectionState.Closed Then
                Me._conn.Close()
            End If
        End Try
        Return report
    End Function
#End Region

#Region "ExcuteNonQuery"
    Public Function ExcuteNonQuery(ByVal _SQL As String, ByVal sqlParameter() As OracleParameter) As DataReport
        Dim report As New DataReport
        Try
            Me.OpenConnection()

            Me._myCommand = New OracleCommand()
            Me._myCommand.Connection = Me._conn
            Me._myCommand.CommandText = _SQL
            Me._myCommand.CommandType = CommandType.StoredProcedure
            Me._myCommand.Parameters.AddRange(sqlParameter)
            Me._myCommand.ExecuteNonQuery()

        Catch ex As ApplicationException
            report.ErrorMessage = ex.Message
        Finally
            If Me._conn.State <> ConnectionState.Closed Then
                Me._conn.Close()
            End If

            Me._myCommand.Parameters.Clear()
            Me._myCommand.Dispose()
            Me._conn.Dispose()
        End Try

        Return report
    End Function


    Public Function ExcuteNonQuery(ByVal _SQL As String) As DataReport
        Dim report As New DataReport
        Try
            Me.OpenConnection()

            Try
                Me._myCommand = New OracleCommand()
                Me._myCommand.Connection = Me._conn
                Me._myCommand.CommandText = _SQL
                Me._myCommand.ExecuteNonQuery()
            Catch ex As Exception
                report.ErrorMessage = ex.Message
                Me._myCommand.Dispose()
            End Try

        Catch ex As ApplicationException
            report.ErrorMessage = ex.Message
        Finally
            If Me._conn.State <> ConnectionState.Closed Then
                Me._conn.Close()
            End If
            Me._conn.Dispose()
        End Try

        Return report
    End Function
#End Region

#Region "ExecuteSaveQueryBulk"
    'Public Function ExecuteSaveQueryBulk(ByVal dt As DataTable) As DataReport
    '    Dim report As New DataReport

    '    Try
    '        Me.OpenConnection()

    '        Dim bulkInsert = New OracleBulkCopy(Me._conn, OracleBulkCopyOptions.UseInternalTransaction)
    '        bulkInsert.DestinationTableName = dt.TableName
    '        bulkInsert.BatchSize = dt.Rows.Count
    '        bulkInsert.WriteToServer(dt)
    '    Catch ex As ApplicationException
    '        report.ErrorMessage = ex.Message
    '    Finally
    '        If Me._conn.State <> ConnectionState.Closed Then
    '            Me._conn.Close()
    '        End If

    '        Me._conn.Dispose()
    '    End Try

    '    Return report
    'End Function
#End Region

#Region "ExecuteSaveQuery"
    Public Function ExecuteSaveQuery(ByVal _ListSQL As List(Of String), ByVal ds As DataSet) As DataReport
        Dim savetransaction As OracleTransaction = Nothing
        Dim report As New DataReport
        Dim errMsg As String = ""
        Try
            Me.OpenConnection()
        Catch ex As Exception
            report.ErrorMessage = ex.Message

            If Me._conn.State <> ConnectionState.Closed Then
                Me._conn.Close()
            End If

            Me._conn.Dispose()

            Return report
        End Try

        Try
            savetransaction = Me._conn.BeginTransaction()
            Dim cnt As Integer = 0
            For Each _SQL As String In _ListSQL
                cnt += 1
                errMsg = cnt.ToString & " " & _SQL
                Me._myCommand = New OracleCommand()
                Me._myCommand.Connection = Me._conn
                Me._myCommand.CommandText = _SQL
                Me._myCommand.Transaction = savetransaction
                Me._myCommand.ExecuteNonQuery()
            Next

            For Each table As DataTable In ds.Tables
                Dim listInsertSQL As New List(Of String)
                Dim colDataType As New List(Of String)
                Dim SQL As New StringBuilder()
                With SQL
                    .Append("INSERT INTO ")
                    .Append(table.TableName)
                    .Append(" (")

                    For Each col As DataColumn In table.Columns
                        .Append(col.ColumnName)
                        .Append(",")
                        'Get the column datatype
                        colDataType.Add(col.DataType.ToString())
                    Next

                    'Remove the extra comma
                    .Remove(SQL.Length - 1, 1)
                    .Append(")")
                    .Append("VALUES")
                    .Append("(")
                End With

                For Each row As DataRow In table.Rows
                    Dim insertSTR As New StringBuilder()

                    For index As Integer = 0 To colDataType.Count - 1

                        With insertSTR
                            Select Case colDataType(index)
                                Case "System.DateTime"
                                    If IsDate(row(index)) Then
                                        Dim datevalue As Date = CDate(row(index))

                                        .Append(String.Format("TO_DATE('"))
                                        .Append(datevalue.ToString("MM/dd/yyyy"))
                                        .Append(" ")
                                        .Append(datevalue.Hour.ToString("00"))
                                        .Append(datevalue.Minute.ToString(":00"))
                                        .Append(datevalue.Second.ToString(":00"))
                                        .Append(String.Format("', 'mm/dd/yyyy hh24:mi:ss')"))
                                        .Append(",")
                                    Else
                                        .Append(" NULL ")
                                        .Append(",")
                                    End If

                                Case "System.String"
                                    If Not IsDBNull(row(index)) Then
                                        .Append("'")
                                        .Append(CStr(row(index)))
                                        .Append("',")
                                    Else
                                        .Append("'',")
                                    End If
                                Case Else
                                    If Not IsDBNull(row(index)) Then
                                        .Append(CStr(row(index)))
                                        .Append(",")
                                    Else
                                        .Append(" NULL ")
                                        .Append(",")
                                    End If

                            End Select
                        End With
                    Next

                    'Remove the extra comma
                    insertSTR.Remove(insertSTR.Length - 1, 1)
                    insertSTR.Append(")")

                    listInsertSQL.Add(SQL.ToString() & insertSTR.ToString())
                Next

                For Each _SQL As String In listInsertSQL
                    Me._myCommand = New OracleCommand()
                    Me._myCommand.Connection = Me._conn
                    Me._myCommand.CommandText = _SQL
                    Me._myCommand.Transaction = savetransaction
                    Me._myCommand.ExecuteNonQuery()
                Next
            Next
            savetransaction.Commit()
        Catch ex As ApplicationException
            report.ErrorMessage = ex.Message & vbNewLine & errMsg
            savetransaction.Rollback()
        Catch ex1 As OracleException
            If ex1.Number = 1756 Then
                report.ErrorMessage = "Single quote is not allowed. Please remove all single quote in all fields"
            Else
                report.ErrorMessage = ex1.Message & vbNewLine & errMsg
            End If
            savetransaction.Rollback()
        Catch ex2 As Exception
            report.ErrorMessage = ex2.Message & vbNewLine & errMsg
            savetransaction.Rollback()
        Finally
            If Me._conn.State <> ConnectionState.Closed Then
                Me._conn.Close()
            End If
            _ListSQL = Nothing
            savetransaction.Dispose()
            Me._myCommand.Dispose()
            Me._conn.Dispose()
        End Try
        Return report
    End Function

    Public Function ExecuteSaveQuery2(ByVal _ListSQL As List(Of String), ByVal progress As IProgress(Of ProgressClass),
                                   ByVal ct As CancellationToken) As DataReport
        Dim savetransaction As OracleTransaction = Nothing
        Dim report As New DataReport
        Dim errMsg As String = ""
        Dim newProgress As ProgressClass = New ProgressClass
        Try
            Me.OpenConnection()
        Catch ex As Exception
            report.ErrorMessage = ex.Message

            If Me._conn.State <> ConnectionState.Closed Then
                Me._conn.Close()
            End If

            Me._conn.Dispose()

            Return report
        End Try

        Try
            savetransaction = Me._conn.BeginTransaction()
            Dim cnt As Integer = 0
            For Each _SQL As String In _ListSQL
                If ct.IsCancellationRequested Then
                    Throw New OperationCanceledException
                End If
                cnt += 1
                errMsg = cnt.ToString & " " & _SQL
                Me._myCommand = New OracleCommand()
                Me._myCommand.Connection = Me._conn
                Me._myCommand.CommandText = _SQL
                Me._myCommand.Transaction = savetransaction
                Me._myCommand.ExecuteNonQuery()
                If (cnt Mod 1000) = 0 Then
                    Dim percent As Decimal = Math.Round(CDec(cnt / _ListSQL.Count()), 2)
                    newProgress.ProgressMsg = "Executing SQL scripts for saving: " & percent.ToString("P")
                    progress.Report(newProgress)
                End If
            Next
            savetransaction.Commit()
        Catch ex As ApplicationException
            savetransaction.Rollback()
            report.ErrorMessage = ex.Message & vbNewLine & errMsg
        Catch ex1 As OracleException
            If ex1.Number = 1756 Then
                report.ErrorMessage = "Single quote is not allowed. Please remove all single quote in all fields"
            Else
                report.ErrorMessage = ex1.Message & vbNewLine & errMsg
            End If
            savetransaction.Rollback()
        Catch exCancel As OperationCanceledException
            savetransaction.Rollback()
            report.ErrorMessage = "Saving in db is requested to cancel!"
        Catch ex2 As Exception
            savetransaction.Rollback()
            report.ErrorMessage = ex2.Message & vbNewLine & errMsg
        Finally
            If Me._conn.State <> ConnectionState.Closed Then
                Me._conn.Close()
            End If
            _ListSQL = Nothing
            savetransaction.Dispose()
            Me._myCommand.Dispose()
            Me._conn.Dispose()
        End Try
        Return report
    End Function

#End Region

#Region "ExecuteSaveQuery"
    Public Function ExecuteSaveQueryAsync(ByVal _ListSQL As List(Of String), ByVal taskProgress As IProgress(Of Integer)) As DataReport
        Dim saveTransaction As OracleTransaction = Nothing
        Dim report As New DataReport
        Dim errMsg As String = ""
        Try
            Me.OpenConnection()
        Catch ex As Exception
            report.ErrorMessage = ex.Message

            If Me._conn.State <> ConnectionState.Closed Then
                Me._conn.Close()
            End If

            Me._conn.Dispose()

            Return report
        End Try

        Try
            saveTransaction = Me._conn.BeginTransaction()
            Dim cnt As Integer = 0
            For Each _SQL As String In _ListSQL
                cnt += 1
                errMsg = cnt.ToString & " " & _SQL
                Me._myCommand = New OracleCommand()
                Me._myCommand.Connection = Me._conn
                Me._myCommand.CommandText = _SQL
                Me._myCommand.Transaction = saveTransaction
                Me._myCommand.ExecuteNonQuery()
            Next

            saveTransaction.Commit()
        Catch ex As ApplicationException
            report.ErrorMessage = ex.Message & vbNewLine & errMsg
            saveTransaction.Rollback()
        Catch ex1 As OracleException
            If ex1.Number = 1756 Then
                report.ErrorMessage = "Single quote is not allowed. Please remove all single quote in all fields"
            Else
                report.ErrorMessage = ex1.Message & vbNewLine & errMsg
            End If
            saveTransaction.Rollback()
        Catch ex2 As Exception
            report.ErrorMessage = ex2.Message & vbNewLine & errMsg
            saveTransaction.Rollback()
        Finally
            If Me._conn.State <> ConnectionState.Closed Then
                Me._conn.Close()
            End If
            _ListSQL = Nothing
            saveTransaction.Dispose()
            Me._myCommand.Dispose()
            Me._conn.Dispose()
        End Try
        Return report
    End Function
#End Region

#Region "ExecuteUpdateQueryBulk"
    Public Function ExecuteUpdateQueryBulk(ByVal _ListSQL As List(Of String)) As DataReport

        Dim savetransaction As OracleTransaction = Nothing
        Dim report As New DataReport

        Try
            Me.OpenConnection()
            savetransaction = Me._conn.BeginTransaction()
            For Each _SQL As String In _ListSQL
                Me._myCommand = New OracleCommand()
                Me._myCommand.Connection = Me._conn
                Me._myCommand.CommandText = _SQL
                Me._myCommand.Transaction = savetransaction
                Me._myCommand.ExecuteNonQuery()
            Next

            savetransaction.Commit()
        Catch ex As ApplicationException
            report.ErrorMessage = ex.Message
            savetransaction.Rollback()
        Finally
            If Me._conn.State <> ConnectionState.Closed Then
                Me._conn.Close()
            End If

            savetransaction.Dispose()
            Me._myCommand.Dispose()
            Me._conn.Dispose()
        End Try

        Return report
    End Function
#End Region

#Region "WithParameters"
    Public Sub ExecuteNonQuery(cmd As DALCommand)
        Try
            Me.OpenConnection()
            Dim oraCmd = New OracleCommand(cmd.Sql, _conn)

            If cmd.Args IsNot Nothing Then
                Dim paramCtr = 0
                For Each o As Object In cmd.Args
                    oraCmd.Parameters.Add(New OracleParameter("@" & paramCtr, o))
                    paramCtr += 1
                Next
            End If
            oraCmd.ExecuteNonQuery()

        Catch e As Exception
            Throw New Exception(e.Message, e.InnerException)
        Finally
            If _conn IsNot Nothing Then
                If _conn.State = ConnectionState.Open Then
                    _conn.Close()
                End If
                _conn.Dispose()
            End If
        End Try
    End Sub
#End Region

#Region "ExecuteToCreateDataTable"
    Public Function ExecuteAndCreateDataTable(ByVal SQLQuery As String) As DataTable
        Dim Ret As New DataTable
        Me.OpenConnection()

        Return Ret
    End Function
#End Region

End Class
