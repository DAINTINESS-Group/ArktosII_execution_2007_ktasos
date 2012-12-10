Public Class CreateLookUp
    Inherits ExecutionActivity

    Dim InPack As RowPack, CurrentSplit As String()
    Dim Producer As IdTag, NumberOfSoucres, IdPosition, I As Integer
    Dim CurrentTuple, OutputTuple As String

    Sub New(ByVal CurrentConstruct As Constructs.Activity)
        MyBase.New(CurrentConstruct)
    End Sub

    Protected Overrides Sub InitExecute()
        InitForward()
        LoadSemantics()
        Producer = ProducerList.Item(1)
        Console.WriteLine("Item {0} is CreateLookUp with input {1} and output {2}", Id, Producer.Queue.MyTag, ConsumerList.Item(1).Queue.MyTag)
    End Sub

    Protected Overrides Sub DataProcess()
        Dim Status As Boolean = True

        Producer.Queue.GetData(InPack)
        If (InPack Is Nothing) Then
            If (OperatorStatus.LastMessage) Then
                OperatorStatus.Finished = True
                FinalForward()
            Else
                StallThread()
            End If
        Else
            While (InPack.GetRow(CurrentTuple))
                CurrentSplit = CurrentTuple.Split(TupleDelimiter.ToCharArray, 50)
                For I = 1 To NumberOfSoucres
                    OutputTuple = I.ToString & TupleDelimiter & CurrentSplit(IdPosition - 1) & TupleDelimiter & GetSK(I)
                    Status = Status And ForwardToConsumers(OutputTuple)
                Next
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

    Private Sub LoadSemantics()
        Dim MainSplit As String()

        MainSplit = ConstructActivity.GetActivitySemantics().Split(";".ToCharArray, 50)
        NumberOfSoucres = MainSplit(0)
        IdPosition = MainSplit(1)
    End Sub

    Private Function GetSK(ByVal CurrentSource As Integer) As String
        GetSK = (CurrentSplit(IdPosition - 1) * 10).ToString
    End Function
End Class
