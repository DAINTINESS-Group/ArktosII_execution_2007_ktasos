Public Module Arktos
    Public Const ConstTimeSlot As Integer = 0
    Public Const ConstStallTime As Integer = 4
    Public Const ConstPackSize As Integer = 400
    Public Const ConstQueueSize As Integer = 100

    Public EngineTimeSlot As Integer = 16 'Milliseconds
    Public EngineProcessorCount As Integer = 1  'Default Value
    Public EngineStallTime As Integer = ConstStallTime
    Public EnginePackSize As Integer = ConstPackSize
    Public EngineQueueSize As Integer = ConstQueueSize
    Public EngineSchedPolicy As Integer = RoundRobinSchedPolicy
    Public EngineStartPath As String = String.Empty
    Public EngineUtilsPath As String = String.Empty
    Public EngineCachePath As String = String.Empty
    Public Const TupleDelimiter As String = "|"
    Public Const TupleDelimChar As Char = "|"
    Public Const ArktosNull As String = ""
    Public Const MaxTupleSize As Integer = 50

    Public Const MsgEndOfData As Integer = 0  '"EndOfData"
    Public Const MsgTerminate As Integer = 1  '"Done"
    Public Const MsgResume As Integer = 2     '"Resume"
    Public Const MsgStall As Integer = 3      '"Delay"
    Public Const MsgStats As Integer = 5      '"Production"
    Public Const MsgPing As Integer = 6       '"Ping"
    Public Const MsgPong As Integer = 7       '"Pong"
    Public Const MsgEmptyQueue As Integer = 8 '"EmptyQueue"
    Public Const MsgDummyResume As Integer = 9

    Public Const Win32Platform As Integer = 1
    Public Const DotNetPlatform As Integer = 2

    Public Const RoundRobinSchedPolicy As Integer = 1
    Public Const CostMinSchedPolicy As Integer = 2
    Public Const MemoryMinSchedPolicy As Integer = 3

    Function GetField(ByVal myString As String, ByVal FieldPosition As Integer) As String
        GetField = String.Empty
        Try
            Dim Counter, TSize, FStart, FEnd, I As Integer

            TSize = myString.Length
            FEnd = TSize
            Counter = 1
            For I = 0 To TSize - 1
                If (myString(I).Equals(TupleDelimChar)) Then '
                    If (Counter = FieldPosition) Then
                        FEnd = I
                        Exit For
                    Else
                        Counter += 1
                        FStart = I + 1
                    End If
                End If
            Next
            GetField = myString.Substring(FStart, FEnd - FStart)
        Catch ex As Exception
            'MsgBox("'" & myString & "'")
        End Try
    End Function

    Function GetField(ByVal myString As String, ByVal FieldPosition As Integer, ByVal DelimChar As String) As String
        GetField = String.Empty
        Try
            Dim Counter, TSize, FStart, FEnd, I As Integer

            TSize = myString.Length
            FEnd = TSize
            Counter = 1
            For I = 0 To TSize - 1
                If (myString(I).Equals(DelimChar)) Then '
                    If (Counter = FieldPosition) Then
                        FEnd = I
                        Exit For
                    Else
                        Counter += 1
                        FStart = I + 1
                    End If
                End If
            Next
            GetField = myString.Substring(FStart, FEnd - FStart)
        Catch ex As Exception
            'MsgBox("'" & myString & "'")
        End Try
    End Function

    Public Function GetSchedulingPolicy(ByVal SelectedPolicy As String) As Integer
        If (SelectedPolicy = "Round Robin (default)") Then
            Return 1
        End If
        If (SelectedPolicy = "Minimum Cost") Then
            Return 2
        End If
        If (SelectedPolicy = "Minimum Memory") Then
            Return 3
        End If
        Return 1
    End Function

    Public Sub ClearEngineCache()
        Try
            If (My.Computer.FileSystem.DirectoryExists(EngineStartPath & "\Data\Cache")) Then
                My.Computer.FileSystem.DeleteDirectory(EngineStartPath & "\Data\Cache", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                My.Computer.FileSystem.CreateDirectory(EngineStartPath & "\Data\Cache")
                Console.WriteLine("Cache cleared")
            Else
                MsgBox("Dir not found")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Module
