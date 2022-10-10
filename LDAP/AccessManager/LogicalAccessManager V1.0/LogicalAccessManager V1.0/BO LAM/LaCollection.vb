Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Collections

Namespace LogicalAccessManager.BO
    Public MustInherit Class LaCollection(Of T)
        Private _items As New List(Of T)()
        Private _curPage As Integer = 0
        Private _limit As Integer = 100
        Private _pageCount As Integer
        Private output As Integer

        Friend ReadOnly Property ConnectionInfo() As ConnectionDetails
            Get
                Return ConnectionDetails.Instance
            End Get
        End Property

        Public Property Items() As List(Of T)
            Get
                Return _items
            End Get
            Set(ByVal value As List(Of T))
                _items = value
            End Set
        End Property


        Public ReadOnly Property PageCount() As Integer
            Get                                
                Return If((_items.Count / _limit) < 1, 1, Math.Round((_items.Count / _limit) + IIf(Integer.TryParse(_items.Count / _limit, output), 0, 1), 0))
            End Get
        End Property


        Public Function PageAt(ByVal pageIndex As Integer) As List(Of T)
            Dim take = MustTake(pageIndex)
            Dim skip = MustSkip(pageIndex)
            Return _items.Take(take).Skip(skip).ToList()
        End Function

        Private Function MustTake(ByVal pageIndex As Integer) As Integer
            Return _limit * pageIndex
        End Function

        Private Function MustSkip(ByVal pageIndex As Integer) As Integer
            Return (_limit * pageIndex) - _limit
        End Function

        Public MustOverride Sub Reload()
    End Class
End Namespace