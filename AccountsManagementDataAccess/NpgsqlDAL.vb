'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             NPGSQLDAL
'Orginal Author:         Lance Arjay Villaroza
'File Creation Date:     February 09, 2018
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
Imports Npgsql
Imports System.Data
Imports System.Text
Imports System.Exception
Imports System.Configuration
Public Class NpgsqlDAL
#Region "Member Variables"
    Private _conn As NpgsqlConnection
    Private _myAdapter As NpgsqlDataAdapter
    Private _myCommand As NpgsqlCommand
    Private _myReader As NpgsqlDataReader
#End Region

#Region "Single Instance Code"
    Private Shared m_Instance As NpgsqlDAL = Nothing
    Public Shared Function GetInstance() As NpgsqlDAL
        If m_Instance Is Nothing Then
            m_Instance = New NpgsqlDAL()
        End If
        Return m_Instance
    End Function

    Private Sub New()
        Me._ConnectionString = ""
    End Sub

    Private _DALAcess As NpgsqlDAL
    Public ReadOnly Property DALAcess() As NpgsqlDAL
        Get
            Return Me._DALAcess
        End Get
    End Property

    Private _ConnectionString As String
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
        Me._conn = New NpgsqlConnection(Me._ConnectionString)
        'Open the Connection
        Try
            If Me._conn.State <> ConnectionState.Open Then
                Me._conn.Open()
            End If

        Catch ex As NpgsqlException
            Throw New ApplicationException("The connection is closed or cannot connect to the database")
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

    End Sub
#End Region

#Region "Test Connection"
    Public Function VerifyConnection(ByVal constr As String) As Boolean
        Using oraconn As New NpgsqlConnection(constr)
            Try
                oraconn.Open()
                VerifyConnection = True
            Catch ex As NpgsqlException
                VerifyConnection = False
                Exit Function
            Finally
                oraconn.Close()
            End Try
        End Using
    End Function
#End Region

#Region "ExecuteSelectQueryReturningDataReader"
    Public Function ExecuteSelectQueryReturningDataReader(ByVal _SQL As String) As DataReport
        Dim report As New DataReport
        Me._myReader = Nothing
        Try
            Me.OpenConnection()
            Try
                Me._myCommand = New NpgsqlCommand(_SQL, _conn)
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
    Public Async Function ExecuteSelectQueryReturningDataReaderAsync(ByVal _SQL As String) As Threading.Tasks.Task(Of DataReport)
        Dim report As New DataReport
        Me._myReader = Nothing
        Try
            Me.OpenConnection()
            Try
                Me._myCommand = New NpgsqlCommand(_SQL, _conn)
                Me._myReader = Await Me._myCommand.ExecuteReaderAsync(CommandBehavior.CloseConnection)
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
End Class
