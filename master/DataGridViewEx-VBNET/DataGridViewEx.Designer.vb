Partial Class DataGridViewEx
  Inherits System.Windows.Forms.DataGridView

  <System.Diagnostics.DebuggerNonUserCode()>
  Public Sub New(ByVal container As System.ComponentModel.IContainer)
    MyClass.New()

    'Required for Windows.Forms Class Composition Designer support
    If (container IsNot Nothing) Then
      container.Add(Me)
    End If

  End Sub

  <System.Diagnostics.DebuggerNonUserCode()>
  Public Sub New()
    MyBase.New()

    'This call is required by the Component Designer.
    InitializeComponent()
    'DoubleBuffered = True
    Me.GetType.InvokeMember("DoubleBuffered", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.SetProperty, Nothing, Me, New Object() {True})
    _ExportSettings = New ExportSettings()
  End Sub

  'Component overrides dispose to clean up the component list.
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

  'Required by the Component Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Component Designer
  'It can be modified using the Component Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()>
  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DataGridViewEx))
    Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
    Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
    Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog()
    Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuExport = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
    Me.mnuPrint = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuPrintPreview = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuExportCSV = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuExportExcel = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuColumnSettings = New System.Windows.Forms.ToolStripMenuItem()
    Me.mnuExportHTML = New System.Windows.Forms.ToolStripMenuItem()
    Me.ContextMenuStrip1.SuspendLayout()
    CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'PrintDialog1
    '
    Me.PrintDialog1.UseEXDialog = True
    '
    'PrintDocument1
    '
    '
    'ContextMenuStrip1
    '
    Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPrint, Me.mnuPrintPreview, Me.ToolStripSeparator1, Me.mnuExport, Me.ToolStripSeparator2, Me.mnuColumnSettings})
    Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
    Me.ContextMenuStrip1.Size = New System.Drawing.Size(171, 104)
    '
    'ToolStripSeparator1
    '
    Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
    Me.ToolStripSeparator1.Size = New System.Drawing.Size(167, 6)
    '
    'mnuExport
    '
    Me.mnuExport.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuExportCSV, Me.mnuExportExcel, Me.mnuExportHTML})
    Me.mnuExport.Name = "mnuExport"
    Me.mnuExport.Size = New System.Drawing.Size(170, 22)
    Me.mnuExport.Text = "Export"
    '
    'ToolStripSeparator2
    '
    Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
    Me.ToolStripSeparator2.Size = New System.Drawing.Size(167, 6)
    '
    'mnuPrint
    '
    Me.mnuPrint.Image = Global.DataGridViewExVBNET.My.Resources.Resources.fileprint
    Me.mnuPrint.Name = "mnuPrint"
    Me.mnuPrint.Size = New System.Drawing.Size(170, 22)
    Me.mnuPrint.Text = "Print"
    '
    'mnuPrintPreview
    '
    Me.mnuPrintPreview.Image = Global.DataGridViewExVBNET.My.Resources.Resources.document_preview
    Me.mnuPrintPreview.Name = "mnuPrintPreview"
    Me.mnuPrintPreview.Size = New System.Drawing.Size(170, 22)
    Me.mnuPrintPreview.Text = "Print preview"
    '
    'mnuExportCSV
    '
    Me.mnuExportCSV.Image = Global.DataGridViewExVBNET.My.Resources.Resources.page_excel
    Me.mnuExportCSV.Name = "mnuExportCSV"
    Me.mnuExportCSV.Size = New System.Drawing.Size(116, 22)
    Me.mnuExportCSV.Text = "CSV..."
    '
    'mnuExportExcel
    '
    Me.mnuExportExcel.Image = Global.DataGridViewExVBNET.My.Resources.Resources.page_excel
    Me.mnuExportExcel.Name = "mnuExportExcel"
    Me.mnuExportExcel.Size = New System.Drawing.Size(116, 22)
    Me.mnuExportExcel.Text = "Excel..."
    '
    'mnuColumnSettings
    '
    Me.mnuColumnSettings.Image = Global.DataGridViewExVBNET.My.Resources.Resources.package_settings
    Me.mnuColumnSettings.Name = "mnuColumnSettings"
    Me.mnuColumnSettings.Size = New System.Drawing.Size(170, 22)
    Me.mnuColumnSettings.Text = "Column settings..."
    '
    'mnuExportHTML
    '
    Me.mnuExportHTML.Image = CType(resources.GetObject("mnuExportHTML.Image"), System.Drawing.Image)
    Me.mnuExportHTML.Name = "mnuExportHTML"
    Me.mnuExportHTML.Size = New System.Drawing.Size(116, 22)
    Me.mnuExportHTML.Text = "HTML..."
    '
    'DataGridViewEx
    '
    Me.AllowUserToAddRows = False
    Me.AllowUserToDeleteRows = False
    Me.RowTemplate.Height = 24
    Me.ContextMenuStrip1.ResumeLayout(False)
    CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
  Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
  Friend WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
  Friend WithEvents mnuPrint As ToolStripMenuItem
  Friend WithEvents mnuPrintPreview As ToolStripMenuItem
  Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
  Friend WithEvents mnuExport As ToolStripMenuItem
  Friend WithEvents mnuExportExcel As ToolStripMenuItem
  Friend WithEvents mnuExportCSV As ToolStripMenuItem
  Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
  Friend WithEvents mnuColumnSettings As ToolStripMenuItem
  Private WithEvents ContextMenuStrip1 As ContextMenuStrip
  Friend WithEvents mnuExportHTML As ToolStripMenuItem
End Class
