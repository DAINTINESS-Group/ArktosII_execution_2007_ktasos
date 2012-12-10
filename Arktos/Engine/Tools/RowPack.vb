Public Class RowPackk
    Dim ArrayPack() As String
    Dim MaxSize, ReadPosition, WritePosition As Integer

    Sub New()
        MaxSize = EnginePackSize
        ReadPosition = 0
        WritePosition = 0
        ReDim ArrayPack(MaxSize)
    End Sub

    Function AddRow(ByVal NewRow As String) As Boolean
        If (WritePosition = MaxSize) Then
            Return False
        End If
        ArrayPack(WritePosition) = NewRow
        WritePosition += 1
        Return True
    End Function

    Function GetRow(ByRef NewRow As String) As Boolean
        If (ReadPosition = MaxSize) Then
            NewRow = String.Empty
            Return False
        End If
        NewRow = ArrayPack(ReadPosition)
        ReadPosition += 1
        Return True
    End Function

    Function GetRow(ByVal CurrentPosition As Integer) As String
        GetRow = ArrayPack(CurrentPosition - 1)
        'MsgBox(GetRow)
    End Function

    Sub Repeat()
        ReadPosition = 0
    End Sub

    Function IsEmpty() As Boolean
        If (WritePosition = 0) Then
            Return True
        End If
        Return False
    End Function
End Class

Public Class RowPack
    Dim Pack As ArrayList
    Dim MaxSize, ReadPosition, WritePosition As Integer

    Sub New()
        MaxSize = EnginePackSize
        ReadPosition = 0
        WritePosition = 0
        Pack = New ArrayList(MaxSize)
    End Sub

    Function AddRow(ByVal NewRow As String) As Boolean
        If (Pack.Count = MaxSize) Then
            Return False
        End If
        Pack.Add(NewRow)
        Return True
    End Function

    Function GetRow(ByRef NewRow As String) As Boolean
        If (ReadPosition = Pack.Count) Then
            NewRow = String.Empty
            Return False
        End If
        NewRow = Pack.Item(ReadPosition)
        ReadPosition += 1
        Return True
    End Function

    Function GetRow(ByVal CurrentPosition As Integer) As String
        GetRow = Pack.Item(CurrentPosition - 1)
    End Function

    Sub Repeat()
        ReadPosition = 0
    End Sub

    Function IsEmpty() As Boolean
        If (Pack.Count = 0) Then
            Return True
        End If
        Return False
    End Function
End Class
