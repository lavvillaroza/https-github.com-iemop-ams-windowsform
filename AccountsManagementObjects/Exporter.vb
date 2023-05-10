'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             Exporter
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     December 14, 2011
'Development Group:      Software Development and Support Division
'Description:            Class for Charges
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   December 14, 2011         Vladimir E. Espiritu            Class initialization
'


Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Text
Imports System.IO

Public Class Exporter

#Region "Initialization/Constructor"
    Public Sub New()
        Me._TextDelimiter = ","c
        Me._TextQualifiers = """"c
        Me._HasColumnHeaders = True
    End Sub
#End Region

#Region "TextDelimiter"
    Private _TextDelimiter As Char
    Public Property TextDelimiter() As Char
        Get
            Return _TextDelimiter
        End Get
        Set(ByVal value As Char)
            _TextDelimiter = value
        End Set
    End Property
#End Region

#Region "TextQualifiers"
    Private _TextQualifiers As Char
    Public Property TextQualifiers() As Char
        Get
            Return _TextQualifiers
        End Get
        Set(ByVal value As Char)
            _TextQualifiers = value
        End Set
    End Property
#End Region
    
#Region "HasColumnHeaders"
    Private _HasColumnHeaders As Boolean
    Public Property HasColumnHeaders() As Boolean
        Get
            Return _HasColumnHeaders
        End Get
        Set(ByVal value As Boolean)
            _HasColumnHeaders = value
        End Set
    End Property
#End Region

#Region "CsvFromDatatable"
    Public Function CsvFromDatatable(ByVal InputTable As DataTable) As String
        Dim CsvBuilder As New StringBuilder()
        If HasColumnHeaders Then
            CreateHeader(InputTable, CsvBuilder)
        End If
        CreateRows(InputTable, CsvBuilder)
        Return CsvBuilder.ToString()
    End Function
#End Region
    
#Region "CreateRows"
    Private Sub CreateRows(ByVal InputTable As DataTable, ByVal CsvBuilder As StringBuilder)
        For Each ExportRow As DataRow In InputTable.Rows
            For Each ExportColumn As DataColumn In InputTable.Columns
                Dim ColumnText As String = ExportRow(ExportColumn.ColumnName).ToString()
                ColumnText = ColumnText.Replace(TextQualifiers.ToString(), TextQualifiers.ToString() + TextQualifiers.ToString())
                CsvBuilder.Append(TextQualifiers + ColumnText + TextQualifiers)
                CsvBuilder.Append(TextDelimiter)
            Next
            CsvBuilder.AppendLine()
        Next
    End Sub
#End Region

#Region "CreateHeader"
    Private Sub CreateHeader(ByVal InputTable As DataTable, ByVal CsvBuilder As StringBuilder)
        For Each ExportColumn As DataColumn In InputTable.Columns
            Dim ColumnText As String = ExportColumn.ColumnName.ToString()
            ColumnText = ColumnText.Replace(TextQualifiers.ToString(), TextQualifiers.ToString() + TextQualifiers.ToString())
            CsvBuilder.Append(TextQualifiers + ExportColumn.ColumnName + TextQualifiers)
            CsvBuilder.Append(TextDelimiter)
        Next
        CsvBuilder.AppendLine()
    End Sub
#End Region
    
End Class

