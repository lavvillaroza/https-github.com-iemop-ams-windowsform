<Serializable()> _
Public Class ParticipantRepresentative
    Public Sub New()

    End Sub
    Public Sub New(ByVal fName As String, ByVal mName As String, ByVal lName As String,
                   ByVal position As String)
        Me._FName = fName
        Me._MName = mName
        Me._LName = lName
        Me._Position = position
    End Sub
    Public Sub New(ByVal title As String, ByVal fName As String, ByVal mName As String, ByVal lName As String,
                   ByVal position As String, ByVal contact As String, ByVal email As String)
        Me._Title = title
        Me._FName = fName
        Me._MName = mName
        Me._LName = lName
        Me._Position = position
        Me._Contact = contact
        Me._EmailAddress = email
    End Sub
#Region "FullName"
    Public ReadOnly Property GetFullName() As String
        Get
            Dim getRepFullName As String = ""
            If Title.Length <> 0 Then
                getRepFullName = If(Right(Title, 1) = ".", Title, Title & ".")
            End If
            If FName.Length <> 0 Then
                getRepFullName &= " " & FName
            End If
            If MName.Length <> 0 Then
                getRepFullName &= " " & If(Right(MName, 1) = ".", MName, Left(MName, 1) & ".")
            End If
            If LName.Length <> 0 Then
                getRepFullName &= " " & LName
            End If
            Return getRepFullName
        End Get
    End Property
#End Region

#Region "TitleAndSurname"
    Public ReadOnly Property GetTitleSurname() As String
        Get
            Dim getRepFullName As String = ""
            If Title.Length <> 0 Then
                getRepFullName = If(Right(Title, 1) = ".", Title, Title & ".")
            End If
            If LName.Length <> 0 Then
                getRepFullName &= " " & LName
            End If
            Return getRepFullName
        End Get
    End Property
#End Region

#Region "Title"
    Private _Title As String
    Public Property Title() As String
        Get
            Return _Title
        End Get
        Set(ByVal value As String)
            _Title = value
        End Set
    End Property
#End Region

#Region "FName"
    Private _FName As String
    Public Property FName() As String
        Get
            Return _FName
        End Get
        Set(ByVal value As String)
            _FName = value
        End Set
    End Property
#End Region

#Region "MName"
    Private _MName As String
    Public Property MName() As String
        Get
            Return _MName
        End Get
        Set(ByVal value As String)
            _MName = value
        End Set
    End Property
#End Region

#Region "LName"
    Private _LName As String
    Public Property LName() As String
        Get
            Return _LName
        End Get
        Set(ByVal value As String)
            _LName = value
        End Set
    End Property
#End Region

#Region "Position"
    Private _Position As String
    Public Property Position() As String
        Get
            Return _Position
        End Get
        Set(ByVal value As String)
            _Position = value
        End Set
    End Property

#End Region

#Region "Contact"
    Private _Contact As String
    Public Property Contact() As String
        Get
            Return _Contact
        End Get
        Set(ByVal value As String)
            _Contact = value
        End Set
    End Property

#End Region

#Region "EmailAddress"
    Private _EmailAddress As String
    Public Property EmailAddress() As String
        Get
            Return _EmailAddress
        End Get
        Set(ByVal value As String)
            _EmailAddress = value
        End Set
    End Property
#End Region

End Class
