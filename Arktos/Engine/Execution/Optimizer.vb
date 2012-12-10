Imports Constructs

Public Class Optimizer
    Dim Visited As Collection, Scn As Scenario, MissedObjects As Boolean

    Sub New(ByVal ExecScn As Scenario)
        Scn = ExecScn
        Visited = New Collection
    End Sub

    Function Optimize() As Collection
        Dim Counter As Integer
        Dim CurLogObj, CurPhysObj As Object
        Dim CorrectSortedScn, NextStep, ThisStep, SortedScn As Collection
        SortedScn = New Collection
        CorrectSortedScn = New Collection

        If (Not IsGraphCorrect()) Then
            Throw New Exception("Incorrect Graph")
            Exit Function
        Else
            Console.WriteLine("Graph correct")
        End If

        Console.WriteLine("Start optimizing...")
        Counter = 1

        NextStep = New Collection
        For I As Integer = 1 To Scn.EdgeCount
            If (Scn.GetEdge(I).GetStart.InDegree = 0) Then
                CurPhysObj = LogicalToPhysical(Scn.GetEdge(I).GetStart)
                Console.WriteLine(Counter)
                CurPhysObj.SetId(Counter)
                SortedScn.Add(CurPhysObj, Counter)
                Console.WriteLine(Counter)
                Try
                    NextStep.Add(Scn.GetEdge(I).GetEnd, Scn.GetEdge(I).GetEnd.GetName)
                    'Console.WriteLine("{0}", Scn.GetEdge(I).GetStart.GetName)
                Catch ex As Exception
                    'Do Nothing
                End Try
            End If
        Next

        ThisStep = NextStep
        NextStep = New Collection

        While (ThisStep.Count > 0)
            'Console.WriteLine("While: {0}", ThisStep.Count)
            For J As Integer = 1 To Scn.EdgeCount
                Try
                    CurLogObj = ThisStep.Item(Scn.GetEdge(J).GetStart.GetName)

                    CurPhysObj = LogicalToPhysical(CurLogObj)
                    CurPhysObj.SetId(Counter)
                    SortedScn.Add(CurPhysObj, Counter)

                    Try
                        NextStep.Add(Scn.GetEdge(J).GetEnd, Scn.GetEdge(J).GetEnd.GetName)
                        'Console.WriteLine(" --- {0}", Scn.GetEdge(J).GetStart.GetName)
                    Catch ex As Exception
                        'Do Nothing
                    End Try
                Catch ex As Exception
                    'Do Nothing
                End Try
            Next
            ThisStep = NextStep
            NextStep = New Collection
        End While

        For I As Integer = 1 To Scn.EdgeCount
            If (Scn.GetEdge(I).GetEnd.OutDegree = 0) Then
                CurPhysObj = LogicalToPhysical(Scn.GetEdge(I).GetEnd)
                CurPhysObj.SetId(Counter)
                SortedScn.Add(CurPhysObj, Counter)
            End If
        Next

        For I As Integer = 1 To SortedScn.Count
            Try
                CorrectSortedScn.Add(SortedScn(I), SortedScn(I).Construct().GetName())
            Catch ex As Exception

            End Try
            'Console.WriteLine("{0} {1}", SortedScn(I).Construct().GetName(), SortedScn(I).GetId())
        Next
        Console.WriteLine("End optimizing...")
        Return CorrectSortedScn
    End Function

    Private Function LogicalToPhysical(ByVal CurLogObj As Object) As ExecutionItem
        Dim PhysObj As ExecutionItem
        Dim CurActivity As Constructs.Activity

        PhysObj = Nothing
        Select Case CurLogObj.GetType().ToString()
            Case "Constructs.Activity"
                CurActivity = CurLogObj
                Select Case CurActivity.GetActivityType
                    Case "GenericActivity"
                        PhysObj = New GenericActivity(CurLogObj)
                    Case "Filter"
                        PhysObj = New Filter(CurLogObj)
                    Case "NotNull"
                        PhysObj = New Filter(CurLogObj)
                    Case "Join"
                        PhysObj = New NLJ(CurLogObj)
                    Case "SMJoin"
                        PhysObj = New SMJoin(CurLogObj)
                    Case "SMDiff"
                        PhysObj = New SMDiff(CurLogObj)
                    Case "SMSKey"
                        PhysObj = New SMSKey(CurLogObj)
                    Case "Diff"
                        PhysObj = New NLJ(CurLogObj)
                    Case "SurrogateKey"
                        PhysObj = New NLJ(CurLogObj)
                    Case "Aggregator"
                        PhysObj = New Aggregator(CurLogObj)
                    Case "DateKey"
                        PhysObj = New DateKey(CurLogObj)
                    Case "Currency"
                        PhysObj = New Currency(CurLogObj)
                    Case "FunctionActivity"
                        PhysObj = New FunctionActivity(CurLogObj)
                    Case "PhoneFormat"
                        PhysObj = New PhoneFormat(CurLogObj)
                    Case "CreateLookUp"
                        PhysObj = New CreateLookUp(CurLogObj)
                    Case "LIDeriveFunc"
                        PhysObj = New LIDeriveFunc(CurLogObj)
                    Case "PSDeriveFunc"
                        PhysObj = New PSDeriveFunc(CurLogObj)
                End Select
            Case "Constructs.RecordSet"
                If (CurLogObj.InDegree = 0) Then
                    PhysObj = New Reader(CurLogObj)
                ElseIf (CurLogObj.InDegree = 1) Then
                    PhysObj = New Writer(CurLogObj)
                End If
        End Select

        If (PhysObj Is Nothing) Then
            Console.WriteLine("Mapper: Error for {0}", CurLogObj.GetName)
            MissedObjects = True
        End If
        Return PhysObj
    End Function

    Private Function IsGraphCorrect() As Boolean
        Dim CurObj As Object
        Dim ActivityCounter, RecordSetCounter, EdgeCounter As Integer
        Dim NextStep, NowChecking As Collection

        ActivityCounter = 0
        RecordSetCounter = 0
        EdgeCounter = 0
        NextStep = New Collection

        For I As Integer = 1 To Scn.EdgeCount    'Find the starting recordsets
            CurObj = Scn.GetEdge(I).GetEnd
            If (CurObj.GetType.ToString = "Constructs.RecordSet") Then
                Try 'If already visited, we must not count it again!
                    Visited.Add(CurObj, CurObj.GetName)
                    RecordSetCounter += 1   'Count the start node
                    Try
                        NextStep.Add(Scn.GetEdge(I).GetStart, Scn.GetEdge(I).GetStart.GetName)
                        EdgeCounter += 1
                        'Console.WriteLine("Counted Edge {0}", Scn.GetEdge(I).GetName)
                    Catch ex As Exception
                        'Avoid Duplicate Insertion
                    End Try
                Catch ex As Exception
                    'Avoid Duplicate Insertion
                End Try
            End If
        Next
        'Console.WriteLine("Act: {0} RSets: {1} Edges: {2}", ActivityCounter, RecordSetCounter, EdgeCounter)
        'Console.WriteLine()

        While (True)        'Walk through the graph, count the new nodes and all the edges
            NowChecking = NextStep
            NextStep = New Collection

            For I As Integer = 1 To NowChecking.Count
                CurObj = NowChecking.Item(I)
                Try
                    Visited.Add(CurObj, CurObj.GetName)
                    If (CurObj.GetType.ToString = "Constructs.RecordSet") Then
                        RecordSetCounter += 1
                    ElseIf (CurObj.GetType.ToString = "Constructs.Activity") Then
                        ActivityCounter += 1
                    End If

                    For J As Integer = 1 To Scn.EdgeCount    'Counting only end nodes
                        If (CurObj.GetName = Scn.GetEdge(J).GetEnd.GetName) Then
                            EdgeCounter += 1
                            Try
                                NextStep.Add(Scn.GetEdge(J).GetStart, Scn.GetEdge(J).GetStart.GetName)
                                'Console.WriteLine("Counted Edge {0}", Scn.GetEdge(J).GetName)
                            Catch ex As Exception
                                'Avoid Duplicate Insertion
                            End Try
                        End If
                    Next
                Catch ex As Exception
                    'Avoid Duplicate Insertion
                End Try
            Next I

            If (NextStep.Count = 0) Then
                Exit While
            End If
        End While

        If (ActivityCounter = Scn.ActivityCount And RecordSetCounter = Scn.RecordSetCount And EdgeCounter = Scn.EdgeCount) Then
            Return True
        Else
            Console.WriteLine("Act: {0} RSets: {1} Edges: {2}", Scn.ActivityCount, Scn.RecordSetCount, Scn.EdgeCount)
            Console.WriteLine("Act: {0} RSets: {1} Edges: {2}", ActivityCounter, RecordSetCounter, EdgeCounter)
            Return False
        End If
    End Function

    Function Errors() As Boolean
        Errors = MissedObjects
    End Function
End Class
