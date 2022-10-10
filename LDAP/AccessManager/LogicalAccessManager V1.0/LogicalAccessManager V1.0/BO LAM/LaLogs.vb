Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Namespace LogicalAccessManager.BO
    Public Class LaLogs
        Private m_transactiondate As DateTime
        Public Property TransactionDate() As DateTime
            Get
                Return m_transactiondate
            End Get
            Set(value As DateTime)
                m_transactiondate = value
            End Set
        End Property

        Private m_applicationname As String
        Public Property ApplicationName() As String
            Get
                Return m_applicationname
            End Get
            Set(value As String)
                m_applicationname = value
            End Set
        End Property

        Private m_modulename As String
        Public Property ModuleName As String
            Get
                Return m_modulename
            End Get
            Set(value As String)
                m_modulename = value
            End Set
        End Property

        Private m_computername As String
        Public Property ComputerName() As String
            Get
                Return m_computername
            End Get
            Set(value As String)
                m_computername = value
            End Set
        End Property

        Private m_ipaddress As String
        Public Property IPAddress() As String
            Get
                Return m_ipaddress
            End Get
            Set(value As String)
                m_ipaddress = value
            End Set
        End Property

        Private m_misc1 As String
        Public Property Misc1() As String
            Get
                Return m_misc1
            End Get
            Set(value As String)
                m_misc1 = value
            End Set
        End Property

        Private m_misc2 As String
        Public Property Misc2() As String
            Get
                Return m_misc2
            End Get
            Set(value As String)
                m_misc2 = value
            End Set
        End Property

        Private m_misc3 As String
        Public Property Misc3() As String
            Get
                Return m_misc3
            End Get
            Set(value As String)
                m_misc3 = value
            End Set
        End Property

        Private m_colorcode As String
        Public Property ColorCode() As String
            Get
                Return m_colorcode
            End Get
            Set(value As String)
                m_colorcode = value
            End Set
        End Property

        Private m_logtype As String
        Public Property LogType() As String
            Get
                Return m_logtype
            End Get
            Set(value As String)
                m_logtype = value
            End Set
        End Property

        Private m_updatedby As String
        Public Property UpdatedBy() As String
            Get
                Return m_updatedby
            End Get
            Set(value As String)
                m_updatedby = value
            End Set
        End Property
    End Class
End Namespace