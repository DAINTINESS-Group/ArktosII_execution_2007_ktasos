Imports Constructs

Public Class SadlLoader
    Inherits Loader
    Dim path As String, Str As System.IO.StreamReader, Lexer As L, Parser As P

    Public Sub New(ByVal Newpath As String, ByRef NewArktos As Core)
        MyBase.New(NewArktos)
        path = Newpath
    End Sub

    Public Overrides Sub Load()
        SadlBaseLoad()
        AssignNodeProperties()
    End Sub

    Private Sub SadlBaseLoad()
        Dim CurrentSchema As Schema, CurrentActivity As Activity
        Dim CurrentRecordSet As RecordSet, CurrentEdge As Edge, CurrentScenario As Scenario

        Str = New System.IO.StreamReader(path)
        Lexer = New L(Str)
        Parser = New P(Lexer)
        Try
            Parser.program()
            Str.Close()
        Catch ex As Exception
            Console.WriteLine("SADL loader: exception caught! {0} {1}", ex.Message, Lexer.getLine())
            Str.Close()
            Return
        End Try

        'This part loads all data from Parser Object to Arktos
        'All information needed will be saved in a Core object
        LoadSchemas()
        LoadActivities()
        LoadRecordSets()
        LoadEdges()
        LoadScenarios()

        For I As Integer = 1 To Arktos.SchemaCount
            CurrentSchema = Arktos.GetSchema(I)
            Console.WriteLine(CurrentSchema.GetName)
        Next
        For I As Integer = 1 To Arktos.ActivityCount
            CurrentActivity = Arktos.GetActivity(I)
            Console.WriteLine(CurrentActivity.GetName)
        Next
        For I As Integer = 1 To Arktos.RecordSetCount
            CurrentRecordSet = Arktos.GetRecordSet(I)
            Console.WriteLine(CurrentRecordSet.GetName)
        Next
        For I As Integer = 1 To Arktos.EdgeCount
            CurrentEdge = Arktos.GetEdge(I)
            Console.WriteLine(CurrentEdge.GetName)
        Next
        For I As Integer = 1 To Arktos.ScenarioCount
            CurrentScenario = Arktos.GetScenario(I)
            'Console.WriteLine(CurrentScenario.GetName)
            'Console.WriteLine("Acts: " & CurrentScenario.ActivityCount)
            'Console.WriteLine("Edgs: " & CurrentScenario.EdgeCount)
            'Console.WriteLine("RSts: " & CurrentScenario.RecordSetCount)
            'Console.WriteLine("Schs: " & CurrentScenario.SchemaCount)
        Next
    End Sub

    Private Sub LoadSchemas()
        Dim CurrentSchema As Schema

        'Loading Schemas into 'Core' object
        For I As Integer = 0 To Parser.AllSchemas.Count - 1
            CurrentSchema = Parser.AllSchemas.GetByIndex(I)
            Arktos.AddSchema(CurrentSchema)
        Next
    End Sub

    Private Sub LoadActivities()
        Dim CurrentActivity As Activity
        Dim LoadActivity As SADLActivity

        If (Parser.AllActivities.Count <> Parser.AllSADLActivities.Count) Then
            Dim ex As New Exception("SADL: Internal Error." & vbNewLine & "Non matching counts of activity objects")
            Throw ex
        End If
        For I As Integer = 0 To Parser.AllActivities.Count - 1
            CurrentActivity = Parser.AllActivities.GetByIndex(I)
            LoadActivity = Parser.AllSADLActivities.GetByIndex(I)
            If (CurrentActivity.GetName <> LoadActivity.GetName) Then
                Console.WriteLine("Must write code for matching 'Constructs' and 'SADL' activities!")
            End If
            'Load all input edges information
            For J As Integer = 0 To LoadActivity.InputCount() - 1
                CurrentActivity.NewInputEdge(LoadActivity.GetInputEdge(J), Arktos.GetSchema(LoadActivity.GetInputSchema(J)))
            Next
            'Load all output edges information
            For J As Integer = 0 To LoadActivity.OutputCount() - 1
                CurrentActivity.NewOutputEdge(LoadActivity.GetOutputEdge(J), Arktos.GetSchema(LoadActivity.GetOutputSchema(J)))
            Next
            Arktos.AddActivity(CurrentActivity)
        Next
    End Sub

    Private Sub LoadRecordSets()
        Dim CurrentRecordSet As RecordSet
        Dim LoadRecordSet As SADLRecordSet

        If (Parser.AllRecordSets.Count <> Parser.AllSADLRecordSets.Count) Then
            Dim ex As New Exception("SADL: Internal Error." & vbNewLine & "Non matching counts of recordset objects")
            Throw ex
        End If
        For I As Integer = 0 To Parser.AllRecordSets.Count - 1
            CurrentRecordSet = Parser.AllRecordSets.GetByIndex(I)
            LoadRecordSet = Parser.AllSADLRecordSets.GetByIndex(I)
            CurrentRecordSet.SetSchema(Arktos.GetSchema(LoadRecordSet.GetSchemaName()))
            Arktos.AddRecordSet(CurrentRecordSet)
        Next
    End Sub

    Public Sub LoadEdges()
        Dim CurrentEdge As Edge = Nothing
        Dim LoadEdge As SADLEdge
        Dim StartNode, EndNode As Node

        For I As Integer = 0 To Parser.AllSADLEdges.Count - 1
            LoadEdge = Parser.AllSADLEdges.GetByIndex(I)
            'Console.WriteLine("in " + LoadEdge.GetStartName)
            'Console.WriteLine("out " + LoadEdge.GetEndName)
            StartNode = Arktos.GetRecordSet(LoadEdge.GetStartName())
            If (StartNode Is Nothing) Then
                StartNode = Arktos.GetActivity(LoadEdge.GetStartName())
            End If
            EndNode = Arktos.GetRecordSet(LoadEdge.GetEndName())
            If (EndNode Is Nothing) Then
                EndNode = Arktos.GetActivity(LoadEdge.GetEndName())
            End If

            Try
                CurrentEdge = New Edge(StartNode, EndNode)
                CurrentEdge.SetStartOutput(LoadEdge.GetStartEdge())
                CurrentEdge.SetEndInput(LoadEdge.GetEndEdge())
                Arktos.AddEdge(CurrentEdge)
            Catch ex As Exception
                Console.WriteLine("Failed on edge: {0} -> {1}", LoadEdge.GetStartName(), LoadEdge.GetEndName())
            End Try
        Next
    End Sub

    Public Sub LoadScenarios()
        Dim CurrentScenario As Scenario
        Dim LoadScenario As SADLScenario
        Dim CurrentActivity As Activity
        Dim CurrentEdge As Edge
        Dim CurrentSchema As Schema
        Dim CurrentStart, CurrentEnd As Node

        If (Parser.AllScenarios.Count <> Parser.AllSADLScenarios.Count) Then
            Dim ex As New Exception("SADL: Internal Error." & vbNewLine & "Non matching counts of Scenario objects")
            Throw ex
        End If
        For I As Integer = 0 To Parser.AllScenarios.Count - 1
            CurrentScenario = Parser.AllScenarios.GetByIndex(I)
            LoadScenario = Parser.AllSADLScenarios.GetByIndex(I)

            For J As Integer = 0 To LoadScenario.GetActivityCount - 1
                CurrentActivity = Arktos.GetActivity(LoadScenario.GetActivity(J))
                If (CurrentActivity IsNot Nothing) Then
                    CurrentScenario.AddActivity(CurrentActivity)
                End If
            Next

            For J As Integer = 1 To Arktos.EdgeCount
                CurrentEdge = Arktos.GetEdge(J)
                If (CurrentScenario.GetActivity(CurrentEdge.GetStart.GetName) IsNot Nothing) Then
                    CurrentScenario.AddEdge(CurrentEdge)
                End If
                If (CurrentScenario.GetActivity(CurrentEdge.GetEnd.GetName) IsNot Nothing) Then
                    CurrentScenario.AddEdge(CurrentEdge)
                End If
            Next

            For J As Integer = 1 To CurrentScenario.EdgeCount
                CurrentEdge = Arktos.GetEdge(J)
                CurrentStart = Arktos.GetRecordSet(CurrentEdge.GetStart.GetName)
                CurrentEnd = Arktos.GetRecordSet(CurrentEdge.GetEnd.GetName)
                If (CurrentStart IsNot Nothing) Then
                    CurrentScenario.AddRecordSet(CurrentStart)
                End If
                If (CurrentEnd IsNot Nothing) Then
                    CurrentScenario.AddRecordSet(CurrentEnd)
                End If
            Next

            For J As Integer = 1 To CurrentScenario.RecordSetCount
                CurrentSchema = CurrentScenario.GetRecordSet(J).GetSchema()
                CurrentScenario.AddSchema(CurrentSchema)
            Next

            For J As Integer = 1 To CurrentScenario.ActivityCount
                For K As Integer = 1 To CurrentScenario.GetActivity(J).InputCount
                    CurrentSchema = CurrentScenario.GetActivity(J).InputSchema(K)
                    CurrentScenario.AddSchema(CurrentSchema)
                Next
                For K As Integer = 1 To CurrentScenario.GetActivity(J).OutputCount
                    CurrentSchema = CurrentScenario.GetActivity(J).OutputSchema(K)
                    CurrentScenario.AddSchema(CurrentSchema)
                Next
            Next
            Arktos.AddScenario(CurrentScenario)
        Next
    End Sub


    Private Sub AssignNodeProperties()
        Dim CurStart, CurEnd As Node
        Dim CurEdge As Edge

        For I As Integer = 1 To Arktos.EdgeCount
            CurEdge = Arktos.GetEdge(I)
            CurStart = Arktos.GetEdge(I).GetStart
            CurEnd = Arktos.GetEdge(I).GetEnd

            CurStart.OutDegree += 1
            CurStart.SetSuccessor(CurEdge)

            CurEnd.InDegree += 1
            CurEnd.SetAncestor(CurEdge)
        Next
    End Sub
End Class
