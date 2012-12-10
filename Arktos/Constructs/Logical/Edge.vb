Public Class Edge
    Dim Name As String
    Dim BeginOutput, EndInput As String
    Dim BeginNode, EndNode As Node

    Public Sub New(ByVal NewBeginNode As Node, ByVal NewEndNode As Node)
        BeginNode = NewBeginNode
        EndNode = NewEndNode
        Name = BeginNode.GetName + "->" + EndNode.GetName
    End Sub

    Public Sub SetEndInput(ByVal InputEdge As String)
        EndInput = InputEdge
    End Sub

    Public Sub SetStartOutput(ByVal OutputEdge As String)
        BeginOutput = OutputEdge
    End Sub

    Public Function GetName() As String
        Return Name
    End Function

    Public Function GetStart() As Node
        Return BeginNode
    End Function

    Public Function GetEnd() As Node
        Return EndNode
    End Function

    Function GetStartOutput() As String
        Return BeginOutput
    End Function

    Function GetEndInput() As String
        Return EndInput
    End Function
End Class
