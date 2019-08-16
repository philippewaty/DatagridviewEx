<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
  Inherits System.Windows.Forms.Form

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()>
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
  <System.Diagnostics.DebuggerStepThrough()>
  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
    Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    Dim ExportSettings1 As DataGridViewEx.ExportSettings = New DataGridViewEx.ExportSettings()
    Me.btnExportCSV = New System.Windows.Forms.Button()
    Me.btnExportXLSX = New System.Windows.Forms.Button()
    Me.btnPrint = New System.Windows.Forms.Button()
    Me.btnGetConfig = New System.Windows.Forms.Button()
    Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
    Me.btnSetConfig = New System.Windows.Forms.Button()
    Me.PictureBox1 = New System.Windows.Forms.PictureBox()
    Me.PictureBox2 = New System.Windows.Forms.PictureBox()
    Me.DataGridViewEx1 = New DataGridViewEx.DataGridViewEx(Me.components)
    Me.ContextMenuStrip1.SuspendLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.DataGridViewEx1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'btnExportCSV
    '
    Me.btnExportCSV.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnExportCSV.Location = New System.Drawing.Point(652, 10)
    Me.btnExportCSV.Margin = New System.Windows.Forms.Padding(2)
    Me.btnExportCSV.Name = "btnExportCSV"
    Me.btnExportCSV.Size = New System.Drawing.Size(79, 24)
    Me.btnExportCSV.TabIndex = 1
    Me.btnExportCSV.Text = "Export CSV"
    Me.btnExportCSV.UseVisualStyleBackColor = True
    '
    'btnExportXLSX
    '
    Me.btnExportXLSX.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnExportXLSX.Location = New System.Drawing.Point(652, 38)
    Me.btnExportXLSX.Margin = New System.Windows.Forms.Padding(2)
    Me.btnExportXLSX.Name = "btnExportXLSX"
    Me.btnExportXLSX.Size = New System.Drawing.Size(79, 24)
    Me.btnExportXLSX.TabIndex = 2
    Me.btnExportXLSX.Text = "Export XLSX"
    Me.btnExportXLSX.UseVisualStyleBackColor = True
    '
    'btnPrint
    '
    Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnPrint.Location = New System.Drawing.Point(652, 67)
    Me.btnPrint.Margin = New System.Windows.Forms.Padding(2)
    Me.btnPrint.Name = "btnPrint"
    Me.btnPrint.Size = New System.Drawing.Size(79, 24)
    Me.btnPrint.TabIndex = 3
    Me.btnPrint.Text = "Print"
    Me.btnPrint.UseVisualStyleBackColor = True
    '
    'btnGetConfig
    '
    Me.btnGetConfig.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnGetConfig.Location = New System.Drawing.Point(652, 96)
    Me.btnGetConfig.Name = "btnGetConfig"
    Me.btnGetConfig.Size = New System.Drawing.Size(79, 24)
    Me.btnGetConfig.TabIndex = 4
    Me.btnGetConfig.Text = "Save config"
    Me.btnGetConfig.UseVisualStyleBackColor = True
    '
    'ContextMenuStrip1
    '
    Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ToolStripMenuItem2, Me.ToolStripMenuItem3})
    Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
    Me.ContextMenuStrip1.Size = New System.Drawing.Size(183, 70)
    '
    'ToolStripMenuItem1
    '
    Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
    Me.ToolStripMenuItem1.Size = New System.Drawing.Size(182, 22)
    Me.ToolStripMenuItem1.Text = "ToolStripMenuItem1"
    '
    'ToolStripMenuItem2
    '
    Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
    Me.ToolStripMenuItem2.Size = New System.Drawing.Size(182, 22)
    Me.ToolStripMenuItem2.Text = "ToolStripMenuItem2"
    '
    'ToolStripMenuItem3
    '
    Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
    Me.ToolStripMenuItem3.Size = New System.Drawing.Size(182, 22)
    Me.ToolStripMenuItem3.Text = "ToolStripMenuItem3"
    '
    'btnSetConfig
    '
    Me.btnSetConfig.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnSetConfig.Location = New System.Drawing.Point(652, 126)
    Me.btnSetConfig.Name = "btnSetConfig"
    Me.btnSetConfig.Size = New System.Drawing.Size(79, 24)
    Me.btnSetConfig.TabIndex = 5
    Me.btnSetConfig.Text = "Read config"
    Me.btnSetConfig.UseVisualStyleBackColor = True
    '
    'PictureBox1
    '
    Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
    Me.PictureBox1.Location = New System.Drawing.Point(670, 198)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(38, 36)
    Me.PictureBox1.TabIndex = 6
    Me.PictureBox1.TabStop = False
    Me.PictureBox1.Visible = False
    '
    'PictureBox2
    '
    Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
    Me.PictureBox2.Location = New System.Drawing.Point(666, 249)
    Me.PictureBox2.Name = "PictureBox2"
    Me.PictureBox2.Size = New System.Drawing.Size(26, 17)
    Me.PictureBox2.TabIndex = 7
    Me.PictureBox2.TabStop = False
    Me.PictureBox2.Visible = False
    '
    'DataGridViewEx1
    '
    Me.DataGridViewEx1.AllowUserToAddRows = False
    Me.DataGridViewEx1.AllowUserToDeleteRows = False
    Me.DataGridViewEx1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
    DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
    DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
    DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.DataGridViewEx1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
    Me.DataGridViewEx1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridViewEx1.Configuration = "<?xml version=""1.0"" encoding=""utf-16""?>" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "<ArrayOfColumnInfo xmlns:xsi=""http://www" &
    ".w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" />" &
    ""
    Me.DataGridViewEx1.ContextMenuStripMergeMode = DataGridViewEx.DataGridViewEx.ContextMenuStripMergingEnum.InsideMenuOnly
    Me.DataGridViewEx1.CSVFilename = "test.csv"
    DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
    DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
    DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
    DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.DataGridViewEx1.DefaultCellStyle = DataGridViewCellStyle2
    ExportSettings1.ProgressBarValue = False
    Me.DataGridViewEx1.ExportSettings = ExportSettings1
    Me.DataGridViewEx1.HTMLFilename = "export.html"
    Me.DataGridViewEx1.Location = New System.Drawing.Point(9, 10)
    Me.DataGridViewEx1.Margin = New System.Windows.Forms.Padding(2)
    Me.DataGridViewEx1.Name = "DataGridViewEx1"
    Me.DataGridViewEx1.PrintDate = False
    Me.DataGridViewEx1.PrintTitle = Nothing
    Me.DataGridViewEx1.RowTemplate.Height = 24
    Me.DataGridViewEx1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
    Me.DataGridViewEx1.Size = New System.Drawing.Size(638, 297)
    Me.DataGridViewEx1.TabIndex = 0
    Me.DataGridViewEx1.XLSXFilename = Nothing
    '
    'Form1
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(740, 317)
    Me.Controls.Add(Me.PictureBox2)
    Me.Controls.Add(Me.PictureBox1)
    Me.Controls.Add(Me.btnSetConfig)
    Me.Controls.Add(Me.btnGetConfig)
    Me.Controls.Add(Me.btnPrint)
    Me.Controls.Add(Me.DataGridViewEx1)
    Me.Controls.Add(Me.btnExportXLSX)
    Me.Controls.Add(Me.btnExportCSV)
    Me.Margin = New System.Windows.Forms.Padding(2)
    Me.Name = "Form1"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Form1"
    Me.ContextMenuStrip1.ResumeLayout(False)
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.DataGridViewEx1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents btnExportCSV As System.Windows.Forms.Button
  Friend WithEvents btnExportXLSX As System.Windows.Forms.Button
  Friend WithEvents DataGridViewEx1 As DataGridViewEx.DataGridViewEx
  Friend WithEvents btnPrint As System.Windows.Forms.Button
  Friend WithEvents btnGetConfig As Button
  Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
  Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem2 As ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem3 As ToolStripMenuItem
  Friend WithEvents btnSetConfig As Button
  Friend WithEvents PictureBox1 As PictureBox
  Friend WithEvents PictureBox2 As PictureBox
End Class
