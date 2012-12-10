Public Class Currency
    Inherits ExecutionActivity

    Private InPack As RowPack, Producer As IdTag, FieldPositionList As Integer()
    Private CurrentTuple, OutputTuple As String, ExchangeRate As Double

    Sub New(ByVal CurrentConstruct As Constructs.Activity)
        MyBase.New(CurrentConstruct)
    End Sub

    Protected Overrides Sub InitExecute()
        Dim FieldList As String(), InputSchema As Constructs.Schema

        InputSchema = ConstructActivity.InputSchema(1)
        FieldList = ConstructActivity.GetActivitySemantics().Split(";".ToCharArray)
        ExchangeRate = FieldList(0)
        FieldList = FieldList(1).Split(",".ToCharArray)
        ReDim FieldPositionList(FieldList.Length)
        For I As Integer = 0 To FieldList.Length - 1
            FieldPositionList(I) = InputSchema.GetAttributePosition(FieldList(I))
        Next
        Producer = ProducerList.Item(1)
        Console.WriteLine("Item {0} is Currency with input {1} and output {2}", Id, Producer.Queue.MyTag, ConsumerList.Item(1).Queue.MyTag)
    End Sub

    Protected Overrides Sub DataProcess()
        Dim Status As Boolean = True

        Producer.Queue.GetData(InPack)
        If (InPack Is Nothing) Then
            If (OperatorStatus.LastMessage) Then
                OperatorStatus.Finished = True
            Else
                StallThread()
            End If
        Else
            InputCounter += 1
            While (InPack.GetRow(CurrentTuple))
                OutputTuple = TransformTuple(CurrentTuple)
                Status = Status And ForwardToConsumers(OutputTuple)
            End While
            If (Not Status) Then
                StallThread()
            End If
        End If
    End Sub

    Protected Overrides Sub EndExecute()
        For I As Integer = 1 To ConsumerList.Count
            ConsumerList.Item(I).Box.SndMsg(New Message(Id, MsgEndOfData))
        Next
        MonitorBox.SndMsg(New Message(Id, MsgTerminate))
    End Sub

    Private Function TransformTuple(ByVal InputTuple As String) As String
        Dim InputDate As String, NewValue As Double

        TransformTuple = String.Empty
        For I As Integer = 0 To FieldPositionList.Length - 1
            InputDate = GetField(InputTuple, FieldPositionList(I))
            NewValue = InputDate * ExchangeRate
            TransformTuple = TransformTuple & TupleDelimiter & NewValue
        Next
        TransformTuple = InputTuple & TransformTuple
    End Function
End Class

