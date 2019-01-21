Public Class FormColumnsConfig

  ''' <summary>
  ''' Class used to store DataGridView column info
  ''' </summary>
  Private Class ItemData

    Private _columnName As String
    Private _Text As String

    Public Sub New(columnName As String, text As String)
      _columnName = columnName
      _Text = text
    End Sub

    Public Property ColumnName() As String
      Get
        Return _columnName
      End Get
      Set(ByVal value As String)
        _columnName = value
      End Set
    End Property

    Public Property Text() As String
      Get
        Return _Text
      End Get
      Set(ByVal value As String)
        _Text = value
      End Set
    End Property

    Public Overrides Function ToString() As String
      Return _Text
    End Function

  End Class

  Private _dataGridView As DataGridViewEx
  Private _loading As Boolean

  Public Sub New(dataGridView As DataGridViewEx)

    ' This call is required by the designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    _dataGridView = dataGridView
  End Sub

  Private Sub FormColumnsConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    _loading = True
    '*** Add dumies items
    For i As Integer = 0 To _dataGridView.Columns.Count - 1
      lstColumns.Items.Add("", False)
    Next
    '*** Fill in the real items at the right place according to displayIndex propertie
    For i As Integer = 0 To _dataGridView.Columns.Count - 1
      Dim item As New ItemData(_dataGridView.Columns(i).Name, _dataGridView.Columns(i).HeaderText)
      lstColumns.Items.Item(_dataGridView.Columns(i).DisplayIndex) = item
      lstColumns.SetItemChecked(i, _dataGridView.Columns(i).Visible)
    Next
    If lstColumns.Items.Count > 0 Then lstColumns.SelectedIndex = 0
    _loading = False
  End Sub

  Private Sub lstColumns_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstColumns.ItemCheck
    If _loading Then Return
    If e.Index > -1 And e.Index < lstColumns.Items.Count Then
      Dim item As ItemData = DirectCast(lstColumns.Items(e.Index), ItemData)
      _dataGridView.Columns(item.ColumnName).Visible = e.NewValue = CheckState.Checked
    End If
  End Sub

  Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
    If lstColumns.SelectedIndex = 0 Then Return

    Dim index As Integer = lstColumns.SelectedIndex
    Dim item As ItemData = DirectCast(lstColumns.SelectedItem, ItemData)
    lstColumns.Items.Insert(index - 1, lstColumns.SelectedItem)
    lstColumns.SetItemChecked(index - 1, lstColumns.GetItemChecked(index+1))
    lstColumns.Items.RemoveAt(index + 1)
    lstColumns.SelectedIndex = index - 1
    _dataGridView.Columns(item.ColumnName).DisplayIndex = lstColumns.SelectedIndex
  End Sub

  Private Sub btnDown_Click(sender As Object, e As EventArgs) Handles btnDown.Click
    If lstColumns.SelectedIndex <> -1 And lstColumns.SelectedIndex = lstColumns.Items.Count - 1 Then Return

    Dim index As Integer = lstColumns.SelectedIndex
    Dim item As ItemData = DirectCast(lstColumns.SelectedItem, ItemData)
    lstColumns.Items.Insert(index + 2, lstColumns.SelectedItem)
    lstColumns.SetItemChecked(index + 2, lstColumns.GetItemChecked(index ))
    lstColumns.Items.RemoveAt(index)
    lstColumns.SelectedIndex = index + 1
    _dataGridView.Columns(item.ColumnName).DisplayIndex = lstColumns.SelectedIndex
  End Sub

  Private Sub btnRestore_Click(sender As Object, e As EventArgs) Handles btnRestore.Click
    lstColumns.Items.Clear()
    '*** Fill in the items
    For i As Integer = 0 To _dataGridView.Columns.Count - 1
      Dim item As New ItemData(_dataGridView.Columns(i).Name, _dataGridView.Columns(i).HeaderText)
      lstColumns.Items.Add(item)
      lstColumns.SetItemChecked(i, True)
      _dataGridView.Columns(item.ColumnName).DisplayIndex = _dataGridView.Columns(item.ColumnName).Index
      _dataGridView.Columns(item.ColumnName).Visible = True
    Next
    If lstColumns.Items.Count > 0 Then lstColumns.SelectedIndex = 0
  End Sub
End Class