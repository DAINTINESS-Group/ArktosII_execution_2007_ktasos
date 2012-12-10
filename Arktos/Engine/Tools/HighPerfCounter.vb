'Original Code in C# by Daniel Strigl.
'http://www.codeproject.com/csharp/highperformancetimercshar.asp
'On-line code tranformation to Vb .NET by
'http://www.developerfusion.co.uk/utilities/convertcsharptovb.aspx

Imports System.Runtime.InteropServices
Imports System.ComponentModel

Public Class HiPerfTimer
    <DllImport("Kernel32.dll")> _
    Private Shared Function QueryPerformanceCounter(ByRef lpPerformanceCount As Long) As Boolean
    End Function

    <DllImport("Kernel32.dll")> _
    Private Shared Function QueryPerformanceFrequency(ByRef lpFrequency As Long) As Boolean
    End Function

    Private startTime, stopTime, freq As Long

    'Constructor
    Public Sub New()
        startTime = 0
        stopTime = 0
        'high-performance counter not supported
        If QueryPerformanceFrequency(freq) = False Then
            Throw New Win32Exception()
        End If
    End Sub

    ' Start the timer
    Public Sub TimerStart()
        ' lets do the waiting threads there work
        Thread.Sleep(0)

        QueryPerformanceCounter(startTime)
    End Sub

    ' Stop the timer
    Public Sub TimerStop()
        QueryPerformanceCounter(stopTime)
    End Sub

    'Returns the duration of the timer (in msecs)
    Public ReadOnly Property Duration() As Double
        Get
            Return CDbl((stopTime - startTime) * 1000) / CDbl(freq)
        End Get
    End Property
End Class
