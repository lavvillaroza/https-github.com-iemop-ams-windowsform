Imports System.Collections.Generic
Imports System.Text
Imports Oracle.DataAccess.Client
Imports System.Data

Namespace Library.OraDAL
    Public Class DataAccess
        Public Shared Function ParseDatasource(ByVal datasource As String, ByVal userId As String, ByVal password As String) As String
            Dim oraConnStr = New OracleConnectionStringBuilder()
            oraConnStr.DataSource = datasource
            oraConnStr.UserID = userId
            oraConnStr.Password = password
            Return oraConnStr.ConnectionString
        End Function

        Private Shared Function ConnectionTest(ByVal connectionStr As String) As Boolean
            Dim ret = False
            Dim conn = New OracleConnection(connectionStr)

            Try
                conn.Open()
                ret = True
            Catch e As Exception
                ret = False
                Throw New Exception(e.Message, e.InnerException)
            Finally
                If conn IsNot Nothing Then
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                    End If
                    conn.Dispose()
                End If
            End Try

            Return ret
        End Function

        Public Shared Function ExecuteScalar(ByVal dataCommand As DataCommand, ByVal connectionStr As String) As Object
            Dim ret = New [Object]()

            If ConnectionTest(connectionStr) Then
                Dim conn = New OracleConnection(connectionStr)

                Try
                    conn.Open()

                    Dim cmd = New OracleCommand(dataCommand.Sql, conn)

                    If dataCommand.Args IsNot Nothing Then
                        Dim paramCtr = 0
                        For Each o As Object In dataCommand.Args
                            cmd.Parameters.Add(New OracleParameter(":" & paramCtr, o))
                            paramCtr += 1
                        Next
                    End If

                    ret = cmd.ExecuteScalar()
                Catch e As Exception
                    Throw New Exception(e.Message, e.InnerException)
                Finally
                    If conn IsNot Nothing Then
                        If conn.State = ConnectionState.Open Then
                            conn.Close()
                        End If
                        conn.Dispose()
                    End If
                End Try
            End If

            Return ret
        End Function

        Public Shared Function ExecuteScalar(ByVal dataCommand As List(Of DataCommand), ByVal connectionStr As String) As List(Of Object)
            Dim ret = New List(Of [Object])()

            If ConnectionTest(connectionStr) Then
                Dim conn = New OracleConnection(connectionStr)

                Try
                    conn.Open()

                    For Each dc As DataCommand In dataCommand
                        Dim cmd = New OracleCommand(dc.Sql, conn)

                        If dc.Args IsNot Nothing Then
                            Dim paramCtr = 0
                            For Each o As Object In dc.Args
                                cmd.Parameters.Add(New OracleParameter(":" & paramCtr, o))
                                paramCtr += 1
                            Next
                        End If

                        ret.Add(cmd.ExecuteScalar())
                    Next
                Catch e As Exception
                    Throw New Exception(e.Message, e.InnerException)
                Finally
                    If conn IsNot Nothing Then
                        If conn.State = ConnectionState.Open Then
                            conn.Close()
                        End If
                        conn.Dispose()
                    End If
                End Try
            End If

            Return ret
        End Function

        Public Shared Function ExecuteQuery(ByVal dataCommand As DataCommand, ByVal connectionStr As String) As DataTable
            Dim ret = New DataTable()

            If ConnectionTest(connectionStr) Then
                Dim conn = New OracleConnection(connectionStr)

                Try
                    conn.Open()

                    Dim cmd = New OracleCommand(dataCommand.Sql, conn)

                    If dataCommand.Args IsNot Nothing Then
                        Dim paramCtr = 0
                        For Each o As Object In dataCommand.Args
                            cmd.Parameters.Add(New OracleParameter(":" & paramCtr, o))
                            paramCtr += 1
                        Next
                    End If

                    Dim da = New OracleDataAdapter(cmd)
                    da.Fill(ret)                    
                Catch e As Exception
                    Throw New Exception(e.Message, e.InnerException)
                Finally
                    If conn IsNot Nothing Then
                        If conn.State = ConnectionState.Open Then
                            conn.Close()
                        End If
                        conn.Dispose()
                    End If
                End Try
            End If

            Return ret
        End Function

        Public Shared Function ExecuteQuery(ByVal dataCommand As List(Of DataCommand), ByVal connectionStr As String) As List(Of DataTable)
            Dim ret = New List(Of DataTable)()

            If ConnectionTest(connectionStr) Then
                Dim conn = New OracleConnection(connectionStr)

                Try
                    conn.Open()

                    Dim tableCtr = 0
                    For Each dc As DataCommand In dataCommand
                        Dim cmd = New OracleCommand(dc.Sql, conn)

                        If dc.Args IsNot Nothing Then
                            Dim paramCtr = 0
                            For Each o As Object In dc.Args
                                cmd.Parameters.Add(New OracleParameter(":" & paramCtr, o))
                                paramCtr += 1
                            Next
                        End If

                        Dim da = New OracleDataAdapter(cmd)
                        Dim dt = New DataTable("Table" & tableCtr)
                        da.Fill(dt)

                        ret.Add(dt)
                        tableCtr += 1
                    Next
                Catch e As Exception
                    Throw New Exception(e.Message, e.InnerException)
                Finally
                    If conn IsNot Nothing Then
                        If conn.State = ConnectionState.Open Then
                            conn.Close()
                        End If
                        conn.Dispose()
                    End If
                End Try
            End If

            Return ret
        End Function

        Public Shared Sub ExecuteNonQuery(ByVal dataCommand As DataCommand, ByVal connectionStr As String)
            If ConnectionTest(connectionStr) Then
                Dim conn = New OracleConnection(connectionStr)

                Try
                    conn.Open()

                    Dim cmd = New OracleCommand(dataCommand.Sql, conn)

                    If dataCommand.Args IsNot Nothing Then
                        Dim paramCtr = 0
                        For Each o As Object In dataCommand.Args
                            cmd.Parameters.Add(New OracleParameter(":" & paramCtr, o))
                            paramCtr += 1
                        Next
                    End If

                    cmd.ExecuteNonQuery()
                Catch e As Exception
                    Throw New Exception(e.Message, e.InnerException)
                Finally
                    If conn IsNot Nothing Then
                        If conn.State = ConnectionState.Open Then
                            conn.Close()
                        End If
                        conn.Dispose()
                    End If
                End Try
            End If
        End Sub

        Public Shared Sub ExecuteNonQuery(ByVal dataCommand As List(Of DataCommand), ByVal connectionStr As String)
            If ConnectionTest(connectionStr) Then
                Dim conn = New OracleConnection(connectionStr)
                Dim trans As OracleTransaction = Nothing

                Try
                    conn.Open()

                    trans = conn.BeginTransaction()

                    For Each dc As DataCommand In dataCommand
                        Dim cmd = New OracleCommand(dc.Sql, conn)

                        If dc.Args IsNot Nothing Then
                            Dim paramCtr = 0
                            For Each o As Object In dc.Args
                                cmd.Parameters.Add(New OracleParameter(":" & paramCtr, o))
                                paramCtr += 1
                            Next
                        End If

                        cmd.ExecuteNonQuery()
                    Next

                    trans.Commit()
                Catch e As Exception
                    trans.Rollback()
                    Throw New Exception(e.Message, e.InnerException)
                Finally
                    If conn IsNot Nothing Then
                        If conn.State = ConnectionState.Open Then
                            conn.Close()
                        End If
                        conn.Dispose()
                    End If
                End Try
            End If
        End Sub
    End Class
End Namespace