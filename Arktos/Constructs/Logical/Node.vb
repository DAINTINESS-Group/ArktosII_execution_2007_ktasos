Public MustInherit Class Node
    Dim Ancestor, Successor As Collection
    Protected Name As String
    Public InDegree, OutDegree As Integer

    Sub New(ByVal NewName As String)
        InDegree = 0
        OutDegree = 0
        Name = NewName
        Ancestor = New Collection
        Successor = New Collection
    End Sub

    Function GetName() As String
        Return Name
    End Function

    Sub SetAncestor(ByVal NewEdge As Edge)
        Ancestor.Add(NewEdge, Ancestor.Count + 1)
    End Sub

    Sub SetSuccessor(ByVal NewEdge As Edge)
        Successor.Add(NewEdge, Successor.Count + 1)
    End Sub

    Function GetAncestor(ByVal Index As Integer) As Object
        Return Ancestor(Index).GetStart()
    End Function

    Function GetSuccessor(ByVal Index As Integer) As Object
        Return Successor(Index).GetEnd()
    End Function
End Class
