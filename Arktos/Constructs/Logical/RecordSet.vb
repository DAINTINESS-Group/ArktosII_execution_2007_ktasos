Public Class RecordSet
    Inherits Node

    Dim Type, Semantics As String
    Dim Sch As Schema

    Public Sub New(ByVal NewName As String)
        MyBase.New(NewName)
        Sch = Nothing
    End Sub

    Public Function GetSchema() As Schema
        If (Sch Is Nothing) Then
            Return Nothing
        Else
            Return Sch
        End If
    End Function

    Public Sub SetSchema(ByRef CurSch As Schema)
        Sch = CurSch
    End Sub

    Sub SetRecordSetType(ByVal NewType As String)
        Type = NewType
    End Sub

    Sub SetRecordSetSemantics(ByVal NewSemantics As String)
        Semantics = NewSemantics.Substring(1, NewSemantics.Length - 2)
    End Sub

    Function GetRecordSetType() As String
        Return Type
    End Function

    Function GetRecordSetSemantics() As String
        Return Semantics
    End Function
End Class
