Public MustInherit Class TwoTupleEvaluator
    Protected LIntValue, RIntValue As Integer
    Protected LStrValue, RStrValue As String

    Public Function Evaluate(ByVal LValue As String, ByVal RValue As String) As Boolean
        LStrValue = LValue
        RStrValue = RValue
        ApplyCast()
        Evaluate = Check()
    End Function

    Protected MustOverride Sub ApplyCast()
    Protected MustOverride Function Check() As Boolean
End Class

Public Class IntegerEqualController
    Inherits TwoTupleEvaluator

    Protected Overrides Sub ApplyCast()
        LIntValue = LStrValue
        RIntValue = RStrValue
    End Sub

    Protected Overrides Function Check() As Boolean
        Check = False
        If (LIntValue = RIntValue) Then
            Check = True
        End If
    End Function
End Class

Public Class IntegerBiggerController
    Inherits TwoTupleEvaluator

    Protected Overrides Sub ApplyCast()
        LIntValue = LStrValue
        RIntValue = RStrValue
    End Sub

    Protected Overrides Function Check() As Boolean
        Check = False
        If (LIntValue > RIntValue) Then
            Check = True
        End If
    End Function
End Class

Public Class IntegerSmallerController
    Inherits TwoTupleEvaluator

    Protected Overrides Sub ApplyCast()
        LIntValue = LStrValue
        RIntValue = RStrValue
    End Sub

    Protected Overrides Function Check() As Boolean
        Check = False
        If (LIntValue < RIntValue) Then
            Check = True
        End If
    End Function
End Class

Public Class IntegerBiggerEqualController
    Inherits TwoTupleEvaluator

    Protected Overrides Sub ApplyCast()
        LIntValue = LStrValue
        RIntValue = RStrValue
    End Sub

    Protected Overrides Function Check() As Boolean
        Check = False
        If (LIntValue >= RIntValue) Then
            Check = True
        End If
    End Function
End Class

Public Class IntegerSmallerEqualController
    Inherits TwoTupleEvaluator

    Protected Overrides Sub ApplyCast()
        LIntValue = LStrValue
        RIntValue = RStrValue
    End Sub

    Protected Overrides Function Check() As Boolean
        Check = False
        If (LIntValue <= RIntValue) Then
            Check = True
        End If
    End Function
End Class

Public Class IntegerNotEqualController
    Inherits TwoTupleEvaluator

    Protected Overrides Sub ApplyCast()
        LIntValue = LStrValue
        RIntValue = RStrValue
    End Sub

    Protected Overrides Function Check() As Boolean
        Check = False
        If (LIntValue <> RIntValue) Then
            Check = True
        End If
    End Function
End Class

Public Class DoubleEqualController
    Inherits TwoTupleEvaluator

    Protected Overrides Sub ApplyCast()
        LIntValue = LStrValue
        RIntValue = RStrValue
    End Sub

    Protected Overrides Function Check() As Boolean
        Check = False
        If (LIntValue = RIntValue) Then
            Check = True
        End If
    End Function
End Class

Public Class DoubleBiggerController
    Inherits TwoTupleEvaluator

    Protected Overrides Sub ApplyCast()
        LIntValue = LStrValue
        RIntValue = RStrValue
    End Sub

    Protected Overrides Function Check() As Boolean
        Check = False
        If (LIntValue > RIntValue) Then
            Check = True
        End If
    End Function
End Class

Public Class DoubleSmallerController
    Inherits TwoTupleEvaluator

    Protected Overrides Sub ApplyCast()
        LIntValue = LStrValue
        RIntValue = RStrValue
    End Sub

    Protected Overrides Function Check() As Boolean
        Check = False
        If (LIntValue < RIntValue) Then
            Check = True
        End If
    End Function
End Class

Public Class DoubleBiggerEqualController
    Inherits TwoTupleEvaluator

    Protected Overrides Sub ApplyCast()
        LIntValue = LStrValue
        RIntValue = RStrValue
    End Sub

    Protected Overrides Function Check() As Boolean
        Check = False
        If (LIntValue >= RIntValue) Then
            Check = True
        End If
    End Function
End Class

Public Class DoubleSmallerEqualController
    Inherits TwoTupleEvaluator

    Protected Overrides Sub ApplyCast()
        LIntValue = LStrValue
        RIntValue = RStrValue
    End Sub

    Protected Overrides Function Check() As Boolean
        Check = False
        If (LIntValue <= RIntValue) Then
            Check = True
        End If
    End Function
End Class

Public Class DoubleNotEqualController
    Inherits TwoTupleEvaluator

    Protected Overrides Sub ApplyCast()
        LIntValue = LStrValue
        RIntValue = RStrValue
    End Sub

    Protected Overrides Function Check() As Boolean
        Check = False
        If (LIntValue <> RIntValue) Then
            Check = True
        End If
    End Function
End Class

Public Class StringEqualController
    Inherits TwoTupleEvaluator

    Protected Overrides Sub ApplyCast()
    End Sub

    Protected Overrides Function Check() As Boolean
        Check = False
        If (LStrValue = RStrValue) Then
            Check = True
        End If
    End Function
End Class

Public Class StringBiggerController
    Inherits TwoTupleEvaluator

    Protected Overrides Sub ApplyCast()
    End Sub

    Protected Overrides Function Check() As Boolean
        Check = False
        If (LStrValue > RStrValue) Then
            Check = True
        End If
    End Function
End Class

Public Class StringSmallerController
    Inherits TwoTupleEvaluator

    Protected Overrides Sub ApplyCast()
    End Sub

    Protected Overrides Function Check() As Boolean
        Check = False
        If (LStrValue < RStrValue) Then
            Check = True
        End If
    End Function
End Class

Public Class StringBiggerEqualController
    Inherits TwoTupleEvaluator

    Protected Overrides Sub ApplyCast()
    End Sub

    Protected Overrides Function Check() As Boolean
        Check = False
        If (LStrValue >= RStrValue) Then
            Check = True
        End If
    End Function
End Class

Public Class StringSmallerEqualController
    Inherits TwoTupleEvaluator

    Protected Overrides Sub ApplyCast()
    End Sub

    Protected Overrides Function Check() As Boolean
        Check = False
        If (LStrValue <= RStrValue) Then
            Check = True
        End If
    End Function
End Class

Public Class StringNotEqualController
    Inherits TwoTupleEvaluator

    Protected Overrides Sub ApplyCast()
    End Sub

    Protected Overrides Function Check() As Boolean
        Check = False
        If (LStrValue <> RStrValue) Then
            Check = True
        End If
    End Function
End Class

Module TwoTupleEvaluatorModule
    Function Get2TEvaluator(ByVal CheckOperation As String, ByVal DataType As String) As TwoTupleEvaluator
        Get2TEvaluator = Nothing

        Select Case DataType.ToLower
            Case "int", "integer"
                Get2TEvaluator = IntegerEvaluator(CheckOperation)
            Case "double"
                Get2TEvaluator = DoubleEvaluator(CheckOperation)
            Case "string"
                Get2TEvaluator = StringEvaluator(CheckOperation)
        End Select
    End Function

    Private Function IntegerEvaluator(ByVal CheckOperation As String) As TwoTupleEvaluator
        IntegerEvaluator = Nothing
        Select Case CheckOperation
            Case "="
                IntegerEvaluator = New IntegerEqualController()
            Case ">"
                IntegerEvaluator = New IntegerBiggerController()
            Case "<"
                IntegerEvaluator = New IntegerSmallerController()
            Case ">="
                IntegerEvaluator = New IntegerBiggerEqualController()
            Case "<="
                IntegerEvaluator = New IntegerSmallerEqualController()
            Case "<>"
                IntegerEvaluator = New IntegerNotEqualController()
        End Select
    End Function

    Private Function DoubleEvaluator(ByVal CheckOperation As String) As TwoTupleEvaluator
        DoubleEvaluator = Nothing
        Select Case CheckOperation
            Case "="
                DoubleEvaluator = New DoubleEqualController()
            Case ">"
                DoubleEvaluator = New DoubleBiggerController()
            Case "<"
                DoubleEvaluator = New DoubleSmallerController()
            Case ">="
                DoubleEvaluator = New DoubleBiggerEqualController()
            Case "<="
                DoubleEvaluator = New DoubleSmallerEqualController()
            Case "<>"
                DoubleEvaluator = New DoubleNotEqualController()
        End Select
    End Function

    Private Function StringEvaluator(ByVal CheckOperation As String) As TwoTupleEvaluator
        StringEvaluator = Nothing
        Select Case CheckOperation
            Case "="
                StringEvaluator = New StringEqualController()
            Case ">"
                StringEvaluator = New StringBiggerController()
            Case "<"
                StringEvaluator = New StringSmallerController()
            Case ">="
                StringEvaluator = New StringBiggerEqualController()
            Case "<="
                StringEvaluator = New StringSmallerEqualController()
            Case "<>"
                StringEvaluator = New StringNotEqualController()
        End Select
    End Function
End Module