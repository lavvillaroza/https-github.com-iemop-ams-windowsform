Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace LogicalAccess.LDAP.BO
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
                value = _TransactionDate
            End Set
        End Property

        Public Property ApplicationName() As String
            Get
                Return _ApplicationName
            End Get
            Set(value As String)
                value = _ApplicationName
            End Set
        End Property

        Public Property ModuleName() As String
            Get
                Return _ModuleName
            End Get
            Set(value As String)
                value = _ModuleName
            End Set
        End Property

        Public Property ComputerName() As String
            Get
                Return _ComputerName
            End Get
            Set(value As String)
                value = _ComputerName
            End Set
        End Property

        Public Property IpAddress() As String
            Get
                Return _IpAddress
            End Get
            Set(value As String)
                value = _IpAddress
            End Set
        End Property

        Public Property Misc1() As String
            Get
                Return _Misc1
            End Get
            Set(value As String)
                value = _Misc1
            End Set
        End Property

        Public Property Misc2() As String
            Get
                Return _Misc2
            End Get
            Set(value As String)
                value = _Misc2
            End Set
        End Property

        Public Property Misc3() As String
            Get
                Return _Misc3
            End Get
            Set(value As String)
                value = _Misc3
            End Set
        End Property

        Public Property ColorCode() As String
            Get
                Return _ColorCode
            End Get
            Set(value As String)
                value = _ColorCode
            End Set
        End Property

        Public Property LogType() As String
            Get
                Return _LogType
            End Get
            Set(value As String)
                value = _LogType
            End Set
        End Property

        Public Property UpdatedBy() As String
            Get
                Return _UpdatedBy
            End Get
            Set(value As String)
                value = _UpdatedBy
            End Set
        End Property

    End Class
End Namespace

