Public MustInherit Class ArktosScheduler
    Public MaxMemory, SumMemory, Counter As Long, SchedulerList As SortedList
    Protected ItemCount As Integer

    Sub New(ByVal MonitorList As Collection)
        SchedulerList = New SortedList
        CreateList(MonitorList)
        MaxMemory = 0 : SumMemory = 0 : Counter = 0
    End Sub

    Private Sub CreateList(ByVal MonitorList As Collection)
        For I As Integer = 1 To MonitorList.Count
            SchedulerList.Add(MonitorList.Item(I).GetId(), MonitorList.Item(I))
        Next
        ItemCount = SchedulerList.Count
    End Sub

    Public Sub RemoveFromList(ByVal ThreadId As Integer)
        RemoveItem(ThreadId)
        SchedulerList.Remove(ThreadId)
        ItemCount = SchedulerList.Count
        'Console.WriteLine("    Removing {0} from a total of {1}", ThreadId, ItemCount)
    End Sub

    Public MustOverride Function NextActivity() As Integer
    Protected MustOverride Sub RemoveItem(ByVal ThreadId As Integer)
End Class

Public Class RoundRobinScheduler
    Inherits ArktosScheduler

    Private Position As Integer

    Sub New(ByVal MonitorList As Collection)
        MyBase.New(MonitorList)
    End Sub

    Public Overrides Function NextActivity() As Integer
        NextActivity = SchedulerList.GetByIndex(Position).GetId()
        Position = (Position + 1) Mod ItemCount
        'Console.WriteLine("Selecting Item {0}", NextActivity)
    End Function

    Protected Overrides Sub RemoveItem(ByVal ThreadId As Integer)
        If (Position > 0) Then
            Position = Position - 1
        End If
    End Sub
End Class

Public Class MinCost
    Inherits ArktosScheduler

    Dim MaxInput As Integer, CurrentItem As ExecutionItem

    Sub New(ByVal MonitorList As Collection)
        MyBase.New(MonitorList)
    End Sub

    Public Overrides Function NextActivity() As Integer
        MaxInput = -1
        NextActivity = -1
        For I As Integer = 0 To ItemCount - 1
            CurrentItem = SchedulerList.GetByIndex(I)
            'Console.WriteLine("           For Item {0} the size is {1}", CurrentItem.GetId(), CurrentItem.InputQueueSize())
            If (MaxInput < CurrentItem.InputQueueSize()) Then
                NextActivity = CurrentItem.GetId()
                MaxInput = CurrentItem.InputQueueSize()
            End If
        Next
        'Console.WriteLine("           Selecting Item {0}", NextActivity)
    End Function

    Protected Overrides Sub RemoveItem(ByVal ThreadId As Integer)

    End Sub
End Class

Public Class MinMemory
    Inherits ArktosScheduler

    Private MaxMemoryConsumption As Double, CurrentItem As ExecutionItem
    Private MaxInput, MCNextActivity As Integer

    Sub New(ByVal MonitorList As Collection)
        MyBase.New(MonitorList)
    End Sub

    Public Overrides Function NextActivity() As Integer
        MaxInput = -1
        MaxMemoryConsumption = Double.MinValue
        For I As Integer = 0 To ItemCount - 1
            CurrentItem = SchedulerList.GetByIndex(I)
            'Console.WriteLine(" || For Item {0} the estimate is {1} (Input Size {2})", CurrentItem.GetId(), CurrentItem.MemoryBenefit(6), CurrentItem.InputQueueSize())
            If (MaxMemoryConsumption < CurrentItem.MemoryBenefit()) Then
                NextActivity = CurrentItem.GetId()
                MaxMemoryConsumption = CurrentItem.MemoryBenefit()
            End If
            If (MaxInput < CurrentItem.InputQueueSize()) Then
                MCNextActivity = CurrentItem.GetId()
                MaxInput = CurrentItem.InputQueueSize()
            End If
        Next
        If (MaxMemoryConsumption <= 0) Then
            'Console.Write("MC!!")
            NextActivity = MCNextActivity
        End If
        'Console.WriteLine("           Selecting Item {0}", NextActivity)
        'Console.ReadLine()
    End Function

    Protected Overrides Sub RemoveItem(ByVal ThreadId As Integer)

    End Sub
End Class
