Public MustInherit Class SingleTupleEvaluator
    Protected Field As Integer

    Public Function Evaluate(ByVal CurrentTuple As String) As Boolean
        Evaluate = PerformCheck(CurrentTuple)
    End Function

    Public MustOverride Function PerformCheck(ByVal CurrentTuple As String) As Boolean
End Class

Public Class NotNullCheck
    Inherits SingleTupleEvaluator

    Dim FieldArray() As Integer

    Sub New(ByVal NotNullField() As Integer)
        FieldArray = NotNullField
    End Sub

    Public Overrides Function PerformCheck(ByVal CurrentTuple As String) As Boolean
        PerformCheck = True
        For I As Integer = 1 To FieldArray.Length - 1
            If (GetField(CurrentTuple, FieldArray(I - 1)) = ArktosNull) Then
                PerformCheck = False
            End If
        Next
    End Function
End Class

Public Class IntegerEqual
    Inherits SingleTupleEvaluator

    Dim Value As Integer

    Sub New(ByVal CompareField As Integer, ByVal CompareValue As Integer)
        Value = CompareValue
        Field = CompareField
    End Sub

    Public Overrides Function PerformCheck(ByVal CurrentTuple As String) As Boolean
        If (GetField(CurrentTuple, Field) = Value) Then
            Return True
        End If
        Return False
    End Function
End Class

Public Class IntegerBigger
    Inherits SingleTupleEvaluator

    Dim Value As Integer

    Sub New(ByVal CompareField As Integer, ByVal CompareValue As Integer)
        Value = CompareValue
        Field = CompareField
    End Sub

    Public Overrides Function PerformCheck(ByVal CurrentTuple As String) As Boolean
        If (GetField(CurrentTuple, Field) > Value) Then
            Return True
        End If
        Return False
    End Function
End Class

Public Class IntegerSmaller
    Inherits SingleTupleEvaluator

    Dim Value As Integer

    Sub New(ByVal CompareField As Integer, ByVal CompareValue As Integer)
        Value = CompareValue
        Field = CompareField
    End Sub

    Public Overrides Function PerformCheck(ByVal CurrentTuple As String) As Boolean
        If (GetField(CurrentTuple, Field) < Value) Then
            Return True
        End If
        Return False
    End Function
End Class

Public Class IntegerBiggerEqual
    Inherits SingleTupleEvaluator

    Dim Value As Integer

    Sub New(ByVal CompareField As Integer, ByVal CompareValue As Integer)
        Value = CompareValue
        Field = CompareField
    End Sub

    Public Overrides Function PerformCheck(ByVal CurrentTuple As String) As Boolean
        If (GetField(CurrentTuple, Field) >= Value) Then
            Return True
        End If
        Return False
    End Function
End Class

Public Class IntegerSmallerEqual
    Inherits SingleTupleEvaluator

    Dim Value As Integer

    Sub New(ByVal CompareField As Integer, ByVal CompareValue As Integer)
        Value = CompareValue
        Field = CompareField
    End Sub

    Public Overrides Function PerformCheck(ByVal CurrentTuple As String) As Boolean
        If (GetField(CurrentTuple, Field) <= Value) Then
            Return True
        End If
        Return False
    End Function
End Class

Public Class IntegerNotEqual
    Inherits SingleTupleEvaluator

    Dim Value As Integer

    Sub New(ByVal CompareField As Integer, ByVal CompareValue As Integer)
        Value = CompareValue
        Field = CompareField
    End Sub

    Public Overrides Function PerformCheck(ByVal CurrentTuple As String) As Boolean
        If (GetField(CurrentTuple, Field) <> Value) Then
            Return True
        End If
        Return False
    End Function
End Class

Public Class StringEqual
    Inherits SingleTupleEvaluator

    Dim Value As String

    Sub New(ByVal CompareField As Integer, ByVal CompareValue As String)
        Value = CompareValue
        Field = CompareField
    End Sub

    Public Overrides Function PerformCheck(ByVal CurrentTuple As String) As Boolean
        If (GetField(CurrentTuple, Field) = Value) Then
            Return True
        End If
        Return False
    End Function
End Class

Public Class StringBigger
    Inherits SingleTupleEvaluator

    Dim Value As String

    Sub New(ByVal CompareField As Integer, ByVal CompareValue As String)
        Value = CompareValue
        Field = CompareField
    End Sub

    Public Overrides Function PerformCheck(ByVal CurrentTuple As String) As Boolean
        If (GetField(CurrentTuple, Field) > Value) Then
            Return True
        End If
        Return False
    End Function
End Class

Public Class StringSmaller
    Inherits SingleTupleEvaluator

    Dim Value As String

    Sub New(ByVal CompareField As Integer, ByVal CompareValue As String)
        Value = CompareValue
        Field = CompareField
    End Sub

    Public Overrides Function PerformCheck(ByVal CurrentTuple As String) As Boolean
        If (GetField(CurrentTuple, Field) < Value) Then
            Return True
        End If
        Return False
    End Function
End Class

Public Class StringBiggerEqual
    Inherits SingleTupleEvaluator

    Dim Value As String

    Sub New(ByVal CompareField As Integer, ByVal CompareValue As String)
        Value = CompareValue
        Field = CompareField
    End Sub

    Public Overrides Function PerformCheck(ByVal CurrentTuple As String) As Boolean
        If (GetField(CurrentTuple, Field) >= Value) Then
            Return True
        End If
        Return False
    End Function
End Class

Public Class StringSmallerEqual
    Inherits SingleTupleEvaluator

    Dim Value As String

    Sub New(ByVal CompareField As Integer, ByVal CompareValue As String)
        Value = CompareValue
        Field = CompareField
    End Sub

    Public Overrides Function PerformCheck(ByVal CurrentTuple As String) As Boolean
        If (GetField(CurrentTuple, Field) <= Value) Then
            Return True
        End If
        Return False
    End Function
End Class

Public Class StringNotEqual
    Inherits SingleTupleEvaluator

    Dim Value As String

    Sub New(ByVal CompareField As Integer, ByVal CompareValue As String)
        Value = CompareValue
        Field = CompareField
    End Sub

    Public Overrides Function PerformCheck(ByVal CurrentTuple As String) As Boolean
        If (GetField(CurrentTuple, Field) <> Value) Then
            Return True
        End If
        Return False
    End Function
End Class

Module SingleTupleEvaluatorModule
    Function GetSingleTupleEvaluator(ByVal InputSchema As Constructs.Schema, ByVal Condition As String) As SingleTupleEvaluator
        Dim Position As Integer
        Dim ConditionSplit As String()
        Dim NewEvaluator As SingleTupleEvaluator = Nothing

        ConditionSplit = Condition.Split(" ".ToCharArray, 3)
        GetSingleTupleEvaluator = Nothing
        For I As Integer = 1 To InputSchema.AttributeCount
            If (InputSchema.GetAttribute(I).GetName() = ConditionSplit.GetValue(0)) Then
                Position = I
                Exit For
            End If
        Next
        If (ConditionSplit.GetValue(2).ToString.Substring(0, 1) = "'") Then
            NewEvaluator = BaseGetSingleTupleEvaluator(ConditionSplit.GetValue(1), InputSchema.GetAttribute(Position).GetAttributeType(), Position, ConditionSplit.GetValue(2).Substring(1, ConditionSplit.GetValue(2).Length - 2))
        Else
            NewEvaluator = BaseGetSingleTupleEvaluator(ConditionSplit.GetValue(1), InputSchema.GetAttribute(Position).GetAttributeType(), Position, ConditionSplit.GetValue(2))
        End If
        GetSingleTupleEvaluator = NewEvaluator
    End Function

    Private Function BaseGetSingleTupleEvaluator(ByVal CheckOperation As String, ByVal DataType As String, ByVal Position As Integer, ByVal CheckValue As String) As SingleTupleEvaluator
        Dim NewEvaluator As SingleTupleEvaluator = Nothing

        NewEvaluator = Nothing
        Select Case DataType.ToLower
            Case "integer", "int"
                NewEvaluator = IntegerCalculator(CheckOperation, Position, CheckValue)
            Case "double"
                NewEvaluator = IntegerCalculator(CheckOperation, Position, CheckValue)
            Case "string"
                NewEvaluator = StringCalculator(CheckOperation, Position, CheckValue)
            Case Else
                MsgBox("dsd")
        End Select
        BaseGetSingleTupleEvaluator = NewEvaluator
    End Function

    Private Function IntegerCalculator(ByVal CheckOperation As String, ByVal Position As Integer, ByVal CheckValue As Integer) As SingleTupleEvaluator
        Dim NewEvaluator As SingleTupleEvaluator = Nothing
        Select Case CheckOperation
            Case "="
                NewEvaluator = New IntegerEqual(Position, CheckValue)
            Case ">"
                NewEvaluator = New IntegerBigger(Position, CheckValue)
            Case "<"
                NewEvaluator = New IntegerSmaller(Position, CheckValue)
            Case ">="
                NewEvaluator = New IntegerBiggerEqual(Position, CheckValue)
            Case "<="
                NewEvaluator = New IntegerSmallerEqual(Position, CheckValue)
            Case "<>"
                NewEvaluator = New IntegerNotEqual(Position, CheckValue)
        End Select
        IntegerCalculator = NewEvaluator
    End Function

    Private Function DoubleCalculator(ByVal CheckOperation As String, ByVal Position As Integer, ByVal CheckValue As Double) As SingleTupleEvaluator
        Dim NewEvaluator As SingleTupleEvaluator = Nothing
        'Select Case CheckOperation
        '    Case "="
        '        NewEvaluator = New DoubleEqual(Position, CheckValue)
        '    Case ">"
        '        NewEvaluator = New DoubleBigger(Position, CheckValue)
        '    Case "<"
        '        NewEvaluator = New DoubleSmaller(Position, CheckValue)
        '    Case ">="
        '        NewEvaluator = New DoubleBiggerEqual(Position, CheckValue)
        '    Case "<="
        '        NewEvaluator = New DoubleSmallerEqual(Position, CheckValue)
        '    Case "<>"
        '        NewEvaluator = New DoubleNotEqual(Position, CheckValue)
        'End Select
        DoubleCalculator = NewEvaluator
    End Function

    Private Function StringCalculator(ByVal CheckOperation As String, ByVal Position As Integer, ByVal CheckValue As String) As SingleTupleEvaluator
        Dim NewEvaluator As SingleTupleEvaluator = Nothing
        Select Case CheckOperation
            Case "="
                NewEvaluator = New StringEqual(Position, CheckValue)
            Case ">"
                NewEvaluator = New StringBigger(Position, CheckValue)
            Case "<"
                NewEvaluator = New StringSmaller(Position, CheckValue)
            Case ">="
                NewEvaluator = New StringBiggerEqual(Position, CheckValue)
            Case "<="
                NewEvaluator = New StringSmallerEqual(Position, CheckValue)
            Case "<>"
                NewEvaluator = New StringNotEqual(Position, CheckValue)
        End Select
        StringCalculator = NewEvaluator
    End Function
End Module

