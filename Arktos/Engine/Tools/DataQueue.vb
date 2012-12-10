Public Class DataQueue
    Dim Size As Integer
    Dim SyncDtQ As Queue
    Public ReadOnly MyTag As Integer

    Sub New(ByVal NewSize As Integer)
        Dim DtQ As New Queue
        Size = NewSize
        SyncDtQ = Queue.Synchronized(DtQ)
        Randomize()
        MyTag = CInt(Int((100000 * Rnd()) + 1))
    End Sub

    Sub GetData(ByRef Row As RowPack)
        If (SyncDtQ.Count > 0) Then
            Row = SyncDtQ.Dequeue()
        Else
            Row = Nothing
        End If
    End Sub

    Function PutData(ByVal Row As RowPack) As Boolean
        SyncDtQ.Enqueue(Row)
        If (SyncDtQ.Count > Size) Then
            Return False
        End If
        If (SyncDtQ.Count < Size) Then
            Return True
        End If
    End Function

    Function QueueSize() As Integer
        Return SyncDtQ.Count
    End Function
End Class
