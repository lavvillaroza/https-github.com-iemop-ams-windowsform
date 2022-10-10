Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Collections

Namespace LogicalAccessManager.BO
    Public MustInherit Class LaCollection2(Of T)
        Private _items As New List(Of T)()

        Public Property Items() As List(Of T)
            Get
                Return _items
            End Get
            Set(ByVal value As List(Of T))
                _items = value
            End Set
        End Property

        Public MustOverride Sub Reload()
    End Class
End Namespace