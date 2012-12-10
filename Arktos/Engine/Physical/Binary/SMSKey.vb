Public Class SMSKey
    Inherits ExecutionActivity

    Private ReceivingInput As Boolean, CurrentTuple, InnerFieldName, OuterFieldName, SKFieldName As String
    Private OuterProducer, InnerProducer As IdTag, OuterPack, InnerPack As RowPack
    Private OuterSorter, InnerSorter As VBSorter, InnerField, OuterField As String
    Private InnerFieldPosition, OuterFieldPosition, SKFieldPosition As Integer
    Private OuterSplit, InnerSplit As String(), delimStr_tuple As String = Arktos.TupleDelimiter
    Private delim_tuple As Char() = delimStr_tuple.ToCharArray
    Private CheckEqual, CheckBigger, CheckSmaller As TwoTupleEvaluator, SourceSelect As SingleTupleEvaluator
    Private InnerTuple, OuterTuple As String

    Sub New(ByRef NewConstruct As Constructs.Activity)
        MyBase.New(NewConstruct)
    End Sub

    Protected Overrides Sub InitExecute()
        Dim InnerArray As New ArrayList
        Dim OuterArray As New ArrayList
        ReceivingInput = True
        OuterProducer = ProducerList.Item(1)
        InnerProducer = ProducerList.Item(2)
        LoadSemantics()
        OuterArray.Add(OuterFieldPosition)
        OuterSorter = New VBSorter(ConstructActivity.InputSchema(1), OuterArray, UniqueFileName())
        InnerArray.Add(InnerFieldPosition)
        InnerSorter = New VBSorter(ConstructActivity.InputSchema(2), InnerArray, UniqueFileName())
        Console.WriteLine("Item {0} is SM-SÊey with input {1}, {2} and output {3}", Id, OuterProducer.Queue.MyTag, InnerProducer.Queue.MyTag, ConsumerList(1).Queue.MyTag)
    End Sub

    Protected Overrides Sub DataProcess()
        If (ReceivingInput) Then
            OuterProducer.Queue.GetData(OuterPack)
            InnerProducer.Queue.GetData(InnerPack)
            If (OuterPack Is Nothing And InnerPack Is Nothing) Then
                If (OperatorStatus.LastMessage) Then
                    OnlyProducingData = True
                    ReceivingInput = False
                    ResetCounters()
                    OuterSorter.Sort(Win32Platform)
                    InnerSorter.Sort(Win32Platform)
                    OuterTuple = OuterSorter.GetFromFile()
                    InnerTuple = InnerSorter.GetFromFile()
                    InnerField = GetField(InnerTuple, InnerFieldPosition)
                    OuterField = GetField(OuterTuple, OuterFieldPosition)
                Else
                    StallThread()
                End If
            Else
                If (OuterPack IsNot Nothing) Then
                    InputCounter += 1
                    While (OuterPack.GetRow(CurrentTuple))
                        OuterSorter.PutInFile(CurrentTuple)
                    End While
                End If
                If (InnerPack IsNot Nothing) Then
                    InputCounter += 1
                    While (InnerPack.GetRow(CurrentTuple))
                        If (SourceSelect.Evaluate(CurrentTuple)) Then
                            InnerSorter.PutInFile(CurrentTuple)
                        End If
                    End While
                End If
            End If
        Else
            ActualDataProcess()
        End If
    End Sub

    Protected Overrides Sub EndExecute()
        InnerSorter.Close()
        OuterSorter.Close()
        For I As Integer = 1 To ConsumerList.Count
            ConsumerList.Item(I).Box.SndMsg(New Message(Id, MsgEndOfData))
        Next
        MonitorBox.SndMsg(New Message(Id, MsgTerminate))
    End Sub

    Private Sub LoadSemantics()
        Dim MainSplit, JoinSplit As String()
        Dim CurrentSchema As Constructs.Schema

        MainSplit = ConstructActivity.GetActivitySemantics().Split(";".ToCharArray, 3)
        JoinSplit = MainSplit(0).Split(" ".ToCharArray, 3)
        OuterFieldName = JoinSplit(0)
        CurrentSchema = ConstructActivity.InputSchema(1)
        For I As Integer = 1 To CurrentSchema.AttributeCount()
            If (CurrentSchema.GetAttribute(I).GetName() = JoinSplit(0)) Then
                OuterFieldPosition = I
                Exit For
            End If
        Next
        InnerFieldName = JoinSplit(2)
        CurrentSchema = ConstructActivity.InputSchema(2)
        For I As Integer = 1 To CurrentSchema.AttributeCount()
            If (CurrentSchema.GetAttribute(I).GetName() = JoinSplit(2)) Then
                InnerFieldPosition = I
                Exit For
            End If
        Next
        CheckEqual = Get2TEvaluator("=", "string")
        If (CheckEqual Is Nothing) Then MsgBox(Id & " CheckEqual error!")
        CheckBigger = Get2TEvaluator(">", CurrentSchema.GetAttribute(InnerFieldPosition).GetAttributeType())
        If (CheckBigger Is Nothing) Then MsgBox(Id & " CheckBigger error!")
        CheckSmaller = Get2TEvaluator("<", CurrentSchema.GetAttribute(InnerFieldPosition).GetAttributeType())
        If (CheckSmaller Is Nothing) Then MsgBox(Id & " CheckSmaller error!")
        SourceSelect = GetSingleTupleEvaluator(CurrentSchema, MainSplit(1))
        For I As Integer = 1 To CurrentSchema.AttributeCount()
            If (CurrentSchema.GetAttribute(I).GetName() = MainSplit(2)) Then
                SKFieldName = MainSplit(2)
                SKFieldPosition = I
                Exit For
            End If
        Next

        Console.WriteLine("SMSKey: Outer: {0} at position {1}", OuterFieldName, OuterFieldPosition)
        Console.WriteLine("SMSKey: Inner: {0} at position {1}", InnerFieldName, InnerFieldPosition)
    End Sub

    Private Sub ActualDataProcess()
        Dim Status As Boolean = True

        For I As Integer = 1 To EnginePackSize
            If (OuterField = String.Empty Or InnerField = String.Empty) Then
                OperatorStatus.Finished = True
                Exit Sub
            End If
            If (CheckEqual.Evaluate(OuterField, InnerField)) Then
                Status = ForwardToConsumers(ProduceOutput())
                OuterTuple = OuterSorter.GetFromFile()
                OuterField = GetField(OuterTuple, OuterFieldPosition)
            ElseIf (CheckBigger.Evaluate(OuterField, InnerField)) Then
                InnerTuple = InnerSorter.GetFromFile()
                InnerField = GetField(InnerTuple, InnerFieldPosition)
            ElseIf (CheckSmaller.Evaluate(OuterField, InnerField)) Then
                OuterTuple = OuterSorter.GetFromFile()
                OuterField = GetField(OuterTuple, OuterFieldPosition)
            End If
            If (Not Status) Then Exit For
        Next
        If (Not Status) Then StallThread()
    End Sub

    Private Function ProduceOutput() As String
        ProduceOutput = OuterTuple & TupleDelimiter & GetField(InnerTuple, SKFieldPosition)
    End Function
End Class
