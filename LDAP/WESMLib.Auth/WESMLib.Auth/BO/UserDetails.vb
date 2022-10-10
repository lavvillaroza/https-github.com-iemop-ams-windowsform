﻿Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Data
Imports WESMLib.Auth.OraDAL
Imports Library.Core.LDAP

Namespace WESMLib.Auth.BO
    Public Class UserDetails : Implements IDisposable
        Private _connectionString As String        
        Public Function GetUserModules(username As String) As List(Of String)
            Try
                Dim ret = New List(Of String)()
                Dim cmd = New DataCommand("SELECT LUM.USER_NAME, " _
                                        & "CASE WHEN LENGTH(LU.MI)=0 THEN LU.FIRST_NAME||' '||LU.LAST_NAME " _
                                        & "WHEN NVL(LU.MI,'NULL') = 'NULL' THEN LU.FIRST_NAME||' '||LU.LAST_NAME " _
                                        & "ELSE LU.FIRST_NAME||' '||LU.MI||' '||LU.LAST_NAME " _
                                        & "END AS FULL_NAME, LU.POSITION, LAM.MODULE_NAME, LUM.UPDATED_BY, LUM.UPDATED_DATE " _
                                        & "FROM LA_USERS_MODULES LUM " _
                                        & "INNER JOIN LA_USERS LU ON LU.USER_NAME=LUM.USER_NAME " _
                                        & "INNER JOIN LA_MODULES LAM ON LAM.MODULE_ID=LUM.MODULE_ID " _
                                        & "WHERE LUM.USER_NAME='" & username & "' AND LU.STATUS = 0")

                Dim dt = DataAccess.ExecuteQuery(cmd, _connectionString)
                For Each dr As DataRow In dt.Rows
                    ret.Add(Convert.ToString(dr("MODULE_NAME")))
                    _LDAPUserInfo.Name = Convert.ToString(dr("FULL_NAME"))
                    _LDAPUserInfo.Position = If(Len(Convert.ToString(dr("POSITION").ToString)) = 0, "", Convert.ToString(dr("POSITION").ToString))
                Next
                Return ret
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        'Constructor
        Public Sub New(datasource As String, userId As String, password As String)
            _connectionString = DataAccess.ParseDatasource(datasource, userId, password)            
        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If
            End If
            Me.disposedValue = True
        End Sub
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace