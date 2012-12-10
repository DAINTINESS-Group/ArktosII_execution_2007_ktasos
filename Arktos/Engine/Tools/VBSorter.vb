Public Class VBSorter
    Public LastGroup As String
    Const RunFileCapacity As Integer = 1000000

    Dim InputData As System.IO.StreamWriter, SortedData As System.IO.StreamReader
    Dim LineCounter, RunCounter As Integer, InputFileList As New ArrayList
    Dim Padding, RunFilePath, SortedFilePath, BatFilePath As String
    Dim DataSchema As Constructs.Schema, GroupFields As ArrayList, SorterLastGroup As String
    Dim Tuple As String()
    Dim delim_tuple As Char() = Arktos.TupleDelimiter.ToCharArray

    Sub New(ByVal InputSchema As Constructs.Schema, ByVal PositionGroupFields As ArrayList, ByVal FileName As String)
        LineCounter = 0
        RunCounter = 1
        DataSchema = InputSchema
        GroupFields = PositionGroupFields
        BatFilePath = EngineCachePath & "\" & FileName & ".bat"
        RunFilePath = EngineCachePath & "\" & FileName
        SortedFilePath = EngineCachePath & "\" & FileName & ".out"
        CreateNewRun()
        For I As Integer = 1 To MaxTupleSize
            Padding = Padding & " "
        Next
    End Sub

    Sub PutInFile(ByVal CurrentLine As String)
        Dim CurrentAttribute As Constructs.Attribute
        Dim CurrentField As String

        Tuple = CurrentLine.Split(delim_tuple, 50)
        For I As Integer = 0 To GroupFields.Count - 1
            CurrentAttribute = DataSchema.GetAttribute(GroupFields.Item(I))
            CurrentField = Tuple(GroupFields.Item(I) - 1)
            If (CurrentAttribute.GetAttributeType = "string") Then
                InputData.Write(CurrentField & Padding.Substring(0, MaxTupleSize - CurrentField.Length))
            Else
                InputData.Write(Padding.Substring(0, MaxTupleSize - CurrentField.Length) & CurrentField)
            End If
        Next
        InputData.WriteLine(TupleDelimiter & CurrentLine)
        LineCounter += 1
        If (LineCounter = RunFileCapacity) Then
            CreateNewRun()
            LineCounter = 0
        End If
    End Sub

    Sub Sort(ByVal Platform As Integer)
        CloseRun()
        CygwinSort()
        SortedData = New System.IO.StreamReader(SortedFilePath)
    End Sub

    Function GetFromFile() As String
        Tuple = Nothing
        GetFromFile = String.Empty
        If (SortedData.Peek > 0) Then
            Tuple = SortedData.ReadLine().Split(delim_tuple, 2)
            LastGroup = Tuple(0)
            GetFromFile = Tuple(1)
        Else
            Return String.Empty
        End If
    End Function

    Private Sub CreateNewRun()
        CloseRun()
        InputData = New System.IO.StreamWriter(RunFilePath & RunCounter & ".in")
        InputFileList.Add(RunFilePath & RunCounter)
        RunCounter += 1
    End Sub

    Private Sub CloseRun()
        If (InputData IsNot Nothing) Then
            InputData.Close()
        End If
    End Sub

    Private Sub CygwinSort()
        Dim myBatFile As New System.IO.StreamWriter(BatFilePath)
        Dim CmdCommand As String

        myBatFile.WriteLine("@echo off")
        'Sort files
        For I As Integer = 0 To InputFileList.Count - 1
            CmdCommand = """" & EngineStartPath & "\Utilities\FileSort\cygwinsort\sort.exe"" -t ""|"" -k 1 """ & InputFileList(I) & ".in"" > """ & InputFileList(I) & ".out"""
            myBatFile.WriteLine(CmdCommand)
        Next
        'Merge files
        If (InputFileList.Count > 1) Then
            CmdCommand = """" & EngineStartPath & "\Utilities\FileSort\cygwinsort\sort.exe"" -t ""|"" -k 1 -m "
            For I As Integer = 0 To InputFileList.Count - 1
                CmdCommand = CmdCommand & " """ & InputFileList(I) & ".out"" "
            Next
            CmdCommand = CmdCommand & " > """ & SortedFilePath & """"
        Else
            CmdCommand = "move """ & InputFileList(0) & ".out"" " & """" & SortedFilePath & """"
        End If
        myBatFile.WriteLine(CmdCommand)
        myBatFile.Close()
        Shell(BatFilePath, AppWinStyle.Hide, True, -1)
    End Sub

    Sub Close()
        SortedData.Close()
    End Sub
End Class
