<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormColumnsConfig
  Inherits System.Windows.Forms.Form

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormColumnsConfig))
    Me.lstColumns = New System.Windows.Forms.CheckedListBox()
    Me.btnUp = New System.Windows.Forms.Button()
    Me.btnDown = New System.Windows.Forms.Button()
    Me.btnClose = New System.Windows.Forms.Button()
    Me.btnRestore = New System.Windows.Forms.Button()
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.SuspendLayout()
    '
    'lstColumns
    '
    Me.lstColumns.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lstColumns.FormattingEnabled = True
    Me.lstColumns.Location = New System.Drawing.Point(12, 12)
    Me.lstColumns.Name = "lstColumns"
    Me.lstColumns.Size = New System.Drawing.Size(225, 214)
    Me.lstColumns.TabIndex = 0
    '
    'btnUp
    '
    Me.btnUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnUp.Image = Global.DataGridViewEx.My.Resources.Resources.up
    Me.btnUp.Location = New System.Drawing.Point(243, 12)
    Me.btnUp.Name = "btnUp"
    Me.btnUp.Size = New System.Drawing.Size(27, 27)
    Me.btnUp.TabIndex = 1
    Me.ToolTip1.SetToolTip(Me.btnUp, "Move up")
    Me.btnUp.UseVisualStyleBackColor = True
    '
    'btnDown
    '
    Me.btnDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnDown.Image = Global.DataGridViewEx.My.Resources.Resources.down
    Me.btnDown.Location = New System.Drawing.Point(243, 45)
    Me.btnDown.Name = "btnDown"
    Me.btnDown.Size = New System.Drawing.Size(27, 27)
    Me.btnDown.TabIndex = 2
    Me.ToolTip1.SetToolTip(Me.btnDown, "Move down")
    Me.btnDown.UseVisualStyleBackColor = True
    '
    'btnClose
    '
    Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.btnClose.Image = Global.DataGridViewEx.My.Resources.Resources.fileclose
    Me.btnClose.Location = New System.Drawing.Point(243, 199)
    Me.btnClose.Name = "btnClose"
    Me.btnClose.Size = New System.Drawing.Size(27, 27)
    Me.btnClose.TabIndex = 4
    Me.btnClose.UseVisualStyleBackColor = True
    '
    'btnRestore
    '
    Me.btnRestore.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnRestore.Image = Global.DataGridViewEx.My.Resources.Resources.recur
    Me.btnRestore.Location = New System.Drawing.Point(243, 78)
    Me.btnRestore.Name = "btnRestore"
    Me.btnRestore.Size = New System.Drawing.Size(27, 27)
    Me.btnRestore.TabIndex = 3
    Me.ToolTip1.SetToolTip(Me.btnRestore, "Restore order and visibility")
    Me.btnRestore.UseVisualStyleBackColor = True
    '
    'FormColumnsConfig
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
    Me.CancelButton = Me.btnClose
    Me.ClientSize = New System.Drawing.Size(282, 232)
    Me.Controls.Add(Me.btnRestore)
    Me.Controls.Add(Me.btnClose)
    Me.Controls.Add(Me.btnDown)
    Me.Controls.Add(Me.btnUp)
    Me.Controls.Add(Me.lstColumns)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "FormColumnsConfig"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Columns configuration"
    Me.ResumeLayout(False)

  End Sub

  Friend WithEvents lstColumns As CheckedListBox
  Friend WithEvents btnUp As Button
  Friend WithEvents btnDown As Button
  Friend WithEvents btnClose As Button
  Friend WithEvents btnRestore As Button
  Friend WithEvents ToolTip1 As ToolTip
End Class
