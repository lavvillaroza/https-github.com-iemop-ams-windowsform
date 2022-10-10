'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             DataReport
'Orginal Author:         Vladimir E.Espiritu
'File Creation Date:     August 22, 2011
'Development Group:      Software Development and Support Division
'Description:            Use by DAL Class to store the data request by user upon executing the SQL scripts.
'Arguments/Parameters:  
'Files/Database Tables: 
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		 Programmer		 Description
'   August 22, 2011      Vloody          Class initialization
'	



Option Strict On
Option Explicit On
Public Class DataReport

#Region "Initialization/Constructor"
    Public Sub New()
        Me._ErrorMessage = ""
        Me._RecordsToWrite = 0
        Me._RecordsWritten = 0
        Me._FailedToWrite = 0
        Me._ReturnedTable = New DataTable()
        Me._ReturnedIDatareader = Nothing
    End Sub
#End Region


#Region "ErrorMessage"
    Private _ErrorMessage As String
    ' <summary>
    ' Gets or sets the error message.
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public Property ErrorMessage() As String
        Get
            Return _ErrorMessage
        End Get
        Set(ByVal value As String)
            _ErrorMessage = value
        End Set
    End Property

#End Region

#Region "RecordsWrite"
    Private _RecordsToWrite As Integer
    ' <summary>
    ' Gets or sets the number of records to be written.
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public Property RecordsToWrite() As Integer
        Get
            Return _RecordsToWrite
        End Get
        Set(ByVal value As Integer)
            _RecordsToWrite = value
        End Set
    End Property

#End Region

#Region "RecordsWritten"
    Private _RecordsWritten As Integer
    ' <summary>
    ' Gets or sets the written number of records.
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public Property RecordsWritten() As Integer
        Get
            Return _RecordsWritten
        End Get
        Set(ByVal value As Integer)
            _RecordsWritten = value
        End Set
    End Property

#End Region

#Region "FailedToWrite"
    Private _FailedToWrite As Integer
    ' <summary>
    ' Get the number of records which are failed to be written.
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public ReadOnly Property FailedToWrite() As Integer
        Get
            _FailedToWrite = Me._RecordsToWrite - Me._RecordsWritten
            Return _FailedToWrite
        End Get
    End Property

#End Region

#Region "ReturnedTable"
    Private _ReturnedTable As DataTable
    ' <summary>
    ' Gets or sets the datable usually coming from retrieved records in database.
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public Property ReturnedTable() As DataTable
        Get
            Return _ReturnedTable
        End Get
        Set(ByVal value As DataTable)
            _ReturnedTable = value
        End Set
    End Property

#End Region

#Region "ReturnedIDataReader"
    Private _ReturnedIDatareader As IDataReader
    ' <summary>
    ' Gets or sets IDatareader usually retrieved records from database.
    ' </summary>
    ' <value></value>
    ' <returns></returns>
    ' <remarks></remarks>
    Public Property ReturnedIDatareader() As IDataReader
        Get
            Return _ReturnedIDatareader
        End Get
        Set(ByVal value As IDataReader)
            _ReturnedIDatareader = value
        End Set
    End Property

#End Region


End Class
