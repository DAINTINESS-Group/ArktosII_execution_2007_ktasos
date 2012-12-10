Public Class Filter
    Inherits ExecutionActivity

    Private InPack As RowPack, Producer As IdTag, CurrentTuple As String
    Private FilterCalculator As SingleTupleEvaluator, NotNulField() As Integer

    Sub New(ByVal CurrentConstruct As Constructs.Activity)
        MyBase.New(CurrentConstruct)
    End Sub

    Protected Overrides Sub InitExecute()
        Producer = ProducerList.Item(1)
        InitializeCalculator()
        If (FilterCalculator Is Nothing) Then
            Console.WriteLine("Error Initializing filter ({0}) calculator!!", Id)
        Else
            Console.WriteLine("ALL OK Initializing filter ({0}) calculator!!", Id)
        End If
        Console.WriteLine("Item {0} is {3} with input {1} and output {2}", Id, Producer.Queue.MyTag, ConsumerList.Item(1).Queue.MyTag, ConstructActivity.GetName())
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
                If (FilterCalculator.Evaluate(CurrentTuple)) Then
                    Status = Status And ForwardToConsumers(CurrentTuple)
                End If
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

    Private Sub InitializeCalculator()
        Dim MySemantics As String
        Dim InputSchema As Constructs.Schema
        Dim split As String() = Nothing

        MySemantics = ConstructActivity.GetActivitySemantics()
        InputSchema = ConstructActivity.InputSchema(1)

        If (ConstructActivity.GetActivityType() = "NotNull") Then
            NotNullCalculator()
        Else
            FilterCalculator = GetSingleTupleEvaluator(InputSchema, MySemantics)
        End If
    End Sub

    Private Sub NotNullCalculator()
        Dim InputSchema As Constructs.Schema = ConstructActivity.InputSchema(1)
        Dim NotNullField As String() = Nothing
        Dim delimSem As String = ","
        Dim delim As Char() = delimSem.ToCharArray
        NotNullField = ConstructActivity.GetActivitySemantics().Split(delim, 50)

        Dim Position(NotNullField.Length - 1) As Integer
        For J As Integer = 0 To Position.Length - 1
            For I As Integer = 1 To InputSchema.AttributeCount
                If (InputSchema.GetAttribute(I).GetName() = NotNullField.GetValue(J)) Then
                    Console.WriteLine("NotNull: Field {0} at position {1}", NotNullField.GetValue(J), I)
                    Position(J) = I
                End If
            Next I
        Next J
        FilterCalculator = New NotNullCheck(Position)
    End Sub
End Class

