<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoadForm
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
        Me.LoadGroupBox = New System.Windows.Forms.GroupBox
        Me.FileComboBox = New System.Windows.Forms.ComboBox
        Me.LoadButton = New System.Windows.Forms.Button
        Me.ExecutionGroupBox = New System.Windows.Forms.GroupBox
        Me.ClearCache = New System.Windows.Forms.Button
        Me.TblSemantics = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.SourceField = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.ActSemanticsField = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.PolicyField = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.TypeField = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.RecordSetListBox = New System.Windows.Forms.ListBox
        Me.ActivityListBox = New System.Windows.Forms.ListBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.ExecuteButton = New System.Windows.Forms.Button
        Me.ScenarioListBox = New System.Windows.Forms.ListBox
        Me.TuneGroupBox = New System.Windows.Forms.GroupBox
        Me.TimeSlotBox = New System.Windows.Forms.NumericUpDown
        Me.Label15 = New System.Windows.Forms.Label
        Me.CPUCount = New System.Windows.Forms.NumericUpDown
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.SelectedPolicyBox = New System.Windows.Forms.ComboBox
        Me.DefaultBtn = New System.Windows.Forms.Button
        Me.QueueSizeBox = New System.Windows.Forms.NumericUpDown
        Me.PackSizeBox = New System.Windows.Forms.NumericUpDown
        Me.StallTimeBox = New System.Windows.Forms.NumericUpDown
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.ScriptButton = New System.Windows.Forms.Button
        Me.LoadGroupBox.SuspendLayout()
        Me.ExecutionGroupBox.SuspendLayout()
        Me.TuneGroupBox.SuspendLayout()
        CType(Me.TimeSlotBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CPUCount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.QueueSizeBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PackSizeBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StallTimeBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LoadGroupBox
        '
        Me.LoadGroupBox.Controls.Add(Me.FileComboBox)
        Me.LoadGroupBox.Controls.Add(Me.LoadButton)
        Me.LoadGroupBox.Location = New System.Drawing.Point(12, 12)
        Me.LoadGroupBox.Name = "LoadGroupBox"
        Me.LoadGroupBox.Size = New System.Drawing.Size(215, 133)
        Me.LoadGroupBox.TabIndex = 0
        Me.LoadGroupBox.TabStop = False
        Me.LoadGroupBox.Text = "Loading Area"
        '
        'FileComboBox
        '
        Me.FileComboBox.FormattingEnabled = True
        Me.FileComboBox.Location = New System.Drawing.Point(22, 31)
        Me.FileComboBox.Name = "FileComboBox"
        Me.FileComboBox.Size = New System.Drawing.Size(121, 21)
        Me.FileComboBox.TabIndex = 2
        '
        'LoadButton
        '
        Me.LoadButton.Location = New System.Drawing.Point(45, 67)
        Me.LoadButton.Name = "LoadButton"
        Me.LoadButton.Size = New System.Drawing.Size(75, 23)
        Me.LoadButton.TabIndex = 3
        Me.LoadButton.Text = "Load"
        Me.LoadButton.UseVisualStyleBackColor = True
        '
        'ExecutionGroupBox
        '
        Me.ExecutionGroupBox.Controls.Add(Me.ScriptButton)
        Me.ExecutionGroupBox.Controls.Add(Me.ClearCache)
        Me.ExecutionGroupBox.Controls.Add(Me.TblSemantics)
        Me.ExecutionGroupBox.Controls.Add(Me.Label13)
        Me.ExecutionGroupBox.Controls.Add(Me.SourceField)
        Me.ExecutionGroupBox.Controls.Add(Me.Label12)
        Me.ExecutionGroupBox.Controls.Add(Me.ActSemanticsField)
        Me.ExecutionGroupBox.Controls.Add(Me.Label11)
        Me.ExecutionGroupBox.Controls.Add(Me.PolicyField)
        Me.ExecutionGroupBox.Controls.Add(Me.Label10)
        Me.ExecutionGroupBox.Controls.Add(Me.TypeField)
        Me.ExecutionGroupBox.Controls.Add(Me.Label8)
        Me.ExecutionGroupBox.Controls.Add(Me.RecordSetListBox)
        Me.ExecutionGroupBox.Controls.Add(Me.ActivityListBox)
        Me.ExecutionGroupBox.Controls.Add(Me.Label7)
        Me.ExecutionGroupBox.Controls.Add(Me.Label4)
        Me.ExecutionGroupBox.Controls.Add(Me.ExecuteButton)
        Me.ExecutionGroupBox.Controls.Add(Me.ScenarioListBox)
        Me.ExecutionGroupBox.Location = New System.Drawing.Point(233, 12)
        Me.ExecutionGroupBox.Name = "ExecutionGroupBox"
        Me.ExecutionGroupBox.Size = New System.Drawing.Size(547, 472)
        Me.ExecutionGroupBox.TabIndex = 1
        Me.ExecutionGroupBox.TabStop = False
        Me.ExecutionGroupBox.Text = "Execution Area"
        '
        'ClearCache
        '
        Me.ClearCache.Location = New System.Drawing.Point(9, 171)
        Me.ClearCache.Name = "ClearCache"
        Me.ClearCache.Size = New System.Drawing.Size(75, 23)
        Me.ClearCache.TabIndex = 28
        Me.ClearCache.Text = "Clear Cache"
        Me.ClearCache.UseVisualStyleBackColor = True
        '
        'TblSemantics
        '
        Me.TblSemantics.BackColor = System.Drawing.Color.AliceBlue
        Me.TblSemantics.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TblSemantics.Location = New System.Drawing.Point(249, 383)
        Me.TblSemantics.Name = "TblSemantics"
        Me.TblSemantics.Size = New System.Drawing.Size(292, 67)
        Me.TblSemantics.TabIndex = 27
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.Label13.Location = New System.Drawing.Point(162, 383)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(81, 16)
        Me.Label13.TabIndex = 26
        Me.Label13.Text = "Semantics:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'SourceField
        '
        Me.SourceField.BackColor = System.Drawing.Color.AliceBlue
        Me.SourceField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SourceField.Location = New System.Drawing.Point(249, 359)
        Me.SourceField.Name = "SourceField"
        Me.SourceField.Size = New System.Drawing.Size(292, 16)
        Me.SourceField.TabIndex = 25
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.Label12.Location = New System.Drawing.Point(162, 359)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(81, 16)
        Me.Label12.TabIndex = 24
        Me.Label12.Text = "Source:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ActSemanticsField
        '
        Me.ActSemanticsField.BackColor = System.Drawing.Color.AliceBlue
        Me.ActSemanticsField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ActSemanticsField.Location = New System.Drawing.Point(249, 289)
        Me.ActSemanticsField.Name = "ActSemanticsField"
        Me.ActSemanticsField.Size = New System.Drawing.Size(292, 67)
        Me.ActSemanticsField.TabIndex = 22
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.Label11.Location = New System.Drawing.Point(162, 289)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(81, 16)
        Me.Label11.TabIndex = 20
        Me.Label11.Text = "Semantics:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PolicyField
        '
        Me.PolicyField.BackColor = System.Drawing.Color.AliceBlue
        Me.PolicyField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PolicyField.Location = New System.Drawing.Point(249, 265)
        Me.PolicyField.Name = "PolicyField"
        Me.PolicyField.Size = New System.Drawing.Size(292, 16)
        Me.PolicyField.TabIndex = 19
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.Label10.Location = New System.Drawing.Point(162, 265)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(81, 16)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "Policy:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TypeField
        '
        Me.TypeField.BackColor = System.Drawing.Color.AliceBlue
        Me.TypeField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TypeField.Location = New System.Drawing.Point(249, 241)
        Me.TypeField.Name = "TypeField"
        Me.TypeField.Size = New System.Drawing.Size(292, 16)
        Me.TypeField.TabIndex = 17
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.Label8.Location = New System.Drawing.Point(162, 241)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(81, 16)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Type:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'RecordSetListBox
        '
        Me.RecordSetListBox.Location = New System.Drawing.Point(9, 359)
        Me.RecordSetListBox.Name = "RecordSetListBox"
        Me.RecordSetListBox.Size = New System.Drawing.Size(154, 82)
        Me.RecordSetListBox.TabIndex = 23
        '
        'ActivityListBox
        '
        Me.ActivityListBox.Location = New System.Drawing.Point(9, 241)
        Me.ActivityListBox.Name = "ActivityListBox"
        Me.ActivityListBox.Size = New System.Drawing.Size(154, 95)
        Me.ActivityListBox.TabIndex = 21
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 340)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(64, 16)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "RecordSets"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 222)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 16)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Activities"
        '
        'ExecuteButton
        '
        Me.ExecuteButton.Location = New System.Drawing.Point(88, 171)
        Me.ExecuteButton.Name = "ExecuteButton"
        Me.ExecuteButton.Size = New System.Drawing.Size(75, 23)
        Me.ExecuteButton.TabIndex = 1
        Me.ExecuteButton.Text = "Execute"
        Me.ExecuteButton.UseVisualStyleBackColor = True
        '
        'ScenarioListBox
        '
        Me.ScenarioListBox.FormattingEnabled = True
        Me.ScenarioListBox.Location = New System.Drawing.Point(9, 31)
        Me.ScenarioListBox.Name = "ScenarioListBox"
        Me.ScenarioListBox.Size = New System.Drawing.Size(154, 134)
        Me.ScenarioListBox.TabIndex = 0
        '
        'TuneGroupBox
        '
        Me.TuneGroupBox.Controls.Add(Me.TimeSlotBox)
        Me.TuneGroupBox.Controls.Add(Me.Label15)
        Me.TuneGroupBox.Controls.Add(Me.CPUCount)
        Me.TuneGroupBox.Controls.Add(Me.Label1)
        Me.TuneGroupBox.Controls.Add(Me.Label14)
        Me.TuneGroupBox.Controls.Add(Me.SelectedPolicyBox)
        Me.TuneGroupBox.Controls.Add(Me.DefaultBtn)
        Me.TuneGroupBox.Controls.Add(Me.QueueSizeBox)
        Me.TuneGroupBox.Controls.Add(Me.PackSizeBox)
        Me.TuneGroupBox.Controls.Add(Me.StallTimeBox)
        Me.TuneGroupBox.Controls.Add(Me.Label6)
        Me.TuneGroupBox.Controls.Add(Me.Label5)
        Me.TuneGroupBox.Controls.Add(Me.Label2)
        Me.TuneGroupBox.Location = New System.Drawing.Point(12, 151)
        Me.TuneGroupBox.Name = "TuneGroupBox"
        Me.TuneGroupBox.Size = New System.Drawing.Size(215, 333)
        Me.TuneGroupBox.TabIndex = 2
        Me.TuneGroupBox.TabStop = False
        Me.TuneGroupBox.Text = "Tuning Parameters"
        '
        'TimeSlotBox
        '
        Me.TimeSlotBox.Location = New System.Drawing.Point(149, 58)
        Me.TimeSlotBox.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.TimeSlotBox.Name = "TimeSlotBox"
        Me.TimeSlotBox.Size = New System.Drawing.Size(52, 20)
        Me.TimeSlotBox.TabIndex = 39
        Me.TimeSlotBox.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(8, 58)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(135, 20)
        Me.Label15.TabIndex = 38
        Me.Label15.Text = "Time slot (msec)"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CPUCount
        '
        Me.CPUCount.Location = New System.Drawing.Point(149, 32)
        Me.CPUCount.Maximum = New Decimal(New Integer() {16, 0, 0, 0})
        Me.CPUCount.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.CPUCount.Name = "CPUCount"
        Me.CPUCount.Size = New System.Drawing.Size(52, 20)
        Me.CPUCount.TabIndex = 33
        Me.CPUCount.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(135, 20)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Number of CPUs"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(66, 164)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(132, 20)
        Me.Label14.TabIndex = 31
        Me.Label14.Text = "Select scheduling policy"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'SelectedPolicyBox
        '
        Me.SelectedPolicyBox.FormattingEnabled = True
        Me.SelectedPolicyBox.Items.AddRange(New Object() {"Round Robin (default)", "Minimum Cost", "Minimum Memory"})
        Me.SelectedPolicyBox.Location = New System.Drawing.Point(69, 189)
        Me.SelectedPolicyBox.Name = "SelectedPolicyBox"
        Me.SelectedPolicyBox.Size = New System.Drawing.Size(132, 21)
        Me.SelectedPolicyBox.TabIndex = 30
        Me.SelectedPolicyBox.Text = "Round Robin (default)"
        '
        'DefaultBtn
        '
        Me.DefaultBtn.BackColor = System.Drawing.SystemColors.Control
        Me.DefaultBtn.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.DefaultBtn.Location = New System.Drawing.Point(8, 159)
        Me.DefaultBtn.Name = "DefaultBtn"
        Me.DefaultBtn.Size = New System.Drawing.Size(55, 51)
        Me.DefaultBtn.TabIndex = 27
        Me.DefaultBtn.Text = "Apply default values"
        Me.DefaultBtn.UseVisualStyleBackColor = False
        '
        'QueueSizeBox
        '
        Me.QueueSizeBox.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.QueueSizeBox.Location = New System.Drawing.Point(149, 136)
        Me.QueueSizeBox.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.QueueSizeBox.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.QueueSizeBox.Name = "QueueSizeBox"
        Me.QueueSizeBox.Size = New System.Drawing.Size(52, 20)
        Me.QueueSizeBox.TabIndex = 25
        Me.QueueSizeBox.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'PackSizeBox
        '
        Me.PackSizeBox.Increment = New Decimal(New Integer() {50, 0, 0, 0})
        Me.PackSizeBox.Location = New System.Drawing.Point(149, 110)
        Me.PackSizeBox.Maximum = New Decimal(New Integer() {2000, 0, 0, 0})
        Me.PackSizeBox.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.PackSizeBox.Name = "PackSizeBox"
        Me.PackSizeBox.Size = New System.Drawing.Size(52, 20)
        Me.PackSizeBox.TabIndex = 24
        Me.PackSizeBox.Value = New Decimal(New Integer() {500, 0, 0, 0})
        '
        'StallTimeBox
        '
        Me.StallTimeBox.Location = New System.Drawing.Point(149, 84)
        Me.StallTimeBox.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.StallTimeBox.Name = "StallTimeBox"
        Me.StallTimeBox.Size = New System.Drawing.Size(52, 20)
        Me.StallTimeBox.TabIndex = 20
        Me.StallTimeBox.Value = New Decimal(New Integer() {20, 0, 0, 0})
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(8, 136)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(135, 20)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "DataQueue Size (#RPs)"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(8, 110)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(135, 20)
        Me.Label5.TabIndex = 26
        Me.Label5.Text = "RowPack Size (#Tuples)"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(135, 20)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "Stall time (msec)"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ScriptButton
        '
        Me.ScriptButton.Location = New System.Drawing.Point(88, 200)
        Me.ScriptButton.Name = "ScriptButton"
        Me.ScriptButton.Size = New System.Drawing.Size(75, 23)
        Me.ScriptButton.TabIndex = 29
        Me.ScriptButton.Text = "Run script"
        Me.ScriptButton.UseVisualStyleBackColor = True
        '
        'LoadForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(792, 496)
        Me.Controls.Add(Me.TuneGroupBox)
        Me.Controls.Add(Me.ExecutionGroupBox)
        Me.Controls.Add(Me.LoadGroupBox)
        Me.Name = "LoadForm"
        Me.Text = "Loading ETL Scenarios"
        Me.LoadGroupBox.ResumeLayout(False)
        Me.ExecutionGroupBox.ResumeLayout(False)
        Me.TuneGroupBox.ResumeLayout(False)
        CType(Me.TimeSlotBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CPUCount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.QueueSizeBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PackSizeBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StallTimeBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LoadGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents ExecutionGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents LoadButton As System.Windows.Forms.Button
    Friend WithEvents ExecuteButton As System.Windows.Forms.Button
    Friend WithEvents ScenarioListBox As System.Windows.Forms.ListBox
    Friend WithEvents FileComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents TuneGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents SelectedPolicyBox As System.Windows.Forms.ComboBox
    Friend WithEvents DefaultBtn As System.Windows.Forms.Button
    Friend WithEvents QueueSizeBox As System.Windows.Forms.NumericUpDown
    Friend WithEvents PackSizeBox As System.Windows.Forms.NumericUpDown
    Friend WithEvents StallTimeBox As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TblSemantics As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents SourceField As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents ActSemanticsField As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents PolicyField As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TypeField As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents RecordSetListBox As System.Windows.Forms.ListBox
    Friend WithEvents ActivityListBox As System.Windows.Forms.ListBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ClearCache As System.Windows.Forms.Button
    Friend WithEvents CPUCount As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TimeSlotBox As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents ScriptButton As System.Windows.Forms.Button
End Class
