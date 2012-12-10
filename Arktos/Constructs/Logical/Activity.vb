Public Class Activity
    Inherits Node

    Dim Type, Policy, ReportArg, ReportTo, Semantics As String
    Dim AllInput, SchInput, AllOutput, SchOutput As SortedList

    Sub New(ByVal NewName As String)
        MyBase.New(NewName)
        AllInput = New SortedList
        SchInput = New SortedList
        AllOutput = New SortedList
        SchOutput = New SortedList
    End Sub

    Sub SetActivityType(ByVal NewType As String)
        Type = NewType
    End Sub

    Function GetActivityType() As String
        Return Type
    End Function

    Sub SetActivityPolicy(ByVal NewPolicy As String)
        Policy = NewPolicy
    End Sub

    Function GetActivityPolicy() As String
        Return Policy
    End Function

    Sub SetReportArgs(ByVal ArgType As String, ByVal NewArg As String)
        If (Policy.ToLower <> "report") Then
            Return
        End If
        ReportArg = ArgType
        ReportTo = NewArg
    End Sub

    Sub SetActivitySemantics(ByVal newsemantics As String)
        Semantics = newsemantics.Substring(1, newsemantics.Length - 2)
    End Sub

    Function GetActivitySemantics() As String
        Return Semantics
    End Function

    Sub NewInputEdge(ByVal EdgeName As String, ByVal InSchema As Schema)
        AllInput.Add(AllInput.Count, EdgeName)
        SchInput.Add(SchInput.Count, InSchema)
    End Sub

    Sub NewOutputEdge(ByVal EdgeName As String, ByVal OutSchema As Schema)
        AllOutput.Add(AllOutput.Count, EdgeName)
        SchOutput.Add(SchOutput.Count, OutSchema)
    End Sub

    Sub Dump(ByVal path As String)
        Dim act_output As New System.IO.StreamWriter(path + "\dump\act_" + Name + ".txt")
        Dim CurSch As Schema

        act_output.WriteLine("InDegree {0} OutDegree {1}", InDegree, OutDegree)
        act_output.WriteLine("Declared input edges:")
        For I As Integer = 0 To SchInput.Count - 1
            CurSch = SchInput.GetByIndex(I)
            act_output.WriteLine(AllInput.GetByIndex(I) + ":   " + CurSch.GetName)
            For J As Integer = 1 To CurSch.AttributeCount
                act_output.WriteLine(vbTab + "Name: " + CurSch.GetAttribute(J).GetName + " Type: " + CurSch.GetAttribute(J).GetAttributeType)
            Next J
        Next I
        act_output.WriteLine("Declared output edges:")
        For I As Integer = 0 To SchOutput.Count - 1
            CurSch = SchOutput.GetByIndex(I)
            act_output.WriteLine(AllOutput.GetByIndex(I) + ":   " + CurSch.GetName)
            For J As Integer = 1 To CurSch.AttributeCount
                act_output.WriteLine(vbTab + "Name: " + CurSch.GetAttribute(J).GetName + " Type: " + CurSch.GetAttribute(J).GetAttributeType)
            Next J
        Next I
        act_output.WriteLine("type: " + Type + " policy: " + Policy)
        If (Policy = "REPORT") Then
            act_output.WriteLine("Report to: " + ReportArg + " ---> " + ReportTo)
        End If
        act_output.WriteLine("Semantics: " + Semantics)
        act_output.Close()
    End Sub

    Function InputCount() As Integer
        Return AllInput.Count
    End Function

    Function InputPosition(ByVal CurInput As String) As Integer
        Return AllInput.IndexOfValue(CurInput) + 1
    End Function

    Function InputSchema(ByVal CurInput As String) As Schema
        Return SchInput.GetByIndex(InputPosition(CurInput) - 1)
    End Function

    Function InputSchema(ByVal Position As Integer) As Schema
        Return SchInput.GetByIndex(Position - 1)
    End Function

    Function OutputCount() As Integer
        Return AllOutput.Count
    End Function

    Function OutputPosition(ByVal CurOutput As String) As Integer
        Return AllOutput.IndexOfValue(CurOutput) + 1
    End Function

    Function OutputSchema(ByVal CurOutput As String) As Schema
        Return SchOutput.GetByIndex(OutputPosition(CurOutput) - 1)
    End Function

    Function OutputSchema(ByVal Position As Integer) As Schema
        Return SchOutput.GetByIndex(Position - 1)
    End Function
End Class
