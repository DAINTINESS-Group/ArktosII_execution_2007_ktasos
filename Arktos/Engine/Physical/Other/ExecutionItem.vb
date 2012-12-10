Public MustInherit Class ExecutionItem
    Public OperatorThread As New Thread(AddressOf Execute)

    Protected Id As Integer, ConsumerList, ProducerList As SortedList
    Protected OperatorStatus As New Status, NodeName As String, OnlyProducingData As Boolean
    Protected MyBox, MonitorBox As Mailbox, PackCollection As SortedList

    Protected InputCounter, OutputCounter As Long
    Private StartTime, EndTime, ExecutionTime As Long
    Private FileCounter As Integer, ExecutionClock As HiPerfTimer

    Sub New(ByVal NewName As String)
        NodeName = NewName
        ConsumerList = New SortedList
        ProducerList = New SortedList
        PackCollection = New SortedList
        MyBox = New Mailbox
        FileCounter = 0
        OnlyProducingData = False
        InputCounter = 0
        OutputCounter = 0
        StartTime = 0
        EndTime = 0
        ExecutionTime = 1   'To avoid Divisiob by zero
    End Sub

    Function GetId() As Integer
        Return Id
    End Function

    Function GetName() As String
        Return NodeName
    End Function

    Sub SetId(ByRef NewId As Integer)
        Id = NewId
        NewId = NewId + 1
    End Sub

    Sub SetMonitorBox(ByRef NewMailBox As Mailbox)
        MonitorBox = NewMailBox
    End Sub

    Function GetBox() As Mailbox
        Return MyBox
    End Function

    Function IsTupleProducer() As Boolean
        Return OnlyProducingData
    End Function

    Sub Execute()
        ExecutionClock = New HiPerfTimer
        OperatorStatus = New Status
        InitForward()
        InitExecute()
        While (Not OperatorStatus.Finished)
            InboxManagement()
            If (OperatorStatus.Stalled) Then      'Sleep if you have to
                Thread.Sleep(EngineStallTime)
            Else
                ExecutionClock.TimerStart()
                DataProcess()
                ExecutionClock.TimerStop()
                ExecutionTime += ExecutionClock.Duration()
            End If
        End While
        FinalForward()
        EndExecute()
        Console.WriteLine("Item {1} ({0}) is done {2}", Id, NodeName, OutputCounter)
    End Sub

    Protected MustOverride Sub InitExecute()
    Protected MustOverride Sub DataProcess()
    Protected MustOverride Sub EndExecute()

    Protected Sub InboxManagement()
        Dim CurMsg As Message
        Dim CurNode As IdTag

        CurMsg = MyBox.RcvMsg()
        While (CurMsg IsNot Nothing)
            Select Case CurMsg.GetMsgType
                Case MsgStall       '"Delay"
                    StallThread()
                Case MsgResume     '"Resume"
                    OperatorStatus.Stalled = False      'Console.WriteLine("Item {0} has some work to do", Id)
                Case MsgTerminate   '"Terminate"
                    OperatorStatus.Finished = True
                Case MsgEndOfData   '"End_Of_Data"
                    OperatorStatus.LastMessage = True  'Neutral value on And operation
                    For I As Integer = 1 To ProducerList.Count
                        CurNode = ProducerList.Item(I)
                        If (CurNode.Id = CurMsg.GetMsgId) Then
                            CurNode.LastMessage = True
                        End If
                        OperatorStatus.LastMessage = OperatorStatus.LastMessage And CurNode.LastMessage
                    Next
                    Console.WriteLine("Item {0} Received EndOfData from {1} and its status is {2}", Id, CurMsg.GetMsgId, OperatorStatus.LastMessage)
                Case MsgStats   '"Production"
                    MonitorBox.SndMsg(New Message(Id, MsgStats, OutputCounter))
                Case MsgPing    '"Ping"
                    MonitorBox.SndMsg(New Message(Id, MsgPong, OutputCounter))
                Case MsgDummyResume
                    OperatorStatus.Stalled = False
                    DataProcess()
                    OperatorStatus.Stalled = True
            End Select
            CurMsg = MyBox.RcvMsg()
        End While
    End Sub

    Protected Sub StallThread()
        'Console.WriteLine("Item {0} is self-stalled now", Id)
        OperatorStatus.Stalled = True
        MonitorBox.SndMsg(New Message(Id, MsgStall))
    End Sub

    Protected Sub InitForward()
        Dim CurrentPack As RowPack

        For I As Integer = 1 To ConsumerList.Count
            CurrentPack = New RowPack
            PackCollection.Add(I, CurrentPack)
        Next
    End Sub

    Protected Function ForwardToConsumers(ByVal CurrentTuple As String) As Boolean
        Dim TempTuple As String, I As Integer
        Dim CurrentPack As RowPack
        Dim CurrentQueue As DataQueue
        Dim TotalStatus As Boolean
        TotalStatus = True  'Not sure that the packs will be sent
        'Console.WriteLine("Item {0} tries to send {1} *******************************", Id, CurrentTuple)
        For I = 1 To ConsumerList.Count
            TempTuple = CurrentTuple
            CurrentPack = PackCollection(I)
            If (Not CurrentPack.AddRow(TempTuple)) Then
                OutputCounter += 1
                CurrentQueue = Nothing
                CurrentQueue = ConsumerList(I).Queue
                TotalStatus = TotalStatus And CurrentQueue.PutData(CurrentPack)
                PackCollection.Remove(I)
                CurrentPack = New RowPack
                CurrentPack.AddRow(CurrentTuple)
                PackCollection.Add(I, CurrentPack)
            End If
            TempTuple = String.Empty
        Next
        Return TotalStatus
    End Function

    Protected Sub FinalForward()
        Dim CurrentPack As RowPack

        For I As Integer = 1 To ConsumerList.Count
            CurrentPack = PackCollection.Item(I)
            If (Not CurrentPack.IsEmpty) Then
                OutputCounter += 1
                ConsumerList.Item(I).Queue.PutData(CurrentPack)
            End If
        Next
    End Sub

    Protected Function UniqueFileName() As String
        UniqueFileName = "ID_" & Id & "_" & FileCounter
        FileCounter += 1
    End Function

    Protected Function UniqueFileName(ByVal Comment As String) As String
        UniqueFileName = "ID_" & Id & "_" & FileCounter & "_" & Comment
        FileCounter += 1
    End Function

    Public Function InputQueueSize() As Integer
        If (OnlyProducingData) Then
            InputQueueSize = 1
        Else
            For I As Integer = 1 To ProducerList.Count
                InputQueueSize = InputQueueSize + ProducerList.Item(I).Queue.QueueSize()
            Next
        End If
    End Function

    Protected Sub ResetCounters()
        InputCounter = 0
        OutputCounter = 0
        ExecutionTime = 1
    End Sub

    Public Function MemoryBenefit() As Double
        MemoryBenefit = InputQueueSize() * ((InputCounter - OutputCounter) / ExecutionTime)
    End Function

    Public Function MemoryBenefit(ByVal asa As Integer) As Double
        Console.Write("({0} - {1}) / {2}  ", InputCounter, OutputCounter, ExecutionTime)
        MemoryBenefit = InputQueueSize() * (InputCounter - OutputCounter) / ExecutionTime
    End Function
End Class

Public MustInherit Class ExecutionActivity
    Inherits ExecutionItem

        Protected ConstructActivity As Constructs.Activity

    Sub New(ByRef NewConstruct As Constructs.Activity)
        MyBase.New(NewConstruct.GetName())
        ConstructActivity = NewConstruct
    End Sub

    Function Construct() As Constructs.Activity
        Return ConstructActivity
    End Function

    Sub SetCTag(ByRef NewTag As IdTag, ByVal OutputName As String)
        Dim Position As Integer

        Position = ConstructActivity.OutputPosition(OutputName)
        'Console.WriteLine("{0}: Setting Ctag {1}->{2}", ConstructActivity.GetName, OutputName, Position)
        ConsumerList.Add(Position, NewTag)
    End Sub

    Sub SetPTag(ByRef NewTag As IdTag, ByVal InputName As String)
        Dim Position As Integer

        Position = ConstructActivity.InputPosition(InputName)
        'Console.WriteLine("{0}: Setting Ptag {1}->{2}", ConstructActivity.GetName, InputName, Position)
        ProducerList.Add(Position, NewTag)
    End Sub

    Function GetTag() As IdTag   'This function provides only the info that is inside the tag
        Dim NewTag As New IdTag(Id, ConstructActivity.GetName, Nothing, MyBox)
        Return NewTag
    End Function
End Class

Public MustInherit Class ExecutionRecordSet
    Inherits ExecutionItem

    Protected ConstructRSet As Constructs.RecordSet

    Sub New(ByRef NewConstruct As Constructs.RecordSet)
        MyBase.New(NewConstruct.GetName())
        ConstructRSet = NewConstruct
    End Sub

    Function Construct() As Constructs.RecordSet
        Return ConstructRSet
    End Function

    Sub SetCTag(ByRef NewTag As IdTag)
        ConsumerList.Add(ConsumerList.Count + 1, NewTag)
    End Sub

    Sub SetPTag(ByRef NewTag As IdTag)
        ProducerList.Add(ProducerList.Count + 1, NewTag)
    End Sub

    Function GetTag() As IdTag   'This function provides only the info that is inside the tag
        Dim NewTag As New IdTag(Id, ConstructRSet.GetName, Nothing, MyBox)
        Return NewTag
    End Function
End Class

Public Class IdTag
    Public ReadOnly Id As Integer
    Public ReadOnly Name As String
    Public ReadOnly Queue As DataQueue
    Public ReadOnly Box As Mailbox
    Public LastMessage, Stalled As Boolean

    Sub New(ByVal NewId As Integer, ByVal NewName As String, ByVal NewQueue As DataQueue, ByVal NewBox As Mailbox)
        Id = NewId
        Name = NewName
        Queue = NewQueue
        Box = NewBox
        LastMessage = False
        Stalled = True      'Only Scheduler can change this value
    End Sub
End Class

Public Class Status
    Public LastMessage, Finished, Stalled As Boolean

    Sub New()
        Finished = False
        Stalled = True
        LastMessage = False
    End Sub
End Class