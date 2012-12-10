Public MustInherit Class ProxyReader
    Public MustOverride Sub ReadTuple(ByRef NewTuple As String)
    Public MustOverride Sub Destroy()
End Class

Public MustInherit Class ProxyWriter
    Public MustOverride Sub WriteTuple(ByVal NewTuple As String)
    Public MustOverride Sub Destroy()
End Class

Public Class FileReader
    Inherits ProxyReader

    Dim InputStream As System.IO.StreamReader
    Dim CurrentLine As String

    Sub New(ByVal CurrentPath As String)
        InputStream = New System.IO.StreamReader(CurrentPath)
    End Sub

    Public Overrides Sub ReadTuple(ByRef NewTuple As String)
        If (InputStream.Peek > 0) Then
            CurrentLine = InputStream.ReadLine()
            If (CurrentLine.EndsWith(TupleDelimiter)) Then
                NewTuple = CurrentLine.Substring(0, CurrentLine.Length - 1)
            Else
                If (CurrentLine = String.Empty) Then
                    NewTuple = Nothing
                Else
                    NewTuple = CurrentLine
                End If
            End If
        Else
            NewTuple = Nothing
        End If
    End Sub

    Public Overrides Sub Destroy()
        InputStream.Close()
    End Sub
End Class

Public Class FileWriter
    Inherits ProxyWriter

    Dim OutputStream As System.IO.StreamWriter

    Sub New(ByVal CurrentPath As String)
        OutputStream = New System.IO.StreamWriter(CurrentPath)
    End Sub

    Overrides Sub WriteTuple(ByVal NewTuple As String)
        OutputStream.WriteLine(NewTuple)
    End Sub

    Public Overrides Sub Destroy()
        OutputStream.Close()
    End Sub
End Class
