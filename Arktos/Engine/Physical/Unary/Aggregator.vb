Public Class Aggregator
    Inherits ExecutionActivity

    Private InputSorter As VBSorter
    Private ReceivingInput As Boolean, Producer, Consumer As IdTag, InputPack As RowPack
    Private CurrentTuple, GroupField, AggregateField, CurrentGroup As String
    Private AggregateFieldPosition, AggregateResult As Integer
    Private delimStr_space As String = " "
    Private delimStr_tuple As String = Arktos.TupleDelimiter
    Private delim_space As Char() = delimStr_space.ToCharArray
    Private delim_tuple As Char() = delimStr_tuple.ToCharArray
    Private TupleSplit As String()
    Private AllAggregators, AllGroupFields As ArrayList

    Sub New(ByRef CurrentConstruct As Constructs.Activity)
        MyBase.New(CurrentConstruct)
        AllAggregators = New ArrayList
        AllGroupFields = New ArrayList
    End Sub

    Protected Overrides Sub InitExecute()
        ReceivingInput = True
        CurrentGroup = String.Empty
        Producer = ProducerList.Item(1)
        Consumer = ConsumerList.Item(1)

        LoadSemantics()
        Console.WriteLine("Item {0} is Aggregator-{3} with input {1} and output {2}", Id, Producer.Queue.MyTag, Consumer.Queue.MyTag, AggregateFieldPosition)
    End Sub

    Protected Overrides Sub DataProcess()
        Dim Status As Boolean = True
        Dim NewTuple As String

        If (ReceivingInput) Then
            Producer.Queue.GetData(InputPack)
            If (InputPack Is Nothing) Then
                If (Producer.LastMessage) Then
                    ReceivingInput = False
                    OnlyProducingData = True
                    ResetCounters()
                    InputSorter.Sort(Win32Platform) 'InputSorter.Sort(DotNetPlatform)
                Else
                    StallThread()
                End If
            Else
                InputCounter += 1
                While (InputPack.GetRow(CurrentTuple))
                    InputSorter.PutInFile(CurrentTuple)
                End While
            End If
        Else
            For I As Integer = 1 To EnginePackSize
                CurrentTuple = InputSorter.GetFromFile()
                If (CurrentGroup = String.Empty) Then
                    CurrentGroup = InputSorter.LastGroup
                End If
                If (CurrentTuple = String.Empty) Then
                    OperatorStatus.Finished = True
                    NewTuple = ResultTuple()
                    Status = ForwardToConsumers(NewTuple)
                    Exit For
                End If
                If (InputSorter.LastGroup <> CurrentGroup) Then
                    NewTuple = ResultTuple()
                    Status = ForwardToConsumers(NewTuple)   'New Group
                    CurrentGroup = InputSorter.LastGroup
                End If
                TupleSplit = CurrentTuple.Split(delim_tuple, 50)
                AggregateTuple()
                If (Not Status) Then Exit For
            Next
            If (Not Status) Then StallThread()
        End If
    End Sub

    Private Function ResultTuple() As String
        Dim CurrentAggregator As Aggregate

        ResultTuple = String.Empty
        For I As Integer = 0 To AllAggregators.Count - 1
            CurrentAggregator = AllAggregators.Item(I)
            ResultTuple = ResultTuple & CurrentAggregator.GetResult() & Arktos.TupleDelimiter
        Next
        For I As Integer = 1 To AllGroupFields.Count
            ResultTuple = ResultTuple & CurrentGroup.Substring(MaxTupleSize * (I - 1), MaxTupleSize).Trim() & TupleDelimiter
        Next
        ResultTuple = ResultTuple.Substring(0, ResultTuple.Length - 1)
    End Function

    Private Sub AggregateTuple()
        Dim CurrentAggregator As Aggregate

        For I As Integer = 0 To AllAggregators.Count - 1
            CurrentAggregator = AllAggregators.Item(I)
            CurrentAggregator.AddItem(TupleSplit(CurrentAggregator.Position - 1))
        Next
    End Sub

    Protected Overrides Sub EndExecute()
        InputSorter.Close()
        For I As Integer = 1 To ConsumerList.Count
            ConsumerList.Item(I).Box.SndMsg(New Message(Id, MsgEndOfData))
        Next
        MonitorBox.SndMsg(New Message(Id, MsgTerminate))
    End Sub

    Private Sub LoadSemantics()
        Dim SemanticsSplit, AggregateSplit, GroupSplit As String()
        Dim CurSchema As Constructs.Schema

        SemanticsSplit = Nothing
        AggregateSplit = Nothing
        GroupSplit = Nothing
        CurSchema = ConstructActivity.InputSchema(1)
        SemanticsSplit = ConstructActivity.GetActivitySemantics().Split(" ")
        'Console.WriteLine("Aggregate part: " & SemanticsSplit(0) & " Group part: " & SemanticsSplit(3))
        GroupField = SemanticsSplit(3)

        'Load All Group Fields Into The Array
        GroupSplit = GroupField.Split(",")
        For I As Integer = 0 To GroupSplit.Length - 1
            For J As Integer = 1 To CurSchema.AttributeCount
                If (CurSchema.GetAttribute(J).GetName() = GroupSplit(I)) Then
                    AllGroupFields.Add(J)
                    'Console.WriteLine("{2} Found '{0}' at position {1}", GroupSplit(I), J, Id)
                    Exit For
                End If
            Next
        Next
        'Console.WriteLine("Size is " & AllGroupFields.Count)

        'Load All Aggregators Into The Array
        AggregateSplit = SemanticsSplit(0).Split(",")
        For I As Integer = 1 To AggregateSplit.Length
            Dim CurrentAggregateSplit As String()
            Console.WriteLine(AggregateSplit(I - 1))
            CurrentAggregateSplit = AggregateSplit(I - 1).Split("(")
            AggregateField = CurrentAggregateSplit(1).Substring(0, CurrentAggregateSplit(1).Length - 1)
            For J As Integer = 1 To CurSchema.AttributeCount
                If (CurSchema.GetAttribute(J).GetName() = AggregateField) Then
                    AggregateFieldPosition = J
                    Exit For
                End If
            Next
            AddAggregator(CurrentAggregateSplit(0).ToLower, AggregateFieldPosition)
            'Console.WriteLine(CurrentAggregateSplit(0) & " on field " & AggregateField & " at position " & AggregateFieldPosition)
            'Console.WriteLine(GroupField)
        Next
        InputSorter = New VBSorter(CurSchema, AllGroupFields, UniqueFileName())
    End Sub

    Private Sub AddAggregator(ByVal AggregateFunction As String, ByVal AggregateFieldPosition As Integer)
        Dim CurrentAggregator As Aggregate = Nothing

        Select Case AggregateFunction.ToLower
            Case "count"
                CurrentAggregator = New AggregateCounter(AggregateFieldPosition)
            Case "avg"
                CurrentAggregator = New AggregateAvg(AggregateFieldPosition)
            Case "max"
                CurrentAggregator = New AggregateMax(AggregateFieldPosition)
            Case "min"
                CurrentAggregator = New AggregateMin(AggregateFieldPosition)
            Case "sum"
                CurrentAggregator = New AggregateSum(AggregateFieldPosition)
        End Select
        AllAggregators.Add(CurrentAggregator)
        Console.WriteLine("Size is {0}", AllAggregators.Count)
    End Sub
End Class
