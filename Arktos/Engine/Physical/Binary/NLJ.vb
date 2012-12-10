Public Class NLJ
    Inherits ExecutionActivity

    Private InnerPack, OuterPack As RowPack, OuterLoop, InnerLoop, Consumer As IdTag
    Private InnerTuple, OuterTuple, OutputTuple, TempFilePath As String
    Private ControlOperator, InnerFieldType, OuterFieldType As String
    Private ReadCache As System.IO.StreamReader, WriteCache As System.IO.StreamWriter
    Private OuterField, InnerField, SurrogateField As Integer
    Private ReceivingInput, MatchMany, MatchOne, MatchNone As Boolean
    Private delimStr_space As String = " "
    Private delimStr_tuple As String = Arktos.TupleDelimiter
    Private delim_space As Char() = delimStr_space.ToCharArray
    Private delim_tuple As Char() = delimStr_tuple.ToCharArray
    Private OuterSplit, InnerSplit As String()
    Private NLJConditionEvaluator As TwoTupleEvaluator

    Sub New(ByRef CurrentConstruct As Constructs.Activity)
        MyBase.New(CurrentConstruct)
        MatchMany = False
        MatchOne = False
        MatchNone = False
    End Sub

    Protected Overrides Sub InitExecute()
        Dim OuterSchema, InnerSchema As Constructs.Schema
        Dim SemanticsSplit As String() = Nothing

        ReceivingInput = True
        OuterSplit = Nothing
        InnerSplit = Nothing
        delim_space = delimStr_space.ToCharArray
        delim_tuple = delimStr_tuple.ToCharArray

        OuterLoop = ProducerList.Item(1)
        InnerLoop = ProducerList.Item(2)
        Consumer = ConsumerList.Item(1)

        OuterSchema = ConstructActivity.InputSchema(1)
        InnerSchema = ConstructActivity.InputSchema(2)

        If (ConstructActivity.GetActivityType = "Join") Then
            MatchMany = True
        ElseIf (ConstructActivity.GetActivityType = "Diff") Then
            MatchNone = True
        ElseIf (ConstructActivity.GetActivityType = "SurrogateKey") Then
            MatchOne = True
            For I As Integer = 1 To InnerSchema.AttributeCount()
                If (InnerSchema.GetAttribute(I).GetName() = "SK") Then
                    SurrogateField = I
                    Exit For
                End If
            Next
        End If
        SemanticsSplit = ConstructActivity.GetActivitySemantics().Split(delim_space, 50)
        For I As Integer = 1 To OuterSchema.AttributeCount()
            If (OuterSchema.GetAttribute(I).GetName() = SemanticsSplit(0)) Then
                OuterField = I
                OuterFieldType = OuterSchema.GetAttribute(I).GetAttributeType()
                Exit For
            End If
        Next
        ControlOperator = SemanticsSplit(1)
        For I As Integer = 1 To InnerSchema.AttributeCount()
            If (InnerSchema.GetAttribute(I).GetName() = SemanticsSplit(2)) Then
                InnerField = I
                InnerFieldType = InnerSchema.GetAttribute(I).GetAttributeType()
                Exit For
            End If
        Next
        LoadEvaluator()

        TempFilePath = EngineStartPath & "\Meta\CacheData_" & ConstructActivity.GetName()
        WriteCache = New System.IO.StreamWriter(TempFilePath)

        Console.WriteLine("Item {0} is NLJ with inputs {1} {2} and output {3}", Id, OuterLoop.Queue.MyTag, InnerLoop.Queue.MyTag, Consumer.Queue.MyTag)
        Console.WriteLine("{0} {1} {2} {3} {4} {5}", MatchMany, MatchOne, MatchNone, InnerField, OuterField, ControlOperator)
    End Sub

    Protected Overrides Sub DataProcess()
        If (ReceivingInput) Then
            InnerLoop.Queue.GetData(InnerPack)
            If (InnerPack Is Nothing) Then
                If (InnerLoop.LastMessage) Then
                    WriteCache.Close()
                    ReadCache = New System.IO.StreamReader(TempFilePath)
                    ReceivingInput = False
                    ResetCounters()
                Else
                    StallThread()
                End If
            Else
                InputCounter += 1
                While (InnerPack.GetRow(InnerTuple))
                    WriteCache.WriteLine(InnerTuple)
                End While
            End If
        Else
            OuterLoop.Queue.GetData(OuterPack)
            If (OuterPack Is Nothing) Then
                If (OperatorStatus.LastMessage) Then
                    OperatorStatus.Finished = True
                Else
                    StallThread()
                End If
            Else
                ActualJoin()
            End If
        End If
    End Sub

    Private Sub ActualJoin()
        Dim TupleStatus(EnginePackSize) As Boolean  'Default Value is False
        Dim JoinField(EnginePackSize), InnerJoin As String
        Dim Position As Integer = 0
        Dim Status As Boolean

        InputCounter += 1
        While (OuterPack.GetRow(OuterTuple))
            OuterSplit = OuterTuple.Split(delim_tuple, 50)
            JoinField(Position) = OuterSplit(OuterField - 1)
            Position += 1
        End While

        While (GetFromCache(InnerTuple))    'Not reached EOF
            Position = 0
            InnerSplit = InnerTuple.Split(delim_tuple, 50)
            InnerJoin = InnerSplit(InnerField - 1)
            While (OuterPack.GetRow(OuterTuple))
                'Console.WriteLine(OuterTuple)
                If (((Not TupleStatus(Position)) And MatchOne) Or ((Not TupleStatus(Position)) And MatchNone) Or MatchMany) Then
                    If (MatchTuple(JoinField(Position), InnerJoin)) Then
                        If (Not MatchNone) Then
                            OutputTuple = ProduceOutput(InnerTuple, OuterTuple)
                            Status = ForwardToConsumers(OutputTuple)
                        End If
                        TupleStatus(Position) = True
                    End If
                End If
                Position += 1
            End While
            OuterPack.Repeat()
        End While
        'Checking For Rejected Tuples
        If (MatchNone) Then
            OuterPack.Repeat()
            Position = 0
            While (OuterPack.GetRow(OuterTuple))
                If (Not TupleStatus(Position)) Then
                    Status = ForwardToConsumers(OuterTuple)
                End If
                Position += 1
            End While
        End If
        If (Not Status) Then
            StallThread()
        End If
    End Sub

    Protected Overrides Sub EndExecute()
        ReadCache.Close()
        System.IO.File.Delete(TempFilePath)
        For I As Integer = 1 To ConsumerList.Count
            ConsumerList.Item(I).Box.SndMsg(New Message(Id, MsgEndOfData))
        Next
        MonitorBox.SndMsg(New Message(Id, MsgTerminate))
    End Sub

    Private Function GetFromCache(ByRef CurrentLine As String) As Boolean
        CurrentLine = ReadCache.ReadLine()
        If (CurrentLine = String.Empty) Then
            ResetCache()
            Return False
        End If
        Return True
    End Function

    Private Sub ResetCache()
        ReadCache.BaseStream.Seek(0, IO.SeekOrigin.Begin)
    End Sub

    Private Function MatchTuple(ByVal OuterTuple As String, ByVal InnerTuple As String) As Boolean
        MatchTuple = False
        OutputTuple = String.Empty
        If (NLJConditionEvaluator.Evaluate(OuterTuple, InnerTuple)) Then
            MatchTuple = True
        End If
    End Function

    Private Function ProduceOutput(ByVal InnerTuple As String, ByVal OuterTuple As String) As String
        ProduceOutput = String.Empty
        Select Case ConstructActivity.GetActivityType
            Case "Join"
                If (InnerTuple.Replace(InnerSplit(InnerField - 1) & TupleDelimiter, String.Empty) = InnerTuple) Then
                    ProduceOutput = OuterTuple & TupleDelimiter & InnerTuple.Replace(TupleDelimiter & InnerSplit(InnerField - 1), String.Empty)
                Else
                    ProduceOutput = OuterTuple & TupleDelimiter & InnerTuple.Replace(InnerSplit(InnerField - 1) & TupleDelimiter, String.Empty)
                End If
            Case "SurrogateKey"
                ProduceOutput = OuterTuple & TupleDelimiter & InnerSplit(SurrogateField - 1)
            Case "Diff"
                ProduceOutput = OuterTuple
        End Select
    End Function

    Private Sub LoadEvaluator()
        If (InnerFieldType <> OuterFieldType) Then
            Console.WriteLine("NLJ: Type conflict in semantics condition")
        Else
            Console.WriteLine("NLJ: No type conflict in semantics condition " & InnerFieldType)
            If (ControlOperator = "=") Then
                NLJConditionEvaluator = New StringEqualController()
            Else
                NLJConditionEvaluator = Get2TEvaluator(InnerFieldType.ToLower, ControlOperator)
            End If
        End If
    End Sub
End Class
