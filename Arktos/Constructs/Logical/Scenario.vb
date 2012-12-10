Public Class Scenario
    Dim Name As String
    Dim AllActivities, AllRecordSets, AllEdges, AllSchemas As Collection

    Public Sub New(ByVal NewName As String)
        AllActivities = New Collection
        AllRecordSets = New Collection
        AllEdges = New Collection
        AllSchemas = New Collection
        Name = NewName
    End Sub

    Public Sub AddActivity(ByVal NewActivity As Activity)
        Try
            AllActivities.Add(NewActivity, NewActivity.GetName)
        Catch ex As Exception
            'Do Nothing
        End Try
    End Sub

    Public Function ActivityCount() As Integer
        Return AllActivities.Count
    End Function

    Public Function GetActivity(ByVal Position As Integer) As Activity
        Try
            Return AllActivities.Item(Position)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetActivity(ByVal Position As String) As Activity
        Try
            Return AllActivities.Item(Position)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub AddRecordSet(ByRef NewRSet As RecordSet)
        Try
            AllRecordSets.Add(NewRSet, NewRSet.GetName)
        Catch ex As Exception
            'Do Nothing
        End Try
    End Sub

    Public Function RecordSetCount() As Integer
        Return AllRecordSets.Count
    End Function

    Public Function GetRecordSet(ByVal Position As Integer) As RecordSet
        Try
            Return AllRecordSets.Item(Position)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetRecordSet(ByVal Position As String) As RecordSet
        Try
            Return AllRecordSets.Item(Position)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub AddEdge(ByRef NewEdge As Edge)
        Try
            AllEdges.Add(NewEdge, NewEdge.GetName)
        Catch ex As Exception
            'Do Nothing
        End Try
    End Sub

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

    Public Function EdgeCount() As Integer
        Return AllEdges.Count
    End Function

    Public Function GetName() As String
        Return Name
    End Function

    Public Sub AddSchema(ByRef NewSchema As Schema)
        Try
            AllSchemas.Add(NewSchema, NewSchema.GetName)
        Catch ex As Exception
            'Do Nothing
        End Try
    End Sub

    Public Function SchemaCount() As Integer
        Return AllSchemas.Count
    End Function

    Public Function GetSchema(ByVal Position As String) As Schema
        Try
            Return AllSchemas.Item(Position)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetSchema(ByVal Position As Integer) As Schema
        Try
            Return AllSchemas.Item(Position)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub Dump(ByVal Path As String)
        Dim ScnOutput As New System.IO.StreamWriter(Path + "\dump\scn_" + Name + ".txt")
        Dim CurrentActivity As Activity
        Dim CurrentRecordSet As RecordSet
        Dim CurrentEdge As Edge

        ScnOutput.WriteLine("*** " + AllActivities.Count.ToString + " Activities Declared ***")
        For I As Integer = 1 To AllActivities.Count
            CurrentActivity = AllActivities.Item(I)
            ScnOutput.WriteLine(CurrentActivity.GetName)
        Next I

        ScnOutput.WriteLine("")
        ScnOutput.WriteLine("*** " + AllRecordSets.Count.ToString + " RecordSets Declared ***")
        For I As Integer = 1 To AllRecordSets.Count
            CurrentRecordSet = AllRecordSets.Item(I)
            ScnOutput.Write("InDegree {0} OutDegree {1} ", CurrentRecordSet.InDegree, CurrentRecordSet.OutDegree)
            ScnOutput.WriteLine(CurrentRecordSet.GetName + " schema: " + CurrentRecordSet.GetSchema.GetName)
        Next I

        ScnOutput.WriteLine("")
        ScnOutput.WriteLine("*** " + AllEdges.Count.ToString + " Edges Declared ***")
        For I As Integer = 1 To AllEdges.Count
            CurrentEdge = AllEdges.Item(I)
            ScnOutput.WriteLine(CurrentEdge.GetName + " -" + CurrentEdge.GetStartOutput + "-" + CurrentEdge.GetEndInput)
        Next I
        ScnOutput.Close()
    End Sub
End Class
