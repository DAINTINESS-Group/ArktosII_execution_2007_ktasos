Public Class GenericActivity
    Inherits ExecutionActivity

    Private InPack, OutPack As RowPack, Producer As IdTag, CurrentTuple As String

    Sub New(ByVal CurrentConstruct As Constructs.Activity)
        MyBase.New(CurrentConstruct)
        OutPack = New RowPack
    End Sub

    Protected Overrides Sub InitExecute()
        Producer = ProducerList.Item(1)

        Console.WriteLine("Item {0} is Filter with input {1} and output {2}", Id, Producer.Queue.MyTag, ConsumerList.Item(1).Queue.MyTag)
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
                Status = Status And ForwardToConsumers(CurrentTuple)
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
End Class

