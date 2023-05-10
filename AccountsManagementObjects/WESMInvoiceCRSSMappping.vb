Public Class WESMInvoiceCRSSMappping
    Sub New()
        Me.New("", "", "", "")
    End Sub
    Sub New(ByVal idNumber As String, ByVal regId As String, ByVal newRegId As String, ByVal remarks As String)
        Me._IDNumber = idNumber
        Me._RegIDNumber = regId
        Me._NewRegIDNumber = newRegId
        Me._Remarks = remarks
    End Sub
    Private _IDNumber As String
    Public Property IDNumber() As String
        Get
            Return _IDNumber
        End Get
        Set(ByVal value As String)
            _IDNumber = value
        End Set
    End Property

    Private _RegIDNumber As String
    Public Property RegIDNumber() As String
        Get
            Return _RegIDNumber
        End Get
        Set(ByVal value As String)
            _RegIDNumber = value
        End Set
    End Property

    Private _NewRegIDNumber As String
    Public Property NewRegIDNumber() As String
        Get
            Return _NewRegIDNumber
        End Get
        Set(ByVal value As String)
            _NewRegIDNumber = value
        End Set
    End Property

    Private _Remarks As String
    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property
End Class
