'http://stackoverflow.com/questions/4646920/populating-a-datagridview-with-text-and-progressbars
Imports System.ComponentModel

Public Class DataGridViewProgressColumn
  Inherits DataGridViewImageColumn

  Public Sub New()
    CellTemplate = New DataGridViewProgressCell()
  End Sub


  Private _ColorProgress As Color = Color.FromArgb(203, 235, 108)
  ''' <summary>
  ''' Gets/Sets the color of the progressbar
  ''' </summary>
  ''' <returns></returns>
  <Browsable(True), Description("Gets/Sets the color of the progressbar"), Category("Appearance"), DefaultValue(GetType(Color), "FFCBEB6C")>
  Public Property ColorProgress() As Color
    Get
      Return _ColorProgress
    End Get
    Set(ByVal value As Color)
      _ColorProgress = value
    End Set
  End Property

  Private _DisplayText As Boolean = True
  ''' <summary>
  ''' Indicates if the text is displayed or not
  ''' </summary>
  ''' <returns></returns>
  <Browsable(True), Description("Indicates if the text is displayed or not"), Category("Behavior"), DefaultValue(True)>
  Public Property DisplayText() As Boolean
    Get
      Return _DisplayText
    End Get
    Set(ByVal value As Boolean)
      _DisplayText = value
    End Set
  End Property

  'Private _FormatText As String = "{0} %"
  '''' <summary>
  '''' Gets/Sets how the text is formatting ({0} is mandatory to display value)
  '''' </summary>
  '''' <returns></returns>
  '<Browsable(True), Description("Gets/Sets how the text is formatting ({0} is mandatory to display value)"), Category("Behavior"), DefaultValue("{0} %")>
  'Public Property FormatText() As String
  '  Get
  '    Return _FormatText
  '  End Get
  '  Set(ByVal value As String)
  '    _FormatText = value
  '  End Set
  'End Property
End Class
