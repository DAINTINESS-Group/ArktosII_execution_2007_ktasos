Public Class Schema
    Dim Name As String
    Dim AttributeCollection As New Collection

    Public Sub New(ByVal NewName As String)
        Name = NewName
    End Sub

    Public Function GetName() As String
        Return Name
    End Function

    Public Sub AddAttribute(ByVal AttName As String, ByVal AttType As String)
        AttributeCollection.Add(New Attribute(AttName, AttType), AttName)
    End Sub

    Public Function GetAttribute(ByVal AttributeName As String) As Attribute
        Try
            Return AttributeCollection.Item(AttributeName)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetAttribute(ByVal Position As Integer) As Attribute
        Try
            Return AttributeCollection.Item(Position)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetAttributePosition(ByVal AttributeName As String) As Integer
        Dim CurrentAttribute As Attribute

        For I As Integer = 1 To AttributeCollection.Count
            CurrentAttribute = AttributeCollection.Item(I)
            If (CurrentAttribute.GetName() = AttributeName) Then
                Return I
            End If
        Next
    End Function

    Public Function AttributeCount() As Integer
        Return AttributeCollection.Count
    End Function
End Class

Public Class Attribute
    Dim Name As String
    Dim Type As String

    Public Sub New(ByVal NewName As String, ByVal NewType As String)
        Name = NewName
        Type = NewType
    End Sub

    Public Function GetName() As String
        Return Name
    End Function

    Public Function GetAttributeType() As String
        Return Type
    End Function

    Public Sub SetAttributeType(ByVal NewType As String)
        Type = NewType
    End Sub
End Class

