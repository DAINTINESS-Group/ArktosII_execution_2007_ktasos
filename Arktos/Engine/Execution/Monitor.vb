Imports Constructs

Public Class Monitor
    Private SortedScn As Collection, AllItems, AllBoxes As SortedList, Scheduler As ArktosScheduler
    Private ScnOpt As Optimizer, ReadyToRise As Boolean, Scn As Scenario, StartStatsCounter As Integer
    Private MonitorBox As Mailbox, pThread As Thread, Id As Integer, t1, t2 As TimeSpan
    Private MonitorClock, StatsClock As HiPerfTimer, WithTimeSlot, ActStalledEndOfTS As Boolean

    Public ExecutionTime As Double, MemLoad, MaxLoad As Long, StatsCounter As Integer

    Sub New()
        MonitorBox = New Mailbox
        AllItems = New SortedList
        AllBoxes = New SortedList
        ReadyToRise = False : ActStalledEndOfTS = True
        MonitorClock = New HiPerfTimer
        StatsClock = New HiPerfTimer
        Id = 0  'Monitor Id
        MemLoad = 0
        StatsCounter = 0
        StartStatsCounter = 0
    End Sub

    Sub SetParameters(ByVal NewTimeSlot As Integer, ByVal NewStallTime As Integer, ByVal NewPackSize As Integer, ByVal NewQueueSize As Integer, ByVal NewPath As String, ByVal SchedulingPolicy As Integer, ByVal ProcessorCount As Integer)
        EngineTimeSlot = NewTimeSlot
        EngineProcessorCount = ProcessorCount
        EngineStallTime = NewStallTime
        EnginePackSize = NewPackSize
        EngineQueueSize = NewQueueSize
        EngineSchedPolicy = SchedulingPolicy
        EngineStartPath = NewPath
        EngineUtilsPath = NewPath & "\Utilities"
        EngineCachePath = NewPath & "\Data\Cache"
        Console.WriteLine("TimeSlot: {0}, StallTime: {1}, RPS: {2}, DQS: {3}", EngineTimeSlot, EngineStallTime, EnginePackSize, EngineQueueSize)
        If (EngineTimeSlot > 0) Then
            WithTimeSlot = True
            Console.WriteLine("Time Slots enabled!")
        Else
            Console.WriteLine("Time Slots disabled!")
            WithTimeSlot = False
        End If
        ReadyToRise = True
    End Sub

    Sub Rise(ByVal ExecScn As Scenario)
        If (ReadyToRise) Then
            Scn = ExecScn
            ScnOpt = New Optimizer(Scn)
            SortedScn = ScnOpt.Optimize()
            If (ScnOpt.Errors()) Then
                Exit Sub
            End If
            For I As Integer = 1 To SortedScn.Count  'Check Topological Sorting...
                Console.Write("Name is: {0} {1} " & vbTab, SortedScn.Item(I).GetName, SortedScn.Item(I).GetId)
            Next
            Console.WriteLine()

            pThread = New Thread(AddressOf RunThread)
            pThread.Start()
            pThread.Join()
        Else
            Throw New Exception("No parameters are set for the Arktos monitor")
        End If
    End Sub

    Private Sub RunThread()
        Console.WriteLine("Monitor Rising... ")
        InitializeScenario()
        RiseScenario()
        Monitoring()
        t2 = DateTime.Now.TimeOfDay
        ExecutionTime = t2.Subtract(t1).TotalSeconds
        Console.WriteLine("{0} -::- {1}", t1, t2)

        Console.WriteLine("The total time was {0} sec", ExecutionTime)
        Console.WriteLine("The max memory was {0} row paks", MaxLoad)
        Console.WriteLine("The average memory was {0} row packs Monitor Terminating...", MemLoad / StatsCounter)
        Close()
    End Sub

    Private Sub InitializeScenario()
        Dim SScnCount As Integer
        Dim CurEdge As Constructs.Edge
        Dim CurStart, CurEnd, CurPhysObj As Object
        Dim CurDQueue As DataQueue
        Dim StartTag, EndTag As IdTag

        SScnCount = SortedScn.Count
        'Assigning Monitor Mailbox to nodes
        For I As Integer = 1 To SScnCount
            SortedScn(I).SetMonitorBox(MonitorBox)
            AllBoxes.Add(SortedScn(I).GetId(), SortedScn(I).GetBox())
            AllItems.Add(SortedScn(I).GetId(), SortedScn(I))
        Next

        'Connecting the nodes of the scenario by giving the mailboxes and dataqueues
        For I As Integer = 1 To Scn.EdgeCount
            CurEdge = Scn.GetEdge(I)
            CurStart = Nothing
            CurEnd = Nothing
            For J As Integer = 1 To SortedScn.Count 'Physical Items are sorted only by ID
                If ((CurStart IsNot Nothing) And (CurEnd IsNot Nothing)) Then
                    Exit For
                End If

                CurPhysObj = SortedScn.Item(J)
                If (CurEdge.GetStart.GetName = CurPhysObj.Construct().GetName) Then
                    CurStart = CurPhysObj
                End If
                If (CurEdge.GetEnd.GetName = CurPhysObj.Construct().GetName) Then
                    CurEnd = CurPhysObj
                End If
            Next
            'Console.Write(CurEdge.GetName())
            'Console.WriteLine(" from '{0}' To '{1}'", CurEdge.GetStart.GetName, CurEdge.GetEnd.GetName)
            'Console.WriteLine("{0} :: Tags for '{1}' and '{2}'", CurEdge.GetName, CurEdge.GetStartOutput, CurEdge.GetEndInput)
            'Assining IdTags to all neighbors
            StartTag = CurStart.GetTag()
            EndTag = CurEnd.GetTag()
            CurDQueue = New DataQueue(EngineQueueSize)
            StartTag = New IdTag(StartTag.Id, StartTag.Name, CurDQueue, StartTag.Box)
            EndTag = New IdTag(EndTag.Id, EndTag.Name, CurDQueue, EndTag.Box)

            If (CurEdge.GetStartOutput = String.Empty) Then       'Start node is RecordSet
                'Console.WriteLine("Start Edge! 0 - {0}", CurEdge.GetEndInput)
                CurStart.SetCTag(EndTag)
                CurEnd.SetPTag(StartTag, CurEdge.GetEndInput)
                'Console.WriteLine("Start Edge!")
            Else
                If (CurEdge.GetEndInput = String.Empty) Then       'End node is RecordSet
                    'Console.WriteLine("End Edge! {0} - 0", CurEdge.GetStartOutput)
                    CurStart.SetCTag(EndTag, CurEdge.GetStartOutput)
                    CurEnd.SetPTag(StartTag)
                    'Console.WriteLine("End Edge!")
                Else
                    'Console.WriteLine("Middle Edge! {0} - {1}", CurEdge.GetStartOutput, CurEdge.GetEndInput)
                    CurStart.SetCTag(EndTag, CurEdge.GetStartOutput)
                    CurEnd.SetPTag(StartTag, CurEdge.GetEndInput)
                    'Console.WriteLine("Middle Edge!")
                End If
            End If
            'Console.WriteLine("OK")
            'Console.ReadLine()
        Next
    End Sub

    Private Sub RiseScenario()
        Dim SScnCount As Integer

        SScnCount = SortedScn.Count
        Console.WriteLine("All nodes ({0}) start running...", SScnCount)
        For I As Integer = 1 To SScnCount
            SortedScn(SScnCount - I + 1).OperatorThread.Start()
        Next
        Select Case EngineSchedPolicy
            Case RoundRobinSchedPolicy
                Scheduler = New RoundRobinScheduler(SortedScn)
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~RR Sched")
            Case CostMinSchedPolicy
                Scheduler = New MinCost(SortedScn)
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~MC Sched")
            Case MemoryMinSchedPolicy
                Scheduler = New MinMemory(SortedScn)
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~MM Sched")
            Case Else
                Scheduler = New RoundRobinScheduler(SortedScn)
                Console.WriteLine("Scheduling Policy not found!!! Selecting RR as default!!!")
        End Select
    End Sub

    Private Sub Monitoring()
        Dim FinishedItems, TotalItems, MonitorStallTime, CurrentId As Integer
        Dim Done As Boolean, CurMsg As Message, CurrentBox As Mailbox

        Console.WriteLine("{1} -- {0}", EngineStallTime, EngineTimeSlot)
        Done = False
        MonitorStallTime = EngineStallTime
        TotalItems = SortedScn.Count
        FinishedItems = 0
        Thread.Sleep(500)
        t1 = DateTime.Now.TimeOfDay
        StatsClock.TimerStart()
        CurrentId = GetNextActivity()
        CurrentBox = AllBoxes.Item(CurrentId)
        If (WithTimeSlot) Then ActStalledEndOfTS = True : MonitorClock.TimerStart()
        CurrentBox.SndMsg(New Message(Id, MsgResume))   'Console.WriteLine("Waking up Item {0}", CurrentId)
        While (Not Done)
            If (WithTimeSlot) Then
                MonitorClock.TimerStop()
                If (ActStalledEndOfTS And MonitorClock.Duration() > EngineTimeSlot) Then
                    CurrentBox = AllBoxes.Item(CurrentId)
                    CurrentBox.SndMsg(New Message(Id, MsgStall))
                    'Console.WriteLine("Time slot exceeded " & MonitorClock.Duration() & " " & ActStalledEndOfTS)
                    ActStalledEndOfTS = False
                End If
            End If
            CollectStats()
            CurMsg = MonitorBox.RcvMsg()
            If (CurMsg Is Nothing) Then
                Thread.Sleep(MonitorStallTime)
            Else
                Select Case CurMsg.GetMsgType
                    Case MsgTerminate
                        FinishedItems += 1
                        Scheduler.RemoveFromList(CurMsg.GetMsgId)           'Console.WriteLine("Monitor: Item {0} is done. Finished Items: {1}", CurMsg.GetMsgId, FinishedItems)
                        If (FinishedItems = TotalItems) Then
                            Done = True
                        Else
                            CurrentId = GetNextActivity()                   'Console.WriteLine("Item {0} did some work. Item {1} is next!", CurMsg.GetMsgId, CurrentId)
                            CurrentBox = AllBoxes.Item(CurrentId)
                            'If (ActStalledEndOfTS) Then Console.WriteLine("Other interupt")
                            If (WithTimeSlot) Then ActStalledEndOfTS = True : MonitorClock.TimerStart()
                            CurrentBox.SndMsg(New Message(Id, MsgResume))   'Console.WriteLine("Waking up Item {0}", CurrentId)
                        End If
                    Case MsgStall
                        CurrentId = GetNextActivity()                       'Console.WriteLine("Item {0} did some work. Item {1} is next!", CurMsg.GetMsgId, CurrentId)
                        CurrentBox = AllBoxes.Item(CurrentId)
                        'If (ActStalledEndOfTS) Then Console.WriteLine("Other interupt")
                        If (WithTimeSlot) Then ActStalledEndOfTS = True : MonitorClock.TimerStart()
                        CurrentBox.SndMsg(New Message(Id, MsgResume))       'Console.WriteLine("Waking up Item {0}", CurrentId)
                    Case MsgPong
                        Console.WriteLine("Monitor: Item {0}( is alive {1} with status: {2}", CurMsg.GetMsgId, FinishedItems, CurMsg.GetMsgInfo)
                End Select
            End If
        End While

    End Sub

    Private Sub Close()
        SortedScn = Nothing
        AllBoxes = Nothing
        Scheduler = Nothing
        ScnOpt = Nothing
        ReadyToRise = Nothing
        Scn = Nothing
        MonitorBox = Nothing
        pThread = Nothing
    End Sub

    Private Function GetNextActivity() As Integer
        Dim CurrentBox As Mailbox, CurrentActivity

        CurrentActivity = Scheduler.NextActivity()
        While (CurrentActivity = 0)
            Console.WriteLine(vbNewLine & vbNewLine & "Waking Up everybody...." & vbNewLine & vbNewLine)
            For I As Integer = 0 To Scheduler.SchedulerList.Count() - 1
                CurrentBox = AllBoxes.Item(Scheduler.SchedulerList.GetByIndex(I))
                CurrentBox.SndMsg(New Message(Id, MsgDummyResume))
            Next
            Thread.Sleep(15)
            CurrentActivity = Scheduler.NextActivity()
        End While
        Return CurrentActivity
    End Function

    Private Sub CollectStats()
        Dim CurrentItem As ExecutionItem, CurrentLoad As Integer

        StatsClock.TimerStop()
        If (StatsClock.Duration > 100) Then
            If (StartStatsCounter > 10) Then
                CurrentLoad = 0
                For I As Integer = 0 To AllItems.Count - 1
                    CurrentItem = AllItems.GetByIndex(I)
                    CurrentLoad += CurrentItem.InputQueueSize()
                Next
                If (CurrentLoad > MaxLoad) Then
                    MaxLoad = CurrentLoad
                    For I As Integer = 0 To AllItems.Count - 1
                        CurrentItem = AllItems.GetByIndex(I)
                        'Console.Write(CurrentItem.InputQueueSize())
                        'Console.Write(" ")
                    Next
                    'Console.WriteLine()
                    'Console.WriteLine(MaxLoad)
                End If
                MemLoad += CurrentLoad
                StatsCounter += 1
            Else
                StartStatsCounter += 1
            End If
            StatsClock.TimerStart()
        End If
    End Sub
End Class
