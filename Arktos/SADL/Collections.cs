public class SADLEdge
{
    private string ActivityName, StartName, EndName, StartOutput, EndInput;

    public SADLEdge(string NewActivityName)
    {
        ActivityName = NewActivityName;
    }

    public void SetNodes(string NewStart, string NewEnd)
    {
        StartName = NewStart;
        EndName = NewEnd;
    }

    public void SetEdges(string NewStart, string NewEnd)
    {
        StartOutput = NewStart;
        EndInput = NewEnd;
    }

    public string GetActivityName()
    {
        return ActivityName;
    }

    public string GetStartName()
    {
        return StartName;
    }

    public string GetEndName()
    {
        return EndName;
    }

    public string GetStartEdge()
    {
        return StartOutput;
    }

    public string GetEndEdge()
    {
        return EndInput;
    }
}

public class SADLRecordSet
{
    private string Name, SchemaName;

    public SADLRecordSet(string NewName)
    {
        Name = NewName;
    }

    public void SetSchema(string NewSchema)
    {
        SchemaName = NewSchema;
    }

    public string GetName()
    {
        return Name;
    }

    public string GetSchemaName()
    {
        return SchemaName;
    }
}

public class SADLActivity
{
    private string Name;
    private System.Collections.SortedList InputEdges;
    private System.Collections.SortedList InputSchemas;
    private System.Collections.SortedList OutputEdges;
    private System.Collections.SortedList OutputSchemas;

    public SADLActivity(string NewName)
    {
        Name = NewName;
        InputEdges = new System.Collections.SortedList();
        InputSchemas = new System.Collections.SortedList();
        OutputEdges = new System.Collections.SortedList();
        OutputSchemas = new System.Collections.SortedList();
    }

    public string GetName()
    {
        return Name;
    }

    public void SetInputEdge(string EdgeName, string SchemaName)
    {
        InputEdges.Add(InputEdges.Count + 1, EdgeName);
        InputSchemas.Add(InputSchemas.Count + 1, SchemaName);
    }

    public void SetOutputEdge(string EdgeName, string SchemaName)
    {
        OutputEdges.Add(OutputEdges.Count + 1, EdgeName);
        OutputSchemas.Add(OutputSchemas.Count + 1, SchemaName);
    }

    public string GetInputEdge(int Position)
    {
        return (string)InputEdges.GetByIndex(Position);
    }

    public string GetOutputEdge(int Position)
    {
        return (string)OutputEdges.GetByIndex(Position);
    }

    public string GetInputSchema(int Position)
    {
        return (string)InputSchemas.GetByIndex(Position);
    }

    public string GetOutputSchema(int Position)
    {
        return (string)OutputSchemas.GetByIndex(Position);
    }

    public int InputCount()
    {
        return InputEdges.Count;
    }

    public int OutputCount()
    {
        return OutputEdges.Count;
    }
}

public class SADLScenario
{
    private string Name;
    private System.Collections.SortedList Activities;

    public SADLScenario(string NewName)
    {
        Name = NewName;
        Activities = new System.Collections.SortedList();
    }

    public string GetName()
    {
        return Name;
    }

    public void AddActivity(string NewActivity)
    {
        Activities.Add(Activities.Count + 1, NewActivity);
    }

    public int GetActivityCount()
    {
        return Activities.Count;
    }

    public string GetActivity(int Position)
    {
        return (string)Activities.GetByIndex(Position);
    }
}
