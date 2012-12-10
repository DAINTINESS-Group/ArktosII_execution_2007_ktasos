Imports Loader
Imports Constructs

Public Class LoadForm
    Private ExecTime(4) As Double
    Private MaxMem(4) As Double
    Private AvgMem(4) As Double

    Dim Arktos As Constructs.Core, PathCollection As New Collection
    Public Sadl As Loader.SadlLoader, Scn As Scenario
    Dim myConsole As ArktosConsole = Nothing
    Dim myParentForm As MainForm

    Sub ClearFileList()
        FileComboBox.Items.Clear()
        PathCollection.Clear()
        FileComboBox.Text = String.Empty
    End Sub

    Sub AddFile(ByVal FileName As String)
        Dim Name As String

        Name = FileName.Substring(FileName.LastIndexOf("\") + 1)
        FileComboBox.Items().Add(Name)
        PathCollection.Add(FileName, Name)
    End Sub

    Private Sub LoadForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        myParentForm = Me.ParentForm
        SetDefaultValues(Nothing, e)
    End Sub

    Private Sub LoadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadButton.Click
        Dim FileName As String, ex As Exception = Nothing

        If (FileComboBox.SelectedIndex() = -1) Then
            Return
        Else
            PrepareConsole()
        End If

        FileName = PathCollection.Item(FileComboBox.SelectedIndex() + 1)
        Console.WriteLine(FileName)
        Try
            Console.WriteLine("Start parsing...")
            Arktos = New Core(EngineStartPath)
            Sadl = New SadlLoader(FileName, Arktos)
            Sadl.Load()
        Catch ex
            MsgBox(ex.ToString)
        End Try
        ScenarioListBox.Items.Clear()

        For I As Integer = 1 To Arktos.ScenarioCount
            ScenarioListBox.Items.Add(Arktos.GetScenario(I).GetName)
        Next I
        Console.WriteLine("End parsing...")
    End Sub

    Private Sub ExecuteButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExecuteButton.Click
        Dim ex As Exception = Nothing
        Dim tMonitor As Monitor

        If (ScenarioListBox.SelectedIndex = -1) Then
            Return
        End If
        PrepareConsole()

        'For I As Integer = 1 To 5
        tMonitor = New Monitor

        Try
            'Console.WriteLine("THIS IS THE {0} EXECUTION", I)
            tMonitor.SetParameters(TimeSlotBox.Value, StallTimeBox.Value, PackSizeBox.Value, QueueSizeBox.Value, _
                                   myParentForm.Path, GetSchedulingPolicy(SelectedPolicyBox.Text), _
                                   CPUCount.Value)
            ClearEngineCache()
            tMonitor.Rise(Scn)
            Console.WriteLine("Success")
            'ExecTime(I - 1) = tMonitor.ExecutionTime
            'MaxMem(I - 1) = tMonitor.MaxLoad
            'AvgMem(I - 1) = tMonitor.MemLoad / tMonitor.StatsCounter
            'Thread.Sleep(5000)
        Catch ex
            Console.WriteLine(ex.Message)
            Console.WriteLine("Aborting execution...")
        End Try
        'Next
        'Console.WriteLine("Execution Time")
        'For I As Integer = 1 To 5
        '    Console.WriteLine(ExecTime(I - 1))
        'Next
        'Console.WriteLine("Max Memory")
        'For I As Integer = 1 To 5
        '    Console.WriteLine(MaxMem(I - 1))
        'Next
        'Console.WriteLine("Max Memory")
        'For I As Integer = 1 To 5
        '    Console.WriteLine(AvgMem(I - 1))
        'Next
        ClearEngineCache()
    End Sub

    Private Sub PrepareConsole()
        'If (myConsole Is Nothing) Then
        '    myConsole = New ArktosConsole
        '    myConsole.Show(Me.MdiParent)
        'End If
        'If (myConsole.IsDisposed) Then
        '    myConsole = New ArktosConsole
        '    myConsole.Show(Me.MdiParent)
        'End If
    End Sub

    Private Sub SetDefaultValues(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DefaultBtn.Click
        TimeSlotBox.Value = ConstTimeSlot
        StallTimeBox.Value = ConstStallTime
        QueueSizeBox.Value = ConstQueueSize
        SelectedPolicyBox.Text = SelectedPolicyBox.Items(1)
        PackSizeBox.Value = ConstPackSize
        CPUCount.Value = 1
    End Sub

    Private Sub ActivityListBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ActivityListBox.Click
        Dim CurActivity As Constructs.Activity

        CurActivity = Nothing
        If (Not (ActivityListBox.SelectedItem Is Nothing)) Then
            For I As Integer = 1 To Scn.ActivityCount
                If (Scn.GetActivity(I).GetName = ActivityListBox.SelectedItem) Then
                    CurActivity = Scn.GetActivity(I)
                    Exit For
                End If
            Next I
            TypeField.Text = CurActivity.GetActivityType()
            PolicyField.Text = CurActivity.GetActivityPolicy()
            ActSemanticsField.Text = CurActivity.GetActivitySemantics()
        End If
    End Sub

    Private Sub RecordSetListBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RecordSetListBox.Click
        Dim CurRSet As Constructs.RecordSet

        CurRSet = Nothing
        If (Not (RecordSetListBox.SelectedItem Is Nothing)) Then
            CurRSet = Scn.GetRecordSet(RecordSetListBox.SelectedItem)
            If (CurRSet Is Nothing) Then
                SourceField.Text = String.Empty
                TblSemantics.Text = String.Empty
            Else
                SourceField.Text = CurRSet.GetRecordSetType
                TblSemantics.Text = CurRSet.GetRecordSetSemantics
            End If
        End If
    End Sub

    Private Sub ScenarioListBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ScenarioListBox.Click
        Dim mySelection As Integer = ScenarioListBox.SelectedIndex

        If (mySelection = -1) Then
            Return
        End If
        Scn = Arktos.GetScenario(ScenarioListBox.Items(mySelection))
        ActivityListBox.Items.Clear()
        For I As Integer = 1 To Scn.ActivityCount
            ActivityListBox.Items.Add(Scn.GetActivity(I).GetName)
        Next I
        RecordSetListBox.Items.Clear()
        For I As Integer = 1 To Scn.RecordSetCount
            RecordSetListBox.Items.Add(Scn.GetRecordSet(I).GetName)
        Next I
    End Sub

    Private Sub ClearCache_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearCache.Click
        Dim tMonitor As New Monitor
        tMonitor.SetParameters(TimeSlotBox.Value, StallTimeBox.Value, PackSizeBox.Value, QueueSizeBox.Value, _
                               myParentForm.Path, GetSchedulingPolicy(SelectedPolicyBox.Text), _
                               CPUCount.Value)
        ClearEngineCache()
        tMonitor = Nothing
    End Sub

    Private Sub CPUCount_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CPUCount.ValueChanged
        If (CPUCount.Value > Environment.ProcessorCount) Then
            CPUCount.Value = Environment.ProcessorCount
        End If
    End Sub

    Private Sub SelectedPolicyBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SelectedPolicyBox.SelectedIndexChanged
        If (SelectedPolicyBox.SelectedIndex = 2) Then
            TimeSlotBox.Value = 70
        Else
            TimeSlotBox.Value = 0
        End If
    End Sub

    Private Sub ScriptButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScriptButton.Click
        Dim ScriptFile As New System.IO.StreamReader("C:\myscript.txt")
        Dim ResultFile As System.IO.StreamWriter
        Dim ScenarioPath, ResultPath As String

        While (ScriptFile.Peek() > 0)
            ScenarioPath = ScriptFile.ReadLine()
            ResultPath = ScriptFile.ReadLine()
            'Parsing the scenario
            Try
                Console.WriteLine("Start parsing...")
                Arktos = New Core(EngineStartPath)
                Sadl = New SadlLoader(ScenarioPath, Arktos)
                Sadl.Load()
            Catch ex2 As Exception
                MsgBox(ex2.ToString)
            End Try
            ScenarioListBox.Items.Clear()

            For I As Integer = 1 To Arktos.ScenarioCount
                ScenarioListBox.Items.Add(Arktos.GetScenario(I).GetName)
            Next I
            Console.WriteLine("End parsing...")

            'Executing The Scenario
            Dim tMonitor As Monitor

            'PrepareConsole()

            For Sched As Integer = 1 To 3
                If (Sched = 1) Then
                    TimeSlotBox.Value = 0
                    StallTimeBox.Value = 4
                    PackSizeBox.Value = 400
                    QueueSizeBox.Value = 100
                    SelectedPolicyBox.SelectedIndex = 0
                    ResultFile = New System.IO.StreamWriter(ResultPath.Replace(".txt", "_RR.txt"))
                End If
                If (Sched = 2) Then
                    TimeSlotBox.Value = 0
                    StallTimeBox.Value = 4
                    PackSizeBox.Value = 400
                    QueueSizeBox.Value = 100
                    SelectedPolicyBox.SelectedIndex = 1
                    ResultFile = New System.IO.StreamWriter(ResultPath.Replace(".txt", "_MC.txt"))
                End If
                If (Sched = 3) Then
                    TimeSlotBox.Value = 70
                    StallTimeBox.Value = 4
                    PackSizeBox.Value = 400
                    QueueSizeBox.Value = 100
                    SelectedPolicyBox.SelectedIndex = 2
                    ResultFile = New System.IO.StreamWriter(ResultPath.Replace(".txt", "_MM.txt"))
                End If
                For I As Integer = 1 To 5
                    tMonitor = New Monitor
                    'MsgBox(ScenarioListBox.Items(0))
                    Scn = Arktos.GetScenario(ScenarioListBox.Items(0))
                    Try
                        ClearEngineCache()
                        Console.WriteLine("THIS IS THE {0} EXECUTION", I)
                        tMonitor.SetParameters(TimeSlotBox.Value, StallTimeBox.Value, PackSizeBox.Value, QueueSizeBox.Value, _
                                               myParentForm.Path, GetSchedulingPolicy(SelectedPolicyBox.Text), _
                                               CPUCount.Value)
                        tMonitor.Rise(Scn)
                        Console.WriteLine("Success")
                        ExecTime(I - 1) = tMonitor.ExecutionTime
                        MaxMem(I - 1) = tMonitor.MaxLoad
                        AvgMem(I - 1) = tMonitor.MemLoad / tMonitor.StatsCounter
                        Thread.Sleep(5000)
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                        Console.WriteLine("Aborting execution...")
                    End Try
                Next
                ResultFile.WriteLine("Execution Time")
                For I As Integer = 1 To 5
                    ResultFile.WriteLine(ExecTime(I - 1))
                Next
                ResultFile.WriteLine("Max Memory")
                For I As Integer = 1 To 5
                    ResultFile.WriteLine(MaxMem(I - 1))
                Next
                ResultFile.WriteLine("Avg Memory")
                For I As Integer = 1 To 5
                    ResultFile.WriteLine(AvgMem(I - 1))
                Next
                ResultFile.Close()
                ClearEngineCache()
            Next
        End While
    End Sub
End Class