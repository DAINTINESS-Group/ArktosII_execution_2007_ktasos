Public Class LIDeriveFunc
    Inherits ExecutionActivity

    Dim InPack As RowPack, Producer As IdTag, CurrentTuple, OutputTuple As String

    Sub New(ByVal CurrentConstruct As Constructs.Activity)
        MyBase.New(CurrentConstruct)
    End Sub

    Protected Overrides Sub InitExecute()
        Producer = ProducerList.Item(1)
        Console.WriteLine("Item {0} is Function with input {1} and output {2}", Id, Producer.Queue.MyTag, ConsumerList.Item(1).Queue.MyTag)
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
        Dim ExPrice, Discount, Tax As Double

        ExPrice = GetField(InputTuple, 6)
        Discount = GetField(InputTuple, 7)
        Tax = GetField(InputTuple, 8)
        TransformTuple = InputTuple & TupleDelimiter & (ExPrice - Discount - Tax).ToString
    End Function
End Class

