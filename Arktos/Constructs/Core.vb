Imports Constructs

Public Class Core
    Dim AllScenarios, AllActivities, AllRSets, AllSchemas, AllEdges As Collection
    Dim Startpath As String

    Public Sub New(ByVal CurrentStartPath As String)
        AllScenarios = New Collection
        AllActivities = New Collection
        AllRSets = New Collection
        AllSchemas = New Collection
        AllEdges = New Collection
        Startpath = CurrentStartPath
    End Sub

    Public Sub AddScenario(ByVal CurObj As Scenario)
        Try
            AllScenarios.Add(CurObj, CurObj.GetName)
        Catch ex As Exception
            'Do Nothing
        End Try
    End Sub

    Public Function ScenarioCount() As Integer
        Return AllScenarios.Count
    End Function

    Public Function GetScenario(ByVal Position As Integer) As Scenario
        Try
            Return AllScenarios.Item(Position)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetScenario(ByVal Position As String) As Scenario
        Try
            Return AllScenarios.Item(Position)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub AddActivity(ByVal CurObj As Activity)
        Try
            AllActivities.Add(CurObj, CurObj.GetName)
        Catch ex2 As Exception
            'Do Nothing
        End Try
    End Sub

    Public Function ActivityCount() As Integer
        Return AllActivities.Count
    End Function

    Public Function GetActivity(ByVal position As Integer) As Activity
        Try
            Return AllActivities.Item(position)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetActivity(ByVal position As String) As Activity
        Try
            Return AllActivities.Item(position)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub AddRecordSet(ByVal CurRSet As RecordSet)
        Try
            AllRSets.Add(CurRSet, CurRSet.GetName)
        Catch ex As Exception
            'Do Nothing
        End Try
    End Sub

    Public Function RecordSetCount() As Integer
        Return AllRSets.Count
    End Function

    Public Function GetRecordSet(ByVal Position As Integer) As RecordSet
        Try
            Return AllRSets.Item(Position)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetRecordSet(ByVal Position As String) As RecordSet
        Try
            Return AllRSets.Item(Position)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub AddSchema(ByVal CurSchema As Schema)
        Try
            AllSchemas.Add(CurSchema, CurSchema.GetName)
        Catch ex As Exception
            'Do Nothing
        End Try
    End Sub

    Public Function SchemaCount() As Integer
        Return AllSchemas.Count
    End Function

    Public Function GetSchema(ByVal Position As Integer) As Schema
        Try
            Return AllSchemas.Item(Position)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetSchema(ByVal Position As String) As Schema
        Try
            Return AllSchemas.Item(Position)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub AddEdge(ByVal CurObj As Edge)
        Try
            AllEdges.Add(CurObj, CurObj.GetName)
        Catch ex As Exception
            'Do Nothing
        End Try
    End Sub

    Public Function EdgeCount() As Integer
        Return AllEdges.Count
    End Function

    Public Function GetEdge(ByVal Position As Integer) As Edge
        Try
            Return AllEdges.Item(Position)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetEdge(ByVal Position As String) As Edge
        Try
            Return AllEdges.Item(Position)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub Clear()
        AllScenarios = New Collection
        AllActivities = New Collection
        AllRSets = New Collection
        AllSchemas = New Collection
        AllEdges = New Collection
    End Sub

    Sub Dump()
        Dim CoreOutput As New System.IO.StreamWriter(Startpath + "\Dump\core_output.txt")
        Dim CurrentScenario As Scenario
        Dim CurrentActivity As Activity
        Dim CurrentRecordSet As RecordSet
        Dim CurrentEdge As Edge
        Dim CurrentSchema As Schema

        CoreOutput.WriteLine("*** Scenarios Declared ***")
        For I As Integer = 1 To AllScenarios.Count
            CurrentScenario = AllScenarios.Item(I)
            CoreOutput.WriteLine(CurrentScenario.GetName)
            CurrentScenario.Dump(Startpath)
        Next I

        CoreOutput.WriteLine("*** Activities Declared ***")
        CurrentActivity = AllActivities.Item(1)
        CoreOutput.Write(CurrentActivity.GetName)
        CurrentActivity.Dump(Startpath)
        For I As Integer = 2 To AllActivities.Count
            CurrentActivity = AllActivities.Item(I)
            CoreOutput.Write(", " + CurrentActivity.GetName)
            CurrentActivity.Dump(Startpath)
        Next I
        CoreOutput.WriteLine()

        CoreOutput.WriteLine("*** RecordSets Declared ***")
        For I As Integer = 1 To AllRSets.Count
            CurrentRecordSet = AllRSets.Item(I)
            CoreOutput.WriteLine(CurrentRecordSet.GetName + " with schema: " + CurrentRecordSet.GetSchema.GetName + " and " + CurrentRecordSet.GetRecordSetType + " " + CurrentRecordSet.GetRecordSetSemantics)
        Next I

        CoreOutput.WriteLine("*** Schemas Declared ***")
        For I As Integer = 1 To AllSchemas.Count
            CurrentSchema = AllSchemas.Item(I)
            CoreOutput.WriteLine(CurrentSchema.GetName)
            For J As Integer = 1 To CurrentSchema.AttributeCount
                CoreOutput.WriteLine(" ~~~~~~ " + CurrentSchema.GetAttribute(J).GetName + vbTab + CurrentSchema.GetAttribute(J).GetAttributeType)
            Next J
        Next I

        CoreOutput.WriteLine("*** Edges Declared ***")
        For I As Integer = 1 To AllEdges.Count
            CurrentEdge = AllEdges.Item(I)
            CoreOutput.WriteLine(CurrentEdge.GetName + ":" + vbTab + CurrentEdge.GetStart.GetName + " goes to " + CurrentEdge.GetEnd.GetName)
        Next I

        CoreOutput.Close()
    End Sub
End Class
