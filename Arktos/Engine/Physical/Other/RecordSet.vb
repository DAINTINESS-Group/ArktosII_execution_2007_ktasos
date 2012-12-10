Public Class Reader
    Inherits ExecutionRecordSet

    Private MyProxy As ProxyReader, CurrentTuple, Path As String, CurrentPack As RowPack

    Sub New(ByRef CurrentConstruct As Constructs.RecordSet)
        MyBase.New(CurrentConstruct)
        CurrentPack = New RowPack
        OnlyProducingData = True
        Path = String.Empty
    End Sub

    Protected Overrides Sub InitExecute()
        Path = ConstructRSet.GetRecordSetSemantics()
        If (Path.Contains(":")) Then
            MyProxy = New FileReader(Path)
        Else
            MyProxy = New FileReader(EngineStartPath + "\" + Path)
        End If
        Console.WriteLine("Item {0} is Reader with output {1}", Id, ConsumerList(1).Queue.MyTag)
    End Sub

    Protected Overrides Sub DataProcess()
        Dim Status As Boolean

        For I As Integer = 1 To EnginePackSize
            MyProxy.ReadTuple(CurrentTuple)
            If (CurrentTuple Is Nothing) Then   'This happens when we reach the end of this file
                OperatorStatus.Finished = True
                Exit For
            Else
                Status = ForwardToConsumers(CurrentTuple)
                If (Not Status) Then
                    StallThread()
                    Exit For
                End If
            End If
        Next
    End Sub

    Protected Overrides Sub EndExecute()
        MyProxy.Destroy()
        For I As Integer = 1 To ConsumerList.Count
            ConsumerList.Item(I).Box.SndMsg(New Message(Id, MsgEndOfData))
        Next
        MonitorBox.SndMsg(New Message(Id, MsgTerminate))
    End Sub
End Class

Public Class Writer
    Inherits ExecutionRecordSet

    Private MyProxy As ProxyWriter, CurrentTuple, Path As String, CurrentPack As RowPack

    Sub New(ByRef CurrentConstruct As Constructs.RecordSet)
        MyBase.New(CurrentConstruct)
        CurrentPack = New RowPack
        Path = String.Empty
    End Sub

    Protected Overrides Sub InitExecute()
        Path = ConstructRSet.GetRecordSetSemantics
        If (Path.Contains(":")) Then
            MyProxy = New FileWriter(Path)
        Else
            MyProxy = New FileWriter(EngineStartPath + "\" + Path)
        End If
        Console.WriteLine("Item {0} is Writer with input {1}", Id, ProducerList(1).Queue.MyTag)
    End Sub

    Protected Overrides Sub DataProcess()
        ProducerList(1).Queue.GetData(CurrentPack)
        If (CurrentPack Is Nothing) Then
            If (OperatorStatus.LastMessage) Then
                OperatorStatus.Finished = True
            Else
                StallThread()
            End If
        Else
            InputCounter += 1
            While (CurrentPack.GetRow(CurrentTuple))
                MyProxy.WriteTuple(CurrentTuple)
            End While
        End If
    End Sub

    Protected Overrides Sub EndExecute()
        MyProxy.Destroy()
        MonitorBox.SndMsg(New Message(Id, MsgTerminate))
    End Sub
End Class
