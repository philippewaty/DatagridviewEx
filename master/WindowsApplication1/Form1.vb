Public Class Form1

  Private _configuration As String

  Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    Dim lst As New List(Of Users)
    Dim index As Integer

    For i As Integer = 1 To 10
      lst.Add(New Users(i * 2, "Nom" & i, "login" & i, "password" & i, New Date(1989 + i, i, i), i * 10))
    Next
    lst.Add(New Users(22, "Nom11", "login11", "password11", Nothing, 110))

    With DataGridViewEx1
      .AutoGenerateColumns = False
      .SuspendLayout()
      .Columns.Clear()

      .AddTextBoxColumn("columnId", "Id", , "Id")
      .AddTextBoxColumn("columnLogin", "Login", , "Login")
      .AddPasswordColumn("columnPassword", "Mot de passe", , "Password")
      index = .AddTextBoxColumn("columnName", "Nom", , "Name")
      '.Columns(index).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
      .AddCalendarColumn("columnDate", "Date création", , "DNais")
      Dim cal As DataGridViewEx.DataGridViewCalendarColumn = DirectCast(.Columns(.ColumnCount - 1), DataGridViewEx.DataGridViewCalendarColumn)
      cal.DateFormat = "dd/MM/yyyy"
      '.Columns("columnDate").DefaultCellStyle.Format = "dd MMMM yyyy"

      '.AddComboBoxColumn("Liste", DataGridViewComboBoxDisplayStyle.DropDownButton, GetType(String), New String() {"test1", "test2"})
      .AddComboBoxColumn("columnLst", "Liste", DataGridViewComboBoxDisplayStyle.DropDownButton, New String() {"test1", "test2"})
      .AddCheckBoxColumn("columnCheck", "Check")
      .AddImageColumn("columnImg", "Image", "My description", Width:=200)
      .AddProgressColumn("columnPrg", "Progression",, "progress", 150)
      .ResumeLayout()
      .DataSource = lst

      Dim progress As DataGridViewEx.DataGridViewProgressColumn = DirectCast(.Columns("columnPrg"), DataGridViewEx.DataGridViewProgressColumn)
      'progress.ColorProgress = Color.Red
      progress.DisplayText = True

      'Dim progress As DataGridViewEx.DataGridViewProgressCell = DirectCast(.Item(8, 5), DataGridViewEx.DataGridViewProgressCell)
      'progress.ColorProgress = Color.Red
      'progress.DisplayText = False
      'progress.Value = 870

      .Rows(9).DefaultCellStyle.BackColor = Color.SandyBrown

      Dim cboCell As DataGridViewComboBoxCell
      cboCell = CType(.Rows(2).Cells(5), DataGridViewComboBoxCell)
      cboCell.Value = cboCell.Items(1)

      cboCell = CType(.Rows(4).Cells(5), DataGridViewComboBoxCell)
      cboCell.Value = "test1"

      .Item(6, 0).Value = True
      .Item(6, 2).Value = True
      .Item(6, 3).Value = 1
      .Item("columnImg", 0).Value = PictureBox1.Image
      Dim colImg As DataGridViewImageCell = DirectCast(.Item("columnImg", 1), DataGridViewImageCell)
      colImg.Value = PictureBox2.Image
      colImg.ImageLayout = DataGridViewImageCellLayout.Normal
      .Item("columnImg", 2).Value = Image.FromFile("..\ticket.png")

      .PrintDate = True
      .PrintTitle = "Liste d'exemple"
      '.Columns(0).Visible = False

      Dim cellDate As DataGridViewEx.DataGridViewCalendarCell = CType(.Item(4, 0), DataGridViewEx.DataGridViewCalendarCell)
      cellDate.MinDate = New Date(2015, 1, 1)
      cellDate.MaxDate = New Date(2016, 12, 31)

      .ExportSettings.ProgressBarValue = False
    End With

    If IO.File.Exists("config.xml") Then
      _configuration = IO.File.ReadAllText("config.xml")
    End If
    'For Each row As DataGridViewRow In Me.DataGridViewEx1.Rows

    '  MessageBox.Show(CStr(row.Cells(6).Value))

    'Next
  End Sub

  Private Sub btnExportCSV_Click(sender As System.Object, e As System.EventArgs) Handles btnExportCSV.Click
    Dim ret As String = DataGridViewEx1.ExportToCSV()
    If Not String.IsNullOrEmpty(ret) Then
      MessageBox.Show(ret)
    End If
  End Sub

  Private Sub btnExportXLSX_Click(sender As Object, e As System.EventArgs) Handles btnExportXLSX.Click
    Dim ret As String = DataGridViewEx1.ExportToXLSX()
    If Not String.IsNullOrEmpty(ret) Then
      MessageBox.Show(ret)
    End If
  End Sub

  Private Sub DataGridViewEx1_DataError(sender As Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles DataGridViewEx1.DataError
    Debug.Print(e.Exception.Message)
  End Sub

  Private Sub btnPrint_Click(sender As System.Object, e As System.EventArgs) Handles btnPrint.Click
    Me.DataGridViewEx1.Print("Test Page Print", "Liste d'exemple", True, True)
  End Sub

  Private Sub btnGetConfig_Click(sender As Object, e As EventArgs) Handles btnGetConfig.Click
    _configuration = DataGridViewEx1.Configuration
    IO.File.WriteAllText("config.xml", _configuration)
  End Sub

  Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnSetConfig.Click
    DataGridViewEx1.Configuration = _configuration
  End Sub

  'Private Sub DataGridViewEx1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridViewEx1.CellFormatting
  '  If e.ColumnIndex = 4 Then
  '    Dim theDate As DateTime = DateTime.Parse(e.Value.ToString)
  '    e.Value = theDate.ToString(e.CellStyle.Format)
  '  End If
  'End Sub
End Class
