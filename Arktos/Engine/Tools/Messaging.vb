Public Class Mailbox
    Dim mQ As Queue

    Sub New()
        Dim DtQ As New Queue
        mQ = Queue.Synchronized(DtQ)
    End Sub

    Function RcvMsg() As Message
        RcvMsg = Nothing
        If (mQ.Count > 0) Then
            RcvMsg = mQ.Dequeue()
        End If
    End Function

    Sub SndMsg(ByVal CurrentMessage As Message)
        mQ.Enqueue(CurrentMessage)
    End Sub
End Class

Public Class Message
    Dim MsgId As Integer
    Dim MsgType As Integer
    Dim MsgInfo As String

    Sub New(ByVal Sender As Integer, ByVal Type As Integer)
        MsgId = Sender
        MsgType = Type
        MsgInfo = String.Empty
    End Sub

    Sub New(ByVal Sender As Integer, ByVal Type As Integer, ByVal Info As String)
        MsgId = Sender
        MsgType = Type
        MsgInfo = Info
    End Sub

    Function GetMsgType() As Integer
        Return MsgType
    End Function

    Function GetMsgId() As Integer
        Return MsgId
    End Function

    Function GetMsgInfo() As String
        Return MsgInfo
    End Function
End Class