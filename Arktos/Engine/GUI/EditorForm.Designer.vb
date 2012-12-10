<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditorForm
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
        Me.CodeBox = New System.Windows.Forms.RichTextBox
        Me.SuspendLayout()
        '
        'CodeBox
        '
        Me.CodeBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CodeBox.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.CodeBox.Location = New System.Drawing.Point(0, 0)
        Me.CodeBox.Name = "CodeBox"
        Me.CodeBox.Size = New System.Drawing.Size(511, 346)
        Me.CodeBox.TabIndex = 0
        Me.CodeBox.Text = ""
        '
        'EditorForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(511, 346)
        Me.Controls.Add(Me.CodeBox)
        Me.Name = "EditorForm"
        Me.Text = "Editor"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CodeBox As System.Windows.Forms.RichTextBox
End Class
