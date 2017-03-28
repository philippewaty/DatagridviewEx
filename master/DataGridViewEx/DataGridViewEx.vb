Option Strict Off

Imports System.ComponentModel
Imports System.IO
Imports System.Reflection
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports System.Xml.Serialization

''' <summary>
''' Datagridview enhanced
''' </summary>
''' <Author>Philippe Waty</Author>
''' <history>
''' V1.2.1 (07/03/2016)
'''   - Deleted display line number whene row header is not visible
''' V1.2 (28/01/2016)
'''   - Correction in print preview : hidden columns are not printed now.
''' V1.1.1
'''   - Better print preview
'''   - Update to EPPlus 4.0.5
''' </history>
Public Class DataGridViewEx
  Inherits DataGridView

#Region "Enum"

  ''' <summary>
  ''' Indicate how the external ContextMenuStrip will be merged with the internal
  ''' </summary>
  Public Enum ContextMenuStripMergingEnum
    InsideMenuOnly = 0
    ExternalMenuOnly
    InsideMenuFirst
    ExternalMenuFirst
  End Enum

#End Region

#Region "Properties"

  ''' <summary>
  ''' Indicates if the user has right clicked and that the menu has been merged or not.
  ''' </summary>
  Private _ContextMenuMerged As Boolean = False

  Private _DisplayLinesNumber As Boolean = True
  ''' <summary>
  ''' Display lines number
  ''' </summary>
  ''' <returns></returns>
  <Description("Display lines number"), Category("Design"), DefaultValue(True)>
  Public Property DisplayLinesNumber() As Boolean
    Get
      Return _DisplayLinesNumber
    End Get
    Set(ByVal value As Boolean)
      _DisplayLinesNumber = value
    End Set
  End Property

  Private _ExportToCSVKey As Keys = Keys.F11
  ''' <summary>
  ''' Key used to start export to CSV
  ''' </summary>
  ''' <returns></returns>
  <Description("Key used to start export to CSV"), DefaultValue(Keys.F11)>
  Public Property ExportToCSVKey() As Keys
    Get
      Return _ExportToCSVKey
    End Get
    Set(ByVal value As Keys)
      _ExportToCSVKey = value
    End Set
  End Property

  Private _ExportToXLSXKey As Keys = Keys.F12
  ''' <summary>
  ''' Key used to start export to XLSX
  ''' </summary>
  ''' <returns></returns>
  <Description("Key used to start export to XLSX"), DefaultValue(Keys.F12)>
  Public Property ExportToXLSXKey() As Keys
    Get
      Return _ExportToXLSXKey
    End Get
    Set(ByVal value As Keys)
      _ExportToXLSXKey = value
    End Set
  End Property

  Private _CSVFilename As String
  ''' <summary>
  ''' Filename for exporting to CSV
  ''' </summary>
  ''' <returns></returns>
  <Description("Filename for exporting to CSV"), DefaultValue("")>
  Public Property CSVFilename() As String
    Get
      Return _CSVFilename
    End Get
    Set(ByVal value As String)
      _CSVFilename = value
    End Set
  End Property

  Private _HTMLFilename As String
  ''' <summary>
  ''' Filename for exporting to HTML
  ''' </summary>
  ''' <returns></returns>
  <Description("Filename for exporting to HTML"), DefaultValue("")>
  Public Property HTMLFilename() As String
    Get
      Return _HTMLFilename
    End Get
    Set(ByVal value As String)
      _HTMLFilename = value
    End Set
  End Property

  Private _XLSXFilename As String
  ''' <summary>
  ''' Filename for exporting to XLSX
  ''' </summary>
  ''' <returns></returns>
  <Description("Filename for exporting to XLSX"), DefaultValue("")>
  Public Property XLSXFilename() As String
    Get
      Return _XLSXFilename
    End Get
    Set(ByVal value As String)
      _XLSXFilename = value
    End Set
  End Property

  Private _CSVFieldSeparator As String = ";"
  ''' <summary>
  ''' Field separator for CSV
  ''' </summary>
  ''' <returns></returns>
  <Description("Field separator for CSV"), DefaultValue(";")>
  Public Property CSVFieldSeparator() As String
    Get
      Return _CSVFieldSeparator
    End Get
    Set(ByVal value As String)
      _CSVFieldSeparator = value
    End Set
  End Property

  Private _ExportWriteColumnHeader As Boolean = True
  ''' <summary>
  ''' Write columns name in CSV/XLSX file
  ''' </summary>
  ''' <returns></returns>
  <Description("Write columns name in CSV/XLSX file"), DefaultValue(True)>
  Public Property ExportWriteColumnHeader() As Boolean
    Get
      Return _ExportWriteColumnHeader
    End Get
    Set(ByVal value As Boolean)
      _ExportWriteColumnHeader = value
    End Set
  End Property

  Private _ExportOpenFileAfter As Boolean = True
  ''' <summary>
  ''' Open CSV or XLSX file after export
  ''' </summary>
  ''' <returns></returns>
  <Description("Open CSV or XLSX file after export"), DefaultValue(True)>
  Public Property ExportOpenFileAfter() As Boolean
    Get
      Return _ExportOpenFileAfter
    End Get
    Set(ByVal value As Boolean)
      _ExportOpenFileAfter = value
    End Set
  End Property

  Private _PrintTitle As String
  ''' <summary>
  ''' Title for printing
  ''' </summary>
  ''' <returns></returns>
  <Description("Title for printing"), DefaultValue("")>
  Public Property PrintTitle() As String
    Get
      Return _PrintTitle
    End Get
    Set(ByVal value As String)
      _PrintTitle = value
    End Set
  End Property

  ''' <summary>
  ''' Merge context menu mode
  ''' </summary>
  Private _ContextMenuStripMergeMode As ContextMenuStripMergingEnum
  <Description("Get/set the mode of ContextMenuStrip is merged"), DefaultValue(ContextMenuStripMergingEnum.ExternalMenuFirst)>
  Public Property ContextMenuStripMergeMode() As ContextMenuStripMergingEnum
    Get
      Return _ContextMenuStripMergeMode
    End Get
    Set(ByVal value As ContextMenuStripMergingEnum)
      _ContextMenuStripMergeMode = value
    End Set
  End Property

  Private _PrintDate As Boolean
  ''' <summary>
  ''' Display date on print
  ''' </summary>
  ''' <returns></returns>
  <Description("Display date on print"), DefaultValue(True)>
  Public Property PrintDate() As Boolean
    Get
      Return _PrintDate
    End Get
    Set(ByVal value As Boolean)
      _PrintDate = value
    End Set
  End Property

  ''' <summary>
  ''' XML configuration of DataGridView
  ''' </summary>
  ''' <returns></returns>
  <Browsable(False), Description("XML configuration of DataGridView"), DefaultValue("")>
  Public Property Configuration() As String
    Get
      Dim sb As New System.Text.StringBuilder()

      Try
        Dim columns = New List(Of ColumnInfo)()
        For i As Integer = 0 To Me.Columns.Count - 1
          Dim column As ColumnInfo = New ColumnInfo()
          column.Name = Me.Columns(i).Name
          column.DisplayIndex = Me.Columns(i).DisplayIndex
          column.Width = Me.Columns(i).Width
          column.Visible = Me.Columns(i).Visible
          columns.Add(column)
        Next
        Using writer As TextWriter = New StringWriter(sb)
          Dim xmlSerializer = New XmlSerializer(GetType(List(Of ColumnInfo)))
          xmlSerializer.Serialize(writer, columns)
        End Using

      Catch ex As Exception
        Debug.Print(ex.Message)
      End Try

      Return sb.ToString
    End Get
    Set(ByVal value As String)

      If Me.Columns Is Nothing Then Return

      Try
        Dim columns As New List(Of ColumnInfo)()
        Using reader As TextReader = New StringReader(value)
          Dim xmlSerializer = New XmlSerializer(GetType(List(Of ColumnInfo)))
          columns = DirectCast(xmlSerializer.Deserialize(reader), List(Of ColumnInfo))
        End Using
        For Each column As ColumnInfo In columns
          If Not Me.Columns(column.Name) Is Nothing Then
            Me.Columns(column.Name).DisplayIndex = column.DisplayIndex
            Me.Columns(column.Name).Width = column.Width
            Me.Columns(column.Name).Visible = column.Visible
          End If
        Next

      Catch ex As Exception
        Debug.Print(ex.Message)
      End Try
    End Set
  End Property

  Private _ExportSettings As ExportSettings = New ExportSettings
  <Browsable(True), Category("Misc"), Description("Sets the settings for export and printing the DataGridView")>
  Public Property ExportSettings() As ExportSettings
    Get
      If _ExportSettings Is Nothing Then _ExportSettings = New ExportSettings
      Return _ExportSettings
    End Get
    Set(ByVal value As ExportSettings)
      If _ExportSettings Is Nothing Then _ExportSettings = New ExportSettings
      _ExportSettings = value
    End Set
  End Property
#End Region

#Region "Public methods"

  ''' <summary>
  ''' Clear datagridview lines and columns
  ''' </summary>
  ''' <param name="clearColumns">Indicate if we clear columns</param>
  ''' <remarks></remarks>
  Public Sub Clear(Optional ByVal clearColumns As Boolean = False)
    If clearColumns Then Me.Columns.Clear()
    Me.Rows.Clear()
  End Sub

  ''' <summary>
  ''' Puts/removes readonly on columns from col1 to col2 (-1 pour les 2 = bloc all)
  ''' </summary>
  ''' <param name="value">TRUE to put in readonly, FALSE to remove readonly</param>
  ''' <param name="col1">Start column</param>
  ''' <param name="col2">End column</param>
  ''' <remarks></remarks>
  Public Sub Lock(ByVal value As Boolean, Optional ByVal col1 As Integer = -1, Optional ByVal col2 As Integer = -1)
    If col1 = -1 Then col1 = 0
    If col2 = -1 Then col2 = Me.ColumnCount - 1
    For i As Integer = col1 To col2
      Me.Columns(i).ReadOnly = value
    Next

    'If col1 = -1 And col2 = -1 Then
    '  For i As Integer = 0 To Me.Columns.Count - 1
    '    Me.Columns(i).ReadOnly = value
    '  Next
    'Else
    '  For i As Integer = col1 To col2
    '    Me.Columns(i).ReadOnly = value
    '  Next
    'End If
  End Sub

  ''' <summary>
  ''' Prepare a datagridview with an array for the width and an array for columns name
  ''' </summary>
  ''' <param name="pColWidth">Array for columns width</param>
  ''' <param name="pColName">Array for columns name</param>
  ''' <remarks></remarks>
  <Obsolete()>
  Public Sub PrepareDGV(ByRef pColWidth() As Integer, ByRef pColName() As String)
    Dim index As Integer

    With Me
      .SuspendLayout()
      Me.Clear(True)
      For i As Integer = 0 To pColWidth.Length - 1
        index = .Columns.Add(String.Format("Column{0}", i), pColName(i))
        .Columns(index).Width = pColWidth(i)
      Next
      .ResumeLayout()
    End With
  End Sub

  ''' <summary>
  ''' Merge the internal contextmenuString with the context menu from the application.
  ''' </summary>
  Private Sub ContextMenuStripMerge()
    If _ContextMenuMerged Then Return

    '*** First call to ContextMenuStripMerge method
    Select Case _ContextMenuStripMergeMode
      Case ContextMenuStripMergingEnum.InsideMenuOnly
        Me.ContextMenuStrip = ContextMenuStrip1

      Case ContextMenuStripMergingEnum.InsideMenuFirst
        Dim newCtxMenu As New ContextMenuStrip
        '*** url : http://stackoverflow.com/questions/16760150/transfer-items-between-contextmenustrips-vb-net
        Dim tsi(ContextMenuStrip1.Items.Count - 1) As ToolStripItem

        Me.ContextMenuStrip1.Items.CopyTo(tsi, 0)
        newCtxMenu.Items.AddRange(tsi)
        If Not Me.ContextMenuStrip Is Nothing Then
          '*** Add separator only if there is a ContextMenuStrip
          Me.ContextMenuStrip.Items.Add("-")
        Else
          '*** Initialize ContextMenuStrip
          Me.ContextMenuStrip = New ContextMenuStrip()
        End If
        Array.Resize(Of ToolStripItem)(tsi, Me.ContextMenuStrip.Items.Count)
        Me.ContextMenuStrip.Items.CopyTo(tsi, 0)
        newCtxMenu.Items.AddRange(tsi)
        Me.ContextMenuStrip = newCtxMenu

      Case ContextMenuStripMergingEnum.ExternalMenuOnly
        '*** Do nothing with the internal menu

      Case ContextMenuStripMergingEnum.ExternalMenuFirst
        If Not Me.ContextMenuStrip Is Nothing Then
          '*** Add separator only if there is a ContextMenuStrip
          Me.ContextMenuStrip.Items.Add("-")
        Else
          '*** Initialize ContextMenuStrip
          Me.ContextMenuStrip = New ContextMenuStrip()
        End If
        Dim tsi(ContextMenuStrip1.Items.Count - 1) As ToolStripItem

        Me.ContextMenuStrip1.Items.CopyTo(tsi, 0)
        ContextMenuStrip.Items.AddRange(tsi)
    End Select
    _ContextMenuMerged = True
  End Sub

  Public Function Print(ByVal JobName As String, ByVal Title As String, ByVal displayDate As Boolean, ByVal Preview As Boolean) As Boolean
    'http://www.codeproject.com/Articles/28046/Printing-of-DataGridView
    Dim result As Boolean = False

    PrintTitle = Title
    PrintDisplayDate = displayDate
    'Open the print dialog
    Dim printDialog As New PrintDialog()
    printDialog.Document = PrintDocument1
    printDialog.UseEXDialog = True
    'Get the document
    If Not Preview Then
      If DialogResult.OK = printDialog.ShowDialog() Then
        PrintDocument1.DocumentName = JobName
        PrintDocument1.Print()
        result = True
      End If
    Else
      'Open the print preview dialog
      Dim objPPdialog As New CoolPrintPreviewDialog()
      objPPdialog.Document = PrintDocument1
      objPPdialog.WindowState = FormWindowState.Maximized
      objPPdialog.ShowDialog()
    End If

    Return result
  End Function

#Region " Begin Print Event Handler"
  Dim strFormat As StringFormat
  'Used to format the grid rows.
  Dim arrColumnLefts As New ArrayList()
  'Used to save left coordinates of columns
  Dim arrColumnWidths As New ArrayList()
  'Used to save column widths
  Dim iCellHeight As Integer = 0
  'Used to get/set the datagridview cell height
  Dim iTotalWidth As Integer = 0
  '
  Dim iRow As Integer = 0
  'Used as counter
  Dim bFirstPage As Boolean = False
  'Used to check whether we are printing first page
  Dim bNewPage As Boolean = False
  ' Used to check whether we are printing a new page
  Dim iHeaderHeight As Integer = 0
  'Used for the header height
  'Dim PrintTitle As String = String.Empty
  Dim PrintDisplayDate As Boolean = False

  ''' <summary>
  ''' Handles the begin print event of print document
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub printDocument1_BeginPrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles PrintDocument1.BeginPrint
    Try
      strFormat = New StringFormat()
      strFormat.Alignment = StringAlignment.Near
      strFormat.LineAlignment = StringAlignment.Center
      strFormat.Trimming = StringTrimming.EllipsisCharacter

      arrColumnLefts.Clear()
      arrColumnWidths.Clear()
      iCellHeight = 0
      bFirstPage = True
      bNewPage = True
      iRow = 0
      iHeaderHeight = 0

      ' Calculating Total Widths
      iTotalWidth = 0
      For Each dgvGridCol As DataGridViewColumn In Me.Columns
        If dgvGridCol.Visible Then iTotalWidth += dgvGridCol.Width
      Next
    Catch ex As Exception
      MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
    End Try
  End Sub
#End Region

#Region " Print Page Event"

  ''' <summary>
  ''' Handles the print page event of print document
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub printDocument1_PrintPage(sender As Object, e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
    Try
      'Set the left margin
      Dim iLeftMargin As Integer = e.MarginBounds.Left
      'Set the top margin
      Dim iTopMargin As Integer = e.MarginBounds.Top
      'Whether more pages have to print or not
      Dim bMorePagesToPrint As Boolean = False
      Dim iTmpWidth As Integer = 0

      '*** Get the columns list according to the displayIndex and visibility = true
      Dim columnsList As List(Of DataGridViewColumn) = (From column As DataGridViewColumn In Me.Columns.Cast(Of DataGridViewColumn)()
                                                        Where column.Visible = True
                                                        Order By column.DisplayIndex Select column).ToList

      'For the first page to print set the cell width and header height
      If bFirstPage Then
        For Each GridCol As DataGridViewColumn In columnsList
          iTmpWidth = Math.Floor((GridCol.Width / iTotalWidth) * iTotalWidth * (e.MarginBounds.Width) / iTotalWidth)

          iHeaderHeight = e.Graphics.MeasureString(GridCol.HeaderText, GridCol.InheritedStyle.Font, iTmpWidth).Height + 11

          ' Save width and height of headers
          arrColumnLefts.Add(iLeftMargin)
          arrColumnWidths.Add(iTmpWidth)
          iLeftMargin += iTmpWidth
        Next
      End If
      'Loop till all the grid rows not get printed
      While iRow <= Me.Rows.Count - 1
        Dim GridRow As DataGridViewRow = Me.Rows(iRow)
        'Set the cell height
        iCellHeight = GridRow.Height + 5
        Dim iCount As Integer = 0
        'Check whether the current page settings allows more rows to print
        If iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top Then
          bNewPage = True
          bFirstPage = False
          bMorePagesToPrint = True
          Exit While
        Else
          If bNewPage Then
            'Draw Header
            e.Graphics.DrawString(PrintTitle, New Font(Me.Font, FontStyle.Bold), Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top - e.Graphics.MeasureString("Customer Summary", New Font(Me.Font, FontStyle.Bold), e.MarginBounds.Width).Height - 13)

            If PrintDisplayDate Then
              Dim strDate As [String] = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString()
              'Draw Date
              e.Graphics.DrawString(strDate, New Font(Me.Font, FontStyle.Bold), Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width - e.Graphics.MeasureString(strDate, New Font(Me.Font, FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top - e.Graphics.MeasureString("Customer Summary", New Font(New Font(Me.Font, FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13)
            End If

            'Draw Columns                 
            iTopMargin = e.MarginBounds.Top
            For Each GridCol As DataGridViewColumn In columnsList
              e.Graphics.FillRectangle(New SolidBrush(Color.LightGray), New Rectangle(DirectCast(arrColumnLefts(iCount), Integer), iTopMargin, DirectCast(arrColumnWidths(iCount), Integer), iHeaderHeight))

              e.Graphics.DrawRectangle(Pens.Black, New Rectangle(DirectCast(arrColumnLefts(iCount), Integer), iTopMargin, DirectCast(arrColumnWidths(iCount), Integer), iHeaderHeight))

              e.Graphics.DrawString(GridCol.HeaderText, GridCol.InheritedStyle.Font, New SolidBrush(GridCol.InheritedStyle.ForeColor), New RectangleF(DirectCast(arrColumnLefts(iCount), Integer), iTopMargin, DirectCast(arrColumnWidths(iCount), Integer), iHeaderHeight), strFormat)
              System.Math.Max(System.Threading.Interlocked.Increment(iCount), iCount - 1)
            Next
            bNewPage = False
            iTopMargin += iHeaderHeight
          End If
          iCount = 0
          'Draw Columns Contents                
          For Each Cel As DataGridViewCell In GridRow.Cells
            If Columns(Cel.ColumnIndex).Visible Then
              '*** Draw back color
              e.Graphics.FillRectangle(New SolidBrush(Cel.InheritedStyle.BackColor), New RectangleF(DirectCast(arrColumnLefts(iCount), Integer), iTopMargin, DirectCast(arrColumnWidths(iCount), Integer), iCellHeight))

              If Not (Cel.Value Is Nothing) Then
                If TypeOf (Cel) Is DataGridViewCalendarCell Then
                  '*** Get the date format from the column
                  If String.IsNullOrEmpty(Cel.OwningColumn.DefaultCellStyle.Format) Then
                    e.Graphics.DrawString(Cel.Value.ToString(), Cel.InheritedStyle.Font, New SolidBrush(Cel.InheritedStyle.ForeColor), New RectangleF(DirectCast(arrColumnLefts(iCount), Integer), iTopMargin, DirectCast(arrColumnWidths(iCount), Integer), iCellHeight), strFormat)
                  Else
                    e.Graphics.DrawString(DirectCast(Cel.Value, Date).ToString(Cel.OwningColumn.DefaultCellStyle.Format), Cel.InheritedStyle.Font, New SolidBrush(Cel.InheritedStyle.ForeColor), New RectangleF(DirectCast(arrColumnLefts(iCount), Integer), iTopMargin, DirectCast(arrColumnWidths(iCount), Integer), iCellHeight), strFormat)
                  End If
                ElseIf TypeOf (Cel) Is DataGridViewPasswordTextBoxCell Then
                  e.Graphics.DrawString(Cel.FormattedValue.ToString(), Cel.InheritedStyle.Font, New SolidBrush(Cel.InheritedStyle.ForeColor), New RectangleF(DirectCast(arrColumnLefts(iCount), Integer), iTopMargin, DirectCast(arrColumnWidths(iCount), Integer), iCellHeight), strFormat)
                ElseIf TypeOf (Cel) Is DataGridViewProgressCell Then
                  Dim printImageWidth As Integer = DirectCast(arrColumnWidths(iCount), Integer)
                  Dim printImageHeigth As Integer = Rows(Cel.RowIndex).Height
                  Dim ImgSize As Size = DirectCast(Cel.FormattedValue, Image).Size

                  If printImageWidth > ImgSize.Width Then printImageWidth = ImgSize.Width
                  If printImageHeigth > ImgSize.Height Then printImageHeigth = ImgSize.Height
                  e.Graphics.DrawImage(DirectCast(Cel.FormattedValue, Image),
                              New Rectangle(DirectCast(arrColumnLefts(iCount), Integer),
                              iTopMargin,
                              printImageWidth,
                              printImageHeigth))
                ElseIf TypeOf (Cel) Is DataGridViewImageCell Then
                  '*** http://www.codeproject.com/Articles/16670/DataGridView-Printing-by-Selecting-Columns-and-Row
                  Dim printImageWidth As Integer = DirectCast(arrColumnWidths(iCount), Integer)
                  Dim printImageHeigth As Integer = Rows(Cel.RowIndex).Height
                  Dim ImgSize As Size = DirectCast(Cel.FormattedValue, Image).Size

                  If printImageWidth > ImgSize.Width Then printImageWidth = ImgSize.Width
                  If printImageHeigth > ImgSize.Height Then printImageHeigth = ImgSize.Height
                  e.Graphics.DrawImage(DirectCast(Cel.FormattedValue, Image),
                              New Rectangle(DirectCast(arrColumnLefts(iCount), Integer),
                              iTopMargin,
                              printImageWidth,
                              printImageHeigth))
                Else
                  e.Graphics.DrawString(Cel.Value.ToString(), Cel.InheritedStyle.Font, New SolidBrush(Cel.InheritedStyle.ForeColor), New RectangleF(DirectCast(arrColumnLefts(iCount), Integer), iTopMargin, DirectCast(arrColumnWidths(iCount), Integer), iCellHeight), strFormat)
                End If

                'e.Graphics.DrawString(Cel.Value.ToString(), Cel.InheritedStyle.Font, New SolidBrush(Cel.InheritedStyle.ForeColor), New RectangleF(DirectCast(arrColumnLefts(iCount), Integer), iTopMargin, DirectCast(arrColumnWidths(iCount), Integer), iCellHeight), strFormat)
              End If
              'Drawing Cells Borders 
              e.Graphics.DrawRectangle(Pens.Black, New Rectangle(DirectCast(arrColumnLefts(iCount), Integer), iTopMargin, DirectCast(arrColumnWidths(iCount), Integer), iCellHeight))
              System.Math.Max(System.Threading.Interlocked.Increment(iCount), iCount - 1)
            End If
          Next
        End If
        System.Math.Max(System.Threading.Interlocked.Increment(iRow), iRow - 1)
        iTopMargin += iCellHeight
      End While
      'If more lines exist, print another page.
      If bMorePagesToPrint Then
        e.HasMorePages = True
      Else
        e.HasMorePages = False
      End If
    Catch exc As Exception
      MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
    End Try
  End Sub
#End Region

  ''' <summary>
  ''' Show/hide a column
  ''' </summary>
  ''' <param name="columnIndex">Column index</param>
  ''' <param name="value">TRUE : hide or FALSE : show</param>
  ''' <remarks></remarks>
  Public Sub HideColumn(ByVal columnIndex As Int32, ByVal value As Boolean)
    Me.Columns.Item(columnIndex).Visible = Not value
  End Sub

  'Public Function AddComboBoxColumn(ByVal HeaderText As String, ByVal DisplayStyle As DataGridViewComboBoxDisplayStyle, ByVal ValueType As Type, ByVal Items() As Object, Optional ByVal ColumnIndex As Integer = -1, Optional ByVal DataPropertyName As String = "") As Integer
  Public Function AddComboBoxColumn(ByVal columnName As String, ByVal HeaderText As String, ByVal DisplayStyle As DataGridViewComboBoxDisplayStyle, ByVal Items() As Object, Optional ByVal ColumnIndex As Integer = -1, Optional ByVal DataPropertyName As String = "", Optional ByVal Width As Integer = -1) As Integer
    'http://fikou-sama.freevar.com/mes-articles.php?ID=1
    'https://msdn.microsoft.com/en-us/library/system.windows.forms.datagridviewcolumn.valuetype%28v=vs.110%29.aspx
    Dim col As New DataGridViewComboBoxColumn
    Dim index As Integer = ColumnIndex

    col.Name = columnName
    col.HeaderText = HeaderText
    col.DisplayStyle = DisplayStyle
    If Not Items Is Nothing Then
      col.Items.AddRange(Items)
    End If
    'col.ValueType = ValueType
    col.DataPropertyName = DataPropertyName
    If Width <> -1 Then col.Width = Width
    If ColumnIndex <> -1 Then
      Me.Columns.Insert(ColumnIndex, col)
    Else
      index = Me.Columns.Add(col)
    End If
    Return index
  End Function

  Public Function AddLinkColumn(ByVal columnName As String, ByVal HeaderText As String, Optional ByVal ColumnIndex As Integer = -1, Optional ByVal DataPropertyName As String = "", Optional ByVal Width As Integer = -1) As Integer
    Dim col As New DataGridViewLinkColumn
    Dim index As Integer = ColumnIndex

    col.Name = columnName
    col.HeaderText = "Link me"
    col.DataPropertyName = DataPropertyName
    If Width <> -1 Then col.Width = Width
    If ColumnIndex <> -1 Then
      Me.Columns.Insert(ColumnIndex, col)
    Else
      index = Me.Columns.Add(col)
    End If
    Return index
  End Function

  Public Function AddCheckBoxColumn(ByVal columnName As String, ByVal HeaderText As String, Optional ByVal ColumnIndex As Integer = -1, Optional ByVal DataPropertyName As String = "", Optional ByVal Width As Integer = -1) As Integer
    Dim col As New DataGridViewCheckBoxColumn
    Dim index As Integer = ColumnIndex

    col.Name = columnName
    col.HeaderText = HeaderText
    col.DataPropertyName = DataPropertyName
    If Width <> -1 Then col.Width = Width
    If ColumnIndex <> -1 Then
      Me.Columns.Insert(ColumnIndex, col)
    Else
      index = Me.Columns.Add(col)
    End If
    Return index
  End Function

  Public Function AddButtonColumn(ByVal columnName As String, ByVal HeaderText As String, ByVal ButtonText As String, Optional ByVal ColumnIndex As Integer = -1, Optional ByVal Width As Integer = -1) As Integer
    Dim col As New DataGridViewButtonColumn
    Dim index As Integer = ColumnIndex

    col.Name = columnName
    col.HeaderText = HeaderText
    col.Text = ButtonText
    col.UseColumnTextForButtonValue = True
    If Width <> -1 Then col.Width = Width
    If ColumnIndex <> -1 Then
      Me.Columns.Insert(ColumnIndex, col)
    Else
      index = Me.Columns.Add(col)
    End If
    Return index
  End Function

  Public Function AddCalendarColumn(ByVal columnName As String, ByVal HeaderText As String, Optional ByVal ColumnIndex As Integer = -1, Optional ByVal DataPropertyName As String = "", Optional ByVal Width As Integer = -1) As Integer
    Dim col As New DataGridViewCalendarColumn
    Dim index As Integer = ColumnIndex

    col.Name = columnName
    col.HeaderText = HeaderText
    col.DataPropertyName = DataPropertyName
    If Width <> -1 Then col.Width = Width
    If ColumnIndex <> -1 Then
      Me.Columns.Insert(ColumnIndex, col)
    Else
      index = Me.Columns.Add(col)
    End If
    Return index
  End Function

  Public Function AddImageColumn(ByVal columnName As String, ByVal HeaderText As String, ByVal Description As String, Optional bitmapPadding As Integer = 6, Optional ByVal ColumnIndex As Integer = -1, Optional ByVal DataPropertyName As String = "", Optional ByVal Width As Integer = -1) As Integer
    Dim col As New DataGridViewImageColumn()
    Dim index As Integer = ColumnIndex

    col.Name = columnName
    col.ImageLayout = DataGridViewImageCellLayout.NotSet
    col.Description = Description
    col.HeaderText = HeaderText
    col.DataPropertyName = DataPropertyName
    If Width <> -1 Then col.Width = Width
    If ColumnIndex <> -1 Then
      Me.Columns.Insert(ColumnIndex, col)
    Else
      index = Me.Columns.Add(col)
    End If
    Return index
  End Function

  Public Function AddPasswordColumn(ByVal columnName As String, ByVal HeaderText As String, Optional ByVal ColumnIndex As Integer = -1, Optional ByVal DataPropertyName As String = "", Optional ByVal Width As Integer = -1) As Integer
    Dim col As New DataGridViewPasswordTextBoxColumn
    Dim index As Integer = ColumnIndex

    col.Name = columnName
    col.HeaderText = HeaderText
    col.DataPropertyName = DataPropertyName
    If Width <> -1 Then col.Width = Width
    If ColumnIndex <> -1 Then
      Me.Columns.Insert(ColumnIndex, col)
    Else
      index = Me.Columns.Add(col)
    End If
    Return index
  End Function

  Public Function AddProgressColumn(ByVal columnName As String, ByVal HeaderText As String, Optional ByVal ColumnIndex As Integer = -1, Optional ByVal DataPropertyName As String = "", Optional ByVal Width As Integer = -1) As Integer
    Dim col As New DataGridViewProgressColumn
    Dim index As Integer = ColumnIndex

    col.Name = columnName
    col.HeaderText = HeaderText
    col.DataPropertyName = DataPropertyName
    If Width <> -1 Then col.Width = Width
    If ColumnIndex <> -1 Then
      Me.Columns.Insert(ColumnIndex, col)
    Else
      index = Me.Columns.Add(col)
    End If
    Return index
  End Function

  Public Function AddTextBoxColumn(ByVal columnName As String, ByVal HeaderText As String, Optional ByVal ColumnIndex As Integer = -1, Optional ByVal DataPropertyName As String = "", Optional ByVal Width As Integer = -1) As Integer
    Dim col As New DataGridViewTextBoxColumn()
    Dim index As Integer = ColumnIndex

    col.Name = columnName
    col.HeaderText = HeaderText
    col.DataPropertyName = DataPropertyName
    If Width <> -1 Then col.Width = Width
    If ColumnIndex <> -1 Then
      Me.Columns.Insert(ColumnIndex, col)
    Else
      index = Me.Columns.Add(col)
    End If
    Return index
  End Function

  Public Sub RemoveColumn(ByVal index As Integer)
    Me.Columns.RemoveAt(index)
  End Sub

  ''' <summary>
  ''' Export a DataGridView to CSV file in temporaray folder
  ''' </summary>
  ''' <returns>Empty string if no error otherwise returns error message</returns>
  ''' <remarks></remarks>
  Public Function ExportToCSV() As String
    Return ExportToCSV(_CSVFilename, _ExportWriteColumnHeader, _CSVFieldSeparator)
  End Function

  ''' <summary>
  ''' Export a DataGridView to CSV file
  ''' </summary>
  ''' <param name="CSVFileName">CSV file</param>
  ''' <param name="WriteColumnHeaderNames">Write header</param>
  ''' <param name="DelimiterFormat">Field separator (; or , or ...)</param>
  ''' <returns>Empty string if no error otherwise returns error message</returns>
  ''' <remarks></remarks>
  ''' http://www.daniweb.com/software-development/vbnet/threads/364492/export-datagridview-as-csv
  ''' http://www.developpez.net/forums/d1036098/dotnet/langages/vb-net/export-datagrid-vers-csv-problemes-headers/
  Public Function ExportToCSV(ByVal CSVFileName As String, Optional ByVal WriteColumnHeaderNames As Boolean = False, Optional ByVal DelimiterFormat As String = ";") As String
    Dim sr As StreamWriter = Nothing
    Dim delimiter As String = DelimiterFormat
    Dim columnCount As Integer = Me.Columns.Count - 1
    Dim rowsCount As Integer = Me.RowCount - 1
    Dim rowData As String = ""

    If String.IsNullOrEmpty(CSVFileName) Then
      CSVFileName = System.IO.Path.GetTempFileName.Replace(".tmp", ".csv") 'temp file name , not really sure 100% is unique.
    End If
    '*** Test si adding column is allowed
    '*** If yes, there is an empty line that we doesn't export
    If Me.AllowUserToAddRows Then rowsCount -= 1

    Try
      '*** Get the columns list according to the displayIndex and visibility = true
      Dim columnsList As List(Of String) = (From column As DataGridViewColumn In Me.Columns.Cast(Of DataGridViewColumn)()
                                            Where column.Visible = True
                                            Order By column.DisplayIndex Select column.Name).ToList

      sr = New StreamWriter(CSVFileName, False, System.Text.Encoding.Default)
      sr.AutoFlush = True
      If WriteColumnHeaderNames Then
        '*** Export headers
        For col As Integer = 0 To columnsList.Count - 1
          If Not TypeOf (Me.Columns(columnsList(col))) Is DataGridViewImageColumn Or TypeOf (Me.Columns(columnsList(col))) Is DataGridViewProgressColumn Then
            '***Add text in column
            If Me.Columns(columnsList(col)).HeaderText.Contains(delimiter) Then
              rowData += """" & Me.Columns(columnsList(col)).HeaderText & """"
            Else
              rowData += Me.Columns(columnsList(col)).HeaderText
            End If
            '*** Test adding field separator
            rowData += IIf(col < columnCount, delimiter, "").ToString()
          End If
        Next col
        sr.WriteLine(rowData)
      End If

      '*** Export data
      For row As Integer = 0 To rowsCount
        rowData = ""
        For col As Integer = 0 To columnsList.Count - 1
          If Not TypeOf (Me.Columns(columnsList(col))) Is DataGridViewImageColumn Or TypeOf (Me.Columns(columnsList(col))) Is DataGridViewProgressColumn Then
            '*** Add data
            If Me.Rows(row).Cells(columnsList(col)).Value Is Nothing Then
              rowData += ""
            Else
              If TypeOf (Me.Rows(row).Cells(columnsList(col))) Is DataGridViewPasswordTextBoxCell Then
                rowData += Me.Rows(row).Cells(columnsList(col)).FormattedValue.ToString
              Else
                If Me.Rows(row).Cells(columnsList(col)).Value.ToString.Contains(delimiter) Then
                  rowData += """" & Me.Rows(row).Cells(columnsList(col)).Value.ToString & """"
                Else
                  rowData += Me.Rows(row).Cells(columnsList(col)).Value.ToString
                End If
              End If
            End If
            '*** Test adding field separator
            rowData += IIf(col < columnCount, delimiter, "").ToString
          End If
        Next col
        If Not String.IsNullOrEmpty(rowData) Then sr.WriteLine(rowData)
      Next row
      sr.Close()

      If IO.File.Exists(CSVFileName) Then
        Dim myProcess As New Process

        myProcess.StartInfo.FileName = CSVFileName
        myProcess.Start()

      End If

      Return String.Empty

    Catch ex As Exception
      If Not sr Is Nothing Then sr.Close()
      Return ex.Message

    Finally
      '*** Cleaning
      If Not sr Is Nothing Then
        sr.Dispose()
      End If
      sr = Nothing
    End Try

  End Function

  ''' <summary>
  ''' Export a DataGridView to XLSX file to temporary folder
  ''' </summary>
  ''' <returns>Empty string if no error otherwise returns error message</returns>
  ''' <remarks></remarks>
  Public Function ExportToXLSX() As String
    Return ExportToXLSX(_XLSXFilename, ExportWriteColumnHeader)
  End Function

  ''' <summary>
  ''' Export a DataGridView to XLSX file
  ''' </summary>
  ''' <param name="XLSXFileName">XLSX file</param>
  ''' <param name="WriteColumnHeaderNames">Write header</param>
  ''' <returns>Empty string if no error otherwise returns error message</returns>
  ''' <remarks></remarks>
  ''' http://codesr.thewebflash.com/2014/10/export-datagridview-data-to-excel-using_16.html
  Public Function ExportToXLSX(ByVal XLSXFileName As String, Optional ByVal WriteColumnHeaderNames As Boolean = False) As String
    Dim columnCount As Integer = Me.Columns.Count - 1
    Dim rowsCount As Integer = Me.RowCount - 1

    If String.IsNullOrEmpty(XLSXFileName) Then
      XLSXFileName = System.IO.Path.GetTempFileName.Replace(".tmp", ".xlsx") 'temp file name , not really sure 100% is unique.
    End If
    '*** Test si adding column is allowed
    '*** If yes, there is an empty line that we doesn't export
    If Me.AllowUserToAddRows Then rowsCount -= 1

    Try
      If IO.File.Exists(XLSXFileName) Then
        IO.File.Delete(XLSXFileName)
      End If

      Using pck As ExcelPackage = New ExcelPackage(New FileInfo(XLSXFileName))
        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("Sheet1")

        'If Me.DataSource IsNot Nothing Then
        '  If Me.DataSource.GetType.Namespace = "System.Collections.Generic" Then
        '    ws.Cells("A1").LoadFromCollection(Me.DataSource, WriteColumnHeaderNames)
        '    'ws.Cells("A1").LoadFromDataTable(Me.ConvertToDataTable(Me.DataSource), WriteColumnHeaderNames)
        '  Else
        '    ws.Cells("A1").LoadFromDataTable(CType(Me.DataSource, DataTable), WriteColumnHeaderNames)
        '  End If
        'Else

        '*** Get the columns list according to the displayIndex and visibility = true
        Dim columnsList As List(Of String) = (From column As DataGridViewColumn In Me.Columns.Cast(Of DataGridViewColumn)()
                                              Where column.Visible = True
                                              Order By column.DisplayIndex Select column.Name).ToList

        If WriteColumnHeaderNames Then
          '*** Export headers
          For col As Integer = 0 To columnsList.Count - 1
            '*** Add column text
            ws.Cells(1, col + 1).Value = Me.Columns(columnsList(col)).HeaderText
          Next col
          ws.Cells(1, 1, 1, columnsList.Count).AutoFilter = True
        End If

        '*** Export data
        For row As Integer = 0 To rowsCount
          For col As Integer = 0 To columnsList.Count - 1
            '*** Add data
            If Me.Rows(row).Cells(columnsList(col)).Value Is Nothing Then
              ws.Cells(row + 2, col + 1).Value = String.Empty
            Else
              If TypeOf (Me.Rows(row).Cells(columnsList(col))) Is DataGridViewCalendarCell Then
                '*** Get the date format from the columnsList(col)
                If String.IsNullOrEmpty(Me.Columns(columnsList(col)).DefaultCellStyle.Format) Then
                  ws.Cells(row + 2, col + 1).Value = Me.Rows(row).Cells(columnsList(col)).Value.ToString()
                Else
                  ws.Cells(row + 2, col + 1).Value = DirectCast(Me.Rows(row).Cells(columnsList(col)).Value, Date).ToString(Me.Columns(col).DefaultCellStyle.Format)
                End If
              ElseIf TypeOf (Me.Rows(row).Cells(columnsList(col))) Is DataGridViewPasswordTextBoxCell Then
                ws.Cells(row + 2, col + 1).Value = Me.Rows(row).Cells(columnsList(col)).FormattedValue.ToString
              ElseIf TypeOf (Me.Rows(row).Cells(columnsList(col))) Is DataGridViewProgressCell Then
                ws.Cells(row + 2, col + 1).Value = Me.Rows(row).Cells(columnsList(col)).Value
              ElseIf TypeOf (Me.Rows(row).Cells(columnsList(col))) Is DataGridViewImageCell Then
                '*** http://www.codeproject.com/Articles/680421/Create-Read-Edit-Advance-Excel-Report-in
                Dim cellImage As Image = DirectCast(Me.Rows(row).Cells(columnsList(col)).FormattedValue, Image)
                If Not cellImage Is Nothing Then
                  Dim excelImage As OfficeOpenXml.Drawing.ExcelPicture
                  excelImage = ws.Drawings.AddPicture("imageC" & col + 1 & "R" & row + 2, cellImage)
                  excelImage.From.Row = row + 1
                  excelImage.From.Column = col
                  excelImage.SetSize(cellImage.Width, cellImage.Height)
                  ' 2x2 px space for better alignment
                  excelImage.From.ColumnOff = Pixel2MTU(2)
                  excelImage.From.RowOff = Pixel2MTU(2)
                End If

              Else
                ws.Cells(row + 2, col + 1).Value = Me.Rows(row).Cells(columnsList(col)).Value
              End If
            End If

          Next col
        Next row

        'End If

        '*** Put color on header
        Using rng As ExcelRange = ws.Cells(1, 1, 1, Me.Columns.Count)
          rng.Style.Font.Bold = True
          rng.Style.Fill.PatternType = ExcelFillStyle.Solid
          rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(79, 129, 189))
          rng.Style.Font.Color.SetColor(System.Drawing.Color.White)
        End Using

        ws.Cells.AutoFitColumns()

        pck.Save()

      End Using

      If IO.File.Exists(XLSXFileName) Then
        Dim myProcess As New Process

        myProcess.StartInfo.FileName = XLSXFileName
        myProcess.Start()

      End If

      Return String.Empty

    Catch ex As Exception
      Return ex.Message

    End Try

  End Function

  ''' <summary>
  ''' Export a DataGridView to HTML file
  ''' </summary>
  ''' <param name="HTMLFilename">HTML file</param>
  ''' <returns>Empty string if no error otherwise returns error message</returns>
  ''' <remarks></remarks>
  Public Function ExportToHTML(ByVal HTMLFilename As String) As String
    Dim columnCount As Integer = Me.Columns.Count - 1
    Dim rowsCount As Integer = Me.RowCount - 1
    Dim filesFolder As String
    Dim filesFolderName As String

    '*** Test si adding column is allowed
    '*** If yes, there is an empty line that we doesn't export
    If Me.AllowUserToAddRows Then rowsCount -= 1

    filesFolderName = IO.Path.GetFileNameWithoutExtension(HTMLFilename) & "_files"
    filesFolder = IO.Path.Combine(IO.Path.GetDirectoryName(HTMLFilename), filesFolderName)
    Try
      '*** Delete HTML file and image folder
      If IO.File.Exists(HTMLFilename) Then
        IO.File.Delete(HTMLFilename)
      End If
      Try
        If IO.Directory.Exists(filesFolder) Then
          IO.Directory.Delete(filesFolder, True)
        End If
        IO.Directory.CreateDirectory(filesFolder)

      Catch ex As Exception

      End Try

      '*** Get the columns list according to the displayIndex and visibility = true
      Dim columnsList As List(Of String) = (From column As DataGridViewColumn In Me.Columns.Cast(Of DataGridViewColumn)()
                                            Where column.Visible = True
                                            Order By column.DisplayIndex Select column.Name).ToList

      Dim writer As New IO.StreamWriter(HTMLFilename, False, System.Text.Encoding.UTF8)

      writer.WriteLine(My.Resources.HTMLHeader)
      '*** Export headers
      writer.WriteLine("    <table>")
      writer.WriteLine("      <thead>")
      writer.WriteLine("        <tr>")
      For Each col As String In columnsList
        '*** Add column text
        writer.WriteLine("          <th>{0}</th>", Me.Columns(col).HeaderText)
      Next col
      writer.WriteLine("        </tr>")
      writer.WriteLine("      </thead>")

      '*** Export data
      For row As Integer = 0 To rowsCount
        writer.WriteLine("      <tr>")
        For Each col As String In columnsList
          writer.Write("        <td>")
          '*** Add data
          If Me.Rows(row).Cells(col).Value Is Nothing Then
            writer.Write(String.Empty)
          Else
            If TypeOf (Me.Rows(row).Cells(col)) Is DataGridViewCalendarCell Then
              '*** Get the date format from the column
              If String.IsNullOrEmpty(Me.Columns(col).DefaultCellStyle.Format) Then
                writer.Write(Me.Rows(row).Cells(col).Value.ToString())
              Else
                writer.Write(DirectCast(Me.Rows(row).Cells(col).Value, Date).ToString(Me.Columns(col).DefaultCellStyle.Format))
              End If
            ElseIf TypeOf (Me.Rows(row).Cells(col)) Is DataGridViewPasswordTextBoxCell Then
              writer.Write(Me.Rows(row).Cells(col).FormattedValue.ToString)
            ElseIf TypeOf (Me.Rows(row).Cells(col)) Is DataGridViewProgressCell Then
              writer.Write(Me.Rows(row).Cells(col).Value)

              'Dim cellImage As Image = DirectCast(Me.Rows(row).Cells(col), DataGridViewProgressCell).GetPaintedCell()
              'If Not cellImage Is Nothing Then
              '  Try
              '    '*** Save image to files folder
              '    Dim imageFilename As String = String.Format("image{0}-{1}.png", row, Me.Columns(col).DisplayIndex)
              '    cellImage.Save(IO.Path.Combine(filesFolder, imageFilename), Imaging.ImageFormat.Png)
              '  Catch ex As Exception

              '  End Try
              'End If
            ElseIf TypeOf (Me.Rows(row).Cells(col)) Is DataGridViewImageCell Then
              '*** http://www.codeproject.com/Articles/680421/Create-Read-Edit-Advance-Excel-Report-in
              Dim cellImage As Image = DirectCast(Me.Rows(row).Cells(col).FormattedValue, Image)
              If Not cellImage Is Nothing Then
                Try
                  '*** Save image to files folder
                  Dim imageFilename As String = $"image{row}-{Me.Columns(col).DisplayIndex}.png"
                  cellImage.Save(IO.Path.Combine(filesFolder, imageFilename), Imaging.ImageFormat.Png)
                  writer.Write("<img src=""" & IO.Path.Combine(filesFolderName, imageFilename) & """/>")
                Catch ex As Exception

                End Try
              End If

            Else
              writer.Write(Me.Rows(row).Cells(col).Value)
            End If
          End If
          writer.WriteLine("</td>")
        Next col
        writer.WriteLine("      </tr>")
      Next row

      writer.WriteLine(My.Resources.HTMLFooter)
      writer.Flush()
      writer.Close()

      If IO.File.Exists(HTMLFilename) Then
        Dim myProcess As New Process

        myProcess.StartInfo.FileName = HTMLFilename
        myProcess.Start()

      End If

    Catch ex As Exception
      Return ex.Message

    End Try
    Return String.Empty
  End Function

  ''' <summary>
  ''' Calculate a margin
  ''' </summary>
  ''' <param name="pixels"></param>
  ''' <returns></returns>
  Private Function Pixel2MTU(pixels As Integer) As Integer
    Dim mtus As Integer = pixels * 9525
    Return mtus
  End Function

  ''' <summary>
  ''' Allows you to change visibility of columns
  ''' </summary>
  ''' <url>http://www.extensionmethod.net/csharp/datagridview/datagridview-columns-visibility-configuration-window</url>
  Public Sub ShowConfigurationWindow()
    Using frmConfig = New FormColumnsConfig(Me)
      frmConfig.ShowDialog()
    End Using
  End Sub

  <Obsolete("Utilisez la propriété 'Items' à la place. 'Cells' existe uniquement pour la conversion des spreads")>
  Public Property Cells(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As Object
    Get
      Dim result As Object = Nothing
      Try
        If rowIndex < Me.Rows.Count AndAlso columnIndex < Me.Columns.Count Then
          If Me.Rows(rowIndex).Cells(columnIndex).Value Is Nothing Then
            result = String.Empty
          Else
            result = Me.Rows(rowIndex).Cells(columnIndex).Value
          End If
        End If

      Catch ex As Exception
        result = String.Empty
      End Try

      Return result

    End Get
    Set(ByVal value As Object)
      Try
        If rowIndex < Me.Rows.Count AndAlso columnIndex < Me.Columns.Count Then
          Me.Rows(rowIndex).Cells(columnIndex).Value = value
        End If

      Catch ex As Exception
        Me.Rows(rowIndex).Cells(columnIndex).Value = Nothing
      End Try

    End Set
  End Property

#End Region

#Region "Private methods"

  'http://stackoverflow.com/questions/1805626/how-to-fill-a-datatable-with-a-listof-t-or-convert-a-listof-t-to-a-datatable
  Private Function ConvertToDataTable(Of T)(ByVal list As IList(Of T)) As DataTable
    Dim table As New DataTable()
    Dim fields() As FieldInfo = GetType(T).GetFields()
    For Each field As FieldInfo In fields
      table.Columns.Add(field.Name, field.FieldType)
    Next
    For Each item As T In list
      Dim row As DataRow = table.NewRow()
      For Each field As FieldInfo In fields
        row(field.Name) = field.GetValue(item)
      Next
      table.Rows.Add(row)
    Next
    Return table
  End Function

#End Region

#Region "Events"

  Private Sub DataGridViewEx_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
    Dim ret As String = String.Empty

    If e.KeyCode = _ExportToCSVKey Then
      ret = Me.ExportToCSV(_CSVFilename, _ExportWriteColumnHeader, _CSVFieldSeparator)
      If Not String.IsNullOrEmpty(ret) Then
        MessageBox.Show(ret, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
      End If
    End If

    If e.KeyCode = _ExportToXLSXKey Then
      ret = Me.ExportToXLSX(_XLSXFilename, _ExportWriteColumnHeader)
      If Not String.IsNullOrEmpty(ret) Then
        MessageBox.Show(ret, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
      End If
    End If

  End Sub

  '*** http://stackoverflow.com/questions/9581626/show-row-number-in-row-header-of-a-datagridview
  Private Sub DataGridViewEx_RowPostPaint(sender As Object, e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs) Handles Me.RowPostPaint
    If Not _DisplayLinesNumber Or Not RowHeadersVisible Then Return
    Dim grid As DataGridView = TryCast(sender, DataGridView)
    Dim rowIdx As String = (e.RowIndex + 1).ToString()

    ' right alignment might actually make more sense for numbers
    Dim centerFormat As StringFormat = New StringFormat() With {
     .Alignment = StringAlignment.Center,
     .LineAlignment = StringAlignment.Center
    }

    Dim headerBounds As Rectangle = New Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height)
    e.Graphics.DrawString(rowIdx, Me.Font, SystemBrushes.ControlText, headerBounds, centerFormat)
  End Sub

  Private Sub mnuColumnSettings_Click(sender As Object, e As EventArgs) Handles mnuColumnSettings.Click
    Me.ShowConfigurationWindow()
  End Sub

  Private Sub mnuExportCSV_Click(sender As Object, e As EventArgs) Handles mnuExportCSV.Click
    Using dlg As New SaveFileDialog
      dlg.Filter = "CSV file (*.csv)|*.csv"
      If dlg.ShowDialog = DialogResult.Cancel Then Return
      Me.ExportToCSV(dlg.FileName, True)
    End Using
  End Sub

  Private Sub mnuExportExcel_Click(sender As Object, e As EventArgs) Handles mnuExportExcel.Click
    Using dlg As New SaveFileDialog
      dlg.Filter = "Excel file (*.xlsx)|*.xlsx"
      If dlg.ShowDialog = DialogResult.Cancel Then Return
      Me.ExportToXLSX(dlg.FileName, True)
    End Using
  End Sub

  Private Sub mnuPrint_Click(sender As Object, e As EventArgs) Handles mnuPrint.Click
    Me.Print(Me.Name, _PrintTitle, _PrintDate, False)
  End Sub

  Private Sub mnuPrintPreview_Click(sender As Object, e As EventArgs) Handles mnuPrintPreview.Click
    Me.Print(Me.Name, _PrintTitle, _PrintDate, True)
  End Sub

  Private Sub DataGridViewEx_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
    If e.Button = MouseButtons.Right Then
      If Me.ContextMenuStrip Is Nothing Then
        If _ContextMenuStripMergeMode <> ContextMenuStripMergingEnum.ExternalMenuOnly Then
          ContextMenuStripMerge()
          Me.ContextMenuStrip.Show(Me, e.X, e.Y)
        End If
      Else
        '*** Merge menu
        ContextMenuStripMerge()
        Me.ContextMenuStrip.Show(Me, e.X, e.Y)
      End If
    End If
  End Sub

  Private Sub mnuExportHTML_Click(sender As Object, e As EventArgs) Handles mnuExportHTML.Click
    Using dlg As New SaveFileDialog
      dlg.Filter = "HTML file (*.html)|*.html"
      If dlg.ShowDialog = DialogResult.Cancel Then Return
      Me.ExportToHTML(dlg.FileName)
    End Using
  End Sub

#End Region

End Class

'#Region "Calendrier"

''http://msdn.microsoft.com/en-us/library/7tas5c80(v=vs.80).aspx
'Public Class CalendarColumn
'  Inherits DataGridViewColumn

'  Public Sub New()
'    MyBase.New(New CalendarCell())
'  End Sub

'  Public Overrides Property CellTemplate() As DataGridViewCell
'    Get
'      Return MyBase.CellTemplate
'    End Get
'    Set(ByVal value As DataGridViewCell)

'      ' Ensure that the cell used for the template is a CalendarCell.
'      If (value IsNot Nothing) AndAlso _
'          Not value.GetType().IsAssignableFrom(GetType(CalendarCell)) _
'          Then
'        Throw New InvalidCastException("Must be a CalendarCell")
'      End If
'      MyBase.CellTemplate = value

'    End Set
'  End Property

'End Class

'Public Class CalendarCell
'  Inherits DataGridViewTextBoxCell

'  Public Sub New()
'    ' Use the short date format.
'    Me.Style.Format = "d"
'  End Sub

'  Public Overrides Sub InitializeEditingControl(ByVal rowIndex As Integer, _
'      ByVal initialFormattedValue As Object, _
'      ByVal dataGridViewCellStyle As DataGridViewCellStyle)

'    ' Set the value of the editing control to the current cell value.
'    MyBase.InitializeEditingControl(rowIndex, initialFormattedValue, _
'        dataGridViewCellStyle)

'    Dim ctl As CalendarEditingControl = _
'        CType(DataGridView.EditingControl, CalendarEditingControl)
'    ctl.Value = CType(Me.Value, DateTime)

'  End Sub

'  Public Overrides ReadOnly Property EditType() As Type
'    Get
'      ' Return the type of the editing contol that CalendarCell uses.
'      Return GetType(CalendarEditingControl)
'    End Get
'  End Property

'  Public Overrides ReadOnly Property ValueType() As Type
'    Get
'      ' Return the type of the value that CalendarCell contains.
'      Return GetType(DateTime)
'    End Get
'  End Property

'  Public Overrides ReadOnly Property DefaultNewRowValue() As Object
'    Get
'      ' Use the current date and time as the default value.
'      Return DateTime.Now
'    End Get
'  End Property

'End Class

'Class CalendarEditingControl
'  Inherits DateTimePicker
'  Implements IDataGridViewEditingControl

'  Private dataGridViewControl As DataGridView
'  Private valueIsChanged As Boolean = False
'  Private rowIndexNum As Integer

'  Public Sub New()
'    Me.Format = DateTimePickerFormat.Short
'  End Sub

'  Public Property EditingControlFormattedValue() As Object _
'      Implements IDataGridViewEditingControl.EditingControlFormattedValue

'    Get
'      Return Me.Value.ToShortDateString()
'    End Get

'    Set(ByVal value As Object)
'      If TypeOf value Is String Then
'        Me.Value = DateTime.Parse(CStr(value))
'      End If
'    End Set

'  End Property

'  Public Function GetEditingControlFormattedValue(ByVal context _
'      As DataGridViewDataErrorContexts) As Object _
'      Implements IDataGridViewEditingControl.GetEditingControlFormattedValue

'    Return Me.Value.ToShortDateString()

'  End Function

'  Public Sub ApplyCellStyleToEditingControl(ByVal dataGridViewCellStyle As  _
'      DataGridViewCellStyle) _
'      Implements IDataGridViewEditingControl.ApplyCellStyleToEditingControl

'    Me.Font = dataGridViewCellStyle.Font
'    Me.CalendarForeColor = dataGridViewCellStyle.ForeColor
'    Me.CalendarMonthBackground = dataGridViewCellStyle.BackColor

'  End Sub

'  Public Property EditingControlRowIndex() As Integer _
'      Implements IDataGridViewEditingControl.EditingControlRowIndex

'    Get
'      Return rowIndexNum
'    End Get
'    Set(ByVal value As Integer)
'      rowIndexNum = value
'    End Set

'  End Property

'  Public Function EditingControlWantsInputKey(ByVal key As Keys, _
'      ByVal dataGridViewWantsInputKey As Boolean) As Boolean _
'      Implements IDataGridViewEditingControl.EditingControlWantsInputKey

'    ' Let the DateTimePicker handle the keys listed.
'    Select Case key And Keys.KeyCode
'      Case Keys.Left, Keys.Up, Keys.Down, Keys.Right, _
'          Keys.Home, Keys.End, Keys.PageDown, Keys.PageUp

'        Return True

'      Case Else
'        Return False
'    End Select

'  End Function

'  Public Sub PrepareEditingControlForEdit(ByVal selectAll As Boolean) _
'      Implements IDataGridViewEditingControl.PrepareEditingControlForEdit

'    ' No preparation needs to be done.

'  End Sub

'  Public ReadOnly Property RepositionEditingControlOnValueChange() _
'      As Boolean Implements _
'      IDataGridViewEditingControl.RepositionEditingControlOnValueChange

'    Get
'      Return False
'    End Get

'  End Property

'  Public Property EditingControlDataGridView() As DataGridView _
'      Implements IDataGridViewEditingControl.EditingControlDataGridView

'    Get
'      Return dataGridViewControl
'    End Get
'    Set(ByVal value As DataGridView)
'      dataGridViewControl = value
'    End Set

'  End Property

'  Public Property EditingControlValueChanged() As Boolean _
'      Implements IDataGridViewEditingControl.EditingControlValueChanged

'    Get
'      Return valueIsChanged
'    End Get
'    Set(ByVal value As Boolean)
'      valueIsChanged = value
'    End Set

'  End Property

'  Public ReadOnly Property EditingControlCursor() As Cursor _
'      Implements IDataGridViewEditingControl.EditingPanelCursor

'    Get
'      Return MyBase.Cursor
'    End Get

'  End Property

'  Protected Overrides Sub OnValueChanged(ByVal eventargs As EventArgs)

'    ' Notify the DataGridView that the contents of the cell have changed.
'    valueIsChanged = True
'    Me.EditingControlDataGridView.NotifyCurrentCellDirty(True)
'    MyBase.OnValueChanged(eventargs)

'  End Sub

'End Class

'#End Region

