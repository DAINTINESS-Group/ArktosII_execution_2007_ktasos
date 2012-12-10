<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ArktosConsole
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ConsoleBox = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'ConsoleBox
        '
        Me.ConsoleBox.BackColor = System.Drawing.Color.Black
        Me.ConsoleBox.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.ConsoleBox.ForeColor = System.Drawing.Color.White
        Me.ConsoleBox.Location = New System.Drawing.Point(0, -1)
        Me.ConsoleBox.Multiline = True
        Me.ConsoleBox.Name = "ConsoleBox"
        Me.ConsoleBox.ReadOnly = True
        Me.ConsoleBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.ConsoleBox.Size = New System.Drawing.Size(546, 276)
        Me.ConsoleBox.TabIndex = 0
        '
        'ArktosConsole
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(544, 276)
        Me.Controls.Add(Me.ConsoleBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "ArktosConsole"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reporting Console"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ConsoleBox As System.Windows.Forms.TextBox
End Class
