'http://stackoverflow.com/questions/4646920/populating-a-datagridview-with-text-and-progressbars
Imports System.ComponentModel

Public Class DataGridViewProgressCell
  Inherits DataGridViewImageCell

  ' Used to make custom cell consistent with a DataGridViewImageCell
  Shared emptyImage As Image
  Shared Sub New()
    emptyImage = New Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
  End Sub

  Public Sub New()
    Me.ValueType = GetType(Integer)
  End Sub

  'Private _ColorProgress As Color = Color.FromArgb(203, 235, 108)
  '''' <summary>
  '''' Gets/Sets the color of the progressbar
  '''' </summary>
  '''' <returns></returns>
  '<Browsable(True), Description("Gets/Sets the color of the progressbar"), Category("Appearance"), DefaultValue(GetType(Color), "FFCBEB6C")>
  'Public Property ColorProgress() As Color
  '  Get
  '    Return _ColorProgress
  '  End Get
  '  Set(ByVal value As Color)
  '    _ColorProgress = value
  '  End Set
  'End Property

  'Private _DisplayText As Boolean = True
  '''' <summary>
  '''' Indicates if the text is displayed or not
  '''' </summary>
  '''' <returns></returns>
  '<Browsable(True), Description("Indicates if the text is displayed or not"), Category("Appearance"), DefaultValue(True)>
  'Public Property DisplayText() As Boolean
  '  Get
  '    Return _DisplayText
  '  End Get
  '  Set(ByVal value As Boolean)
  '    _DisplayText = value
  '  End Set
  'End Property

  ' Method required to make the Progress Cell consistent with the default Image Cell. 
  ' The default Image Cell assumes an Image as a value, although the value of the Progress Cell is an int.
  Protected Overrides Function GetFormattedValue(value As Object, rowIndex As Integer, ByRef cellStyle As DataGridViewCellStyle, valueTypeConverter As TypeConverter, formattedValueTypeConverter As TypeConverter, context As DataGridViewDataErrorContexts) As Object
    Return emptyImage
  End Function

  Protected Overrides Sub Paint(g As System.Drawing.Graphics, clipBounds As System.Drawing.Rectangle, cellBounds As System.Drawing.Rectangle, rowIndex As Integer, cellState As DataGridViewElementStates, value As Object,
  formattedValue As Object, errorText As String, cellStyle As DataGridViewCellStyle, advancedBorderStyle As DataGridViewAdvancedBorderStyle, paintParts As DataGridViewPaintParts)
    Try

      Dim MyOwner As DataGridViewProgressColumn = CType(OwningColumn, DataGridViewProgressColumn)

      Dim progressVal As Integer = DirectCast(value, Integer)
      Dim percentage As Single = (progressVal / 100.0F)
      ' Need to convert to float before division; otherwise C# returns int which is 0 for anything but 100%.
      Dim backColorBrush As Brush = New SolidBrush(cellStyle.BackColor)
      Dim foreColorBrush As Brush = New SolidBrush(cellStyle.ForeColor)
      ' Draws the cell grid
      MyBase.Paint(g, clipBounds, cellBounds, rowIndex, cellState, value,
      formattedValue, errorText, cellStyle, advancedBorderStyle, (paintParts And Not DataGridViewPaintParts.ContentForeground))
      If percentage > 0.0 Then
        ' Draw the progress bar and the text
        Dim width As Single = Convert.ToInt32((percentage * cellBounds.Width - 4))
        ' Check if value exceed the column width
        If width > cellBounds.Width - 4 Then width = cellBounds.Width - 4

        g.FillRectangle(New SolidBrush(MyOwner.ColorProgress), cellBounds.X + 2, cellBounds.Y + 2, width, cellBounds.Height - 4)
        If MyOwner.DisplayText Then
          ' draw the text
          g.DrawString(String.Format("{0} %", progressVal.ToString()), cellStyle.Font, foreColorBrush, Convert.ToSingle(cellBounds.X + (cellBounds.Width / 2) - 5), Convert.ToSingle(cellBounds.Y + 2))
        End If
      Else
        If MyOwner.DisplayText Then
          ' draw the text
          If Me.DataGridView.CurrentRow.Index = rowIndex Then
            g.DrawString(String.Format("{0} %", progressVal.ToString()), cellStyle.Font, New SolidBrush(cellStyle.SelectionForeColor), cellBounds.X + 6, cellBounds.Y + 2)
          Else
            g.DrawString(String.Format("{0} %", progressVal.ToString()), cellStyle.Font, foreColorBrush, cellBounds.X + 6, cellBounds.Y + 2)
          End If
        End If
      End If
    Catch ex As Exception
      Console.WriteLine(ex.Message)
    End Try

  End Sub

  Protected Friend Function GetPaintedCell() As Bitmap
    Dim g As System.Drawing.Graphics
    Dim cellBounds As System.Drawing.Rectangle = Me.ContentBounds

    g = Graphics.FromImage(New Bitmap(OwningColumn.Width, OwningRow.Height))
    Try
      Dim clipBounds As System.Drawing.Rectangle = Me.ContentBounds
      Dim cellStyle As DataGridViewCellStyle = Me.Style
      Dim MyOwner As DataGridViewProgressColumn = CType(OwningColumn, DataGridViewProgressColumn)

      If cellStyle.Font Is Nothing Then
        cellStyle.Font = MyOwner.InheritedStyle.Font
      End If

      Dim progressVal As Integer = DirectCast(Value, Integer)
      Dim percentage As Single = (progressVal / 100.0F)
      ' Need to convert to float before division; otherwise C# returns int which is 0 for anything but 100%.
      Dim backColorBrush As Brush = New SolidBrush(cellStyle.BackColor)
      Dim foreColorBrush As Brush = New SolidBrush(cellStyle.ForeColor)
      ' Draws the cell grid

      If percentage > 0.0 Then
        ' Draw the progress bar and the text
        Dim width As Single = Convert.ToInt32((percentage * cellBounds.Width - 4))
        ' Check if value exceed the column width
        If width > cellBounds.Width - 4 Then width = cellBounds.Width - 4

        g.FillRectangle(New SolidBrush(MyOwner.ColorProgress), cellBounds.X + 2, cellBounds.Y + 2, width, cellBounds.Height - 4)
        If MyOwner.DisplayText Then
          ' draw the text
          g.DrawString(String.Format("{0} %", progressVal.ToString()), cellStyle.Font, foreColorBrush, Convert.ToSingle(cellBounds.X + (cellBounds.Width / 2) - 5), Convert.ToSingle(cellBounds.Y + 2))
        End If
      Else
        If MyOwner.DisplayText Then
          ' draw the text
          If Me.DataGridView.CurrentRow.Index = RowIndex Then
            g.DrawString(String.Format("{0} %", progressVal.ToString()), cellStyle.Font, New SolidBrush(cellStyle.SelectionForeColor), cellBounds.X + 6, cellBounds.Y + 2)
          Else
            g.DrawString(String.Format("{0} %", progressVal.ToString()), cellStyle.Font, foreColorBrush, cellBounds.X + 6, cellBounds.Y + 2)
          End If
        End If
      End If
    Catch ex As Exception
      Console.WriteLine(ex.Message)
    End Try

    Return New Bitmap(OwningColumn.Width, OwningRow.Height, g)
  End Function

End Class
