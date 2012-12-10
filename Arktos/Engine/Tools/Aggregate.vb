Public MustInherit Class Aggregate
    Protected Result As Double
    Protected InitialValue As Double
    Public ReadOnly Position As Integer

    Sub New(ByVal NewPosition As Integer)
        Position = NewPosition
    End Sub

    Public Function GetResult() As Double
        GetResult = Result
        Result = InitialValue
    End Function

    Public Function PeekResult() As Double
        PeekResult = Result
    End Function

    Public MustOverride Sub AddItem(ByVal Value As Double)
End Class

Public Class AggregateCounter
    Inherits Aggregate

    Public Sub New(ByVal NewPosition As Integer)
        MyBase.New(NewPosition)
        InitialValue = 0
        Result = InitialValue
    End Sub

    Public Overrides Sub AddItem(ByVal Value As Double)
        Result = Result + 1
    End Sub
End Class

Public Class AggregateMax
    Inherits Aggregate

    Public Sub New(ByVal NewPosition As Integer)
        MyBase.New(NewPosition)
        InitialValue = Double.MinValue
        Result = InitialValue
    End Sub

    Public Overrides Sub AddItem(ByVal Value As Double)
        If (Value > Result) Then
            Result = Value
        End If
    End Sub
End Class

Public Class AggregateMin
    Inherits Aggregate

    Public Sub New(ByVal NewPosition As Integer)
        MyBase.New(NewPosition)
        InitialValue = Double.MaxValue
        Result = InitialValue
    End Sub

    Public Overrides Sub AddItem(ByVal Value As Double)
        If (Value < Result) Then
            Result = Value
        End If
    End Sub
End Class

Public Class AggregateSum
    Inherits Aggregate

    Public Sub New(ByVal NewPosition As Integer)
        MyBase.New(NewPosition)
        InitialValue = 0
        Result = InitialValue
    End Sub

    Public Overrides Sub AddItem(ByVal Value As Double)
        Result = Result + Value
    End Sub
End Class

Public Class AggregateAvg
    Inherits Aggregate

    Protected Counter As Integer

    Public Sub New(ByVal NewPosition As Integer)
        MyBase.New(NewPosition)
        InitialValue = 0.0
        Result = InitialValue
        Counter = 0
    End Sub

    Public Overrides Sub AddItem(ByVal Value As Double)
        Result = Result + Value
        Counter = Counter + 1
    End Sub

    Public Shadows Function GetResult() As Double
        GetResult = Result / Counter
        Result = InitialValue
        Counter = 0
    End Function

    Public Shadows Function PeekResult() As Double
        PeekResult = Result / Counter
    End Function
End Class