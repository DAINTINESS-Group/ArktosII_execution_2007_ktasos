Imports System.Text

Delegate Sub SetTextCallback(ByVal [text] As String)

Public Class ArktosConsole
    Private myConsole As TextBoxWriter

    Sub New()
        InitializeComponent()
        myConsole = New TextBoxWriter(ConsoleBox, Me)
        Console.SetOut(myConsole)
    End Sub
End Class

Public Class TextBoxWriter
    Inherits System.IO.TextWriter

    Private control As TextBoxBase, Parent As Form
    Private Builder As StringBuilder

    Public Sub New(ByVal control As TextBox, ByVal Parent As Form)
        Me.control = control
        Me.Parent = Parent
        AddHandler control.HandleCreated, _
           New EventHandler(AddressOf OnHandleCreated)
    End Sub

    Public Overrides Sub Write(ByVal ch As Char)
        Write(ch.ToString())
    End Sub

    Public Overrides Sub Write(ByVal s As String)
        If (control.IsHandleCreated) Then
            AppendText(s)
        Else
            BufferText(s)
        End If
    End Sub

    Public Overrides Sub WriteLine(ByVal s As String)
        Write(s + Environment.NewLine)
    End Sub

    Private Sub BufferText(ByVal s As String)
        If (Builder Is Nothing) Then
            Builder = New StringBuilder()
        End If
        Builder.Append(s)
    End Sub

    Private Sub AppendText(ByVal s As String)
        If (Builder Is Nothing = False) Then
            control.AppendText(Builder.ToString())
            Builder = Nothing
        End If

        If Me.control.InvokeRequired Then
            Dim d As New SetTextCallback(AddressOf AppendText)
            Parent.Invoke(d, New Object() {s})
        Else
            Me.control.AppendText(s)
        End If

        'control.AppendText(s)
    End Sub

    Private Sub OnHandleCreated(ByVal sender As Object, _
       ByVal e As EventArgs)
        If (Builder Is Nothing = False) Then
            control.AppendText(Builder.ToString())
            Builder = Nothing
        End If
    End Sub

    Public Overrides ReadOnly Property Encoding() As System.Text.Encoding
        Get
            Return System.Text.Encoding.Default
        End Get
    End Property
End Class
