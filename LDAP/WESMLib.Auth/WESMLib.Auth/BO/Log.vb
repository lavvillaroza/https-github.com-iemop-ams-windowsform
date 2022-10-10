Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace WESMLib.Auth.BO
    Public Class Log
        Private _TransactionDate As DateTime
        Private _ApplicationName As String
        Private _ModuleName As String
        Private _ComputerName As String
        Private _IpAddress As String
        Private _Misc1 As String
        Private _Misc2 As String
        Private _Misc3 As String
        Private _ColorCode As String
        Private _LogType As String
        Private _UpdatedBy As String

        Public Property TransactionDate() As DateTime
            Get
                Return _TransactionDate
            End Get
            Set(value As DateTime)
                _TransactionDate = value
            End Set
        End Property

        Public Property ApplicationName() As String
            Get
                Return _ApplicationName
            End Get
            Set(value As String)
                _ApplicationName = value
            End Set
        End Property

        Public Property ModuleName() As String
            Get
                Return _ModuleName
            End Get
            Set(value As String)
                _ModuleName = value
            End Set
        End Property

        Public Property ComputerName() As String
            Get
                Return _ComputerName
            End Get
            Set(value As String)
                _ComputerName = value
            End Set
        End Property

        Public Property IpAddress() As String
            Get
                Return _IpAddress
            End Get
            Set(value As String)
                _IpAddress = value
            End Set
        End Property

        Public Property Misc1() As String
            Get
                Return _Misc1
            End Get
            Set(value As String)
                _Misc1 = value
            End Set
        End Property

        Public Property Misc2() As String
            Get
                Return _Misc2
            End Get
            Set(value As String)
                _Misc2 = value
            End Set
        End Property

        Public Property Misc3() As String
            Get
                Return _Misc3
            End Get
            Set(value As String)
                _Misc3 = value
            End Set
        End Property

        Public Property ColorCode() As String
            Get
                Return _ColorCode
            End Get
            Set(value As String)
                _ColorCode = value
            End Set
        End Property

        Public Property LogType() As String
            Get
                Return _LogType
            End Get
            Set(value As String)
                _LogType = value
            End Set
        End Property

        Public Property UpdatedBy() As String
            Get
                Return _UpdatedBy
            End Get
            Set(value As String)
                _UpdatedBy = value
            End Set
        End Property

    End Class
End Namespace

