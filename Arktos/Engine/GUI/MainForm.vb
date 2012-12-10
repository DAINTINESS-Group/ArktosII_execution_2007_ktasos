Imports System.Windows.Forms

Public Class MainForm
    Private m_ChildFormNumber As Integer = 0
    Private LoadFormChild As LoadForm = Nothing
    Public Path As String

    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs) Handles NewToolStripMenuItem.Click, NewToolStripButton.Click, NewWindowToolStripMenuItem.Click
        Dim ChildForm As New EditorForm
        ChildForm.MdiParent = Me

        m_ChildFormNumber += 1
        ChildForm.Text = "Untitled " & m_ChildFormNumber
        ChildForm.Show()
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs) Handles OpenToolStripMenuItem.Click, OpenToolStripButton.Click
        Dim OpenFileDialog As New OpenFileDialog
        'OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            Dim ChildForm As EditorForm, CurrentMdiChild As EditorForm

            For Each CurrentChild As Form In Me.MdiChildren
                If (CurrentChild.GetType Is (New EditorForm).GetType()) Then
                    CurrentMdiChild = CurrentChild
                    If (CurrentMdiChild.ItemFilePath = FileName) Then
                        CurrentMdiChild.Activate()
                        Return
                    End If
                End If
            Next

            ChildForm = New EditorForm
            ChildForm.MdiParent = Me
            ChildForm.Text = FileName.Substring(FileName.LastIndexOf("\") + 1)
            Try
                ChildForm.LoadCodeBox(FileName)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            ChildForm.Show()
        End If
    End Sub

    Private Sub SaveActiveChild(ByVal sender As Object, ByVal e As EventArgs) Handles SaveToolStripMenuItem.Click, SaveToolStripButton.Click
        If (Me.ActiveMdiChild IsNot Nothing) Then
            If (Me.ActiveMdiChild.GetType Is (New EditorForm).GetType()) Then
                Dim CurrentActiveChild As EditorForm
                CurrentActiveChild = Me.ActiveMdiChild
                CurrentActiveChild.SaveCodeBox()
            End If
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            If (Me.ActiveMdiChild IsNot Nothing) Then
                If (Me.ActiveMdiChild.GetType Is (New EditorForm).GetType()) Then
                    Dim CurrentActiveChild As EditorForm
                    CurrentActiveChild = Me.ActiveMdiChild
                    CurrentActiveChild.SaveAsCodeBox(FileName)
                    CurrentActiveChild.Text = FileName.Substring(FileName.LastIndexOf("\") + 1)
                End If
            End If
        End If
    End Sub

    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        Global.System.Windows.Forms.Application.Exit()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CutToolStripMenuItem.Click
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CopyToolStripMenuItem.Click
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PasteToolStripMenuItem.Click
        'Use My.Computer.Clipboard.GetText() or My.Computer.Clipboard.GetData to retrieve information from the clipboard.
    End Sub

    Private Sub ToolBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolBarToolStripMenuItem.Click
        Me.ToolStrip.Visible = Me.ToolBarToolStripMenuItem.Checked
    End Sub

    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StatusBarToolStripMenuItem.Click
        Me.StatusStrip.Visible = Me.StatusBarToolStripMenuItem.Checked
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CascadeToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticleToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileVerticalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ArrangeIconsToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CloseAllToolStripMenuItem.Click
        'Close all child forms of the parent.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private Sub LoadFormButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadFormButton.Click
        Dim CurrentMdiChild As EditorForm

        If (LoadFormChild Is Nothing) Then
            LoadFormChild = New LoadForm
            LoadFormChild.MdiParent = Me
            LoadFormChild.Show()
        Else
            LoadFormChild.ClearFileList()
            LoadFormChild.Show()
            LoadFormChild.Activate()
        End If

        For Each CurrentChild As Form In Me.MdiChildren
            If (CurrentChild.GetType Is (New EditorForm).GetType()) Then
                CurrentMdiChild = CurrentChild
                LoadFormChild.AddFile(CurrentMdiChild.ItemFilePath())
            End If
        Next
    End Sub

    Private Sub MainForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Path = CurDir()

        Dim myTimer As New HiPerfTimer()
        For I As Integer = 0 To 5
            myTimer.TimerStart()
            Thread.Sleep(I)
            myTimer.TimerStop()
            Console.WriteLine("Slept {1}. Duration: {0} msec", myTimer.Duration, I)
        Next
    End Sub
End Class
