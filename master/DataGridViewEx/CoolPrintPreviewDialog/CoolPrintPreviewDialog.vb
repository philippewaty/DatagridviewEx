Imports System.Drawing.Printing
Imports System.ComponentModel


Public Class CoolPrintPreviewDialog

  ' ** fields
  Private _doc As PrintDocument

  ' ** ctors
  Public Sub New()
    Me.New(Nothing)
  End Sub

  Public Sub New(ByVal parentForm As Control)
    Me.components = Nothing
    Me.InitializeComponent()
    If (Not parentForm Is Nothing) Then
      MyBase.Size = parentForm.Size
    End If
  End Sub

  ' ** overrides
  Protected Overrides Sub OnFormClosing(ByVal e As FormClosingEventArgs)
    MyBase.OnFormClosing(e)
    If Not (Not Me._preview.IsRendering OrElse e.Cancel) Then
      Me._preview.Cancel()
    End If
  End Sub

  Protected Overrides Sub OnShown(ByVal e As EventArgs)
    MyBase.OnShown(e)
    Me._preview.Document = Me.Document
  End Sub

  ' ** properties
  Public Property Document() As PrintDocument
    Get
      Return Me._doc
    End Get
    Set(ByVal value As PrintDocument)
      If (Not Me._doc Is Nothing) Then
        RemoveHandler Me._doc.BeginPrint, New PrintEventHandler(AddressOf Me._doc_BeginPrint)
        RemoveHandler Me._doc.EndPrint, New PrintEventHandler(AddressOf Me._doc_EndPrint)
      End If
      Me._doc = value
      If (Not Me._doc Is Nothing) Then
        AddHandler Me._doc.BeginPrint, New PrintEventHandler(AddressOf Me._doc_BeginPrint)
        AddHandler Me._doc.EndPrint, New PrintEventHandler(AddressOf Me._doc_EndPrint)
      End If
      If MyBase.Visible Then
        Me._preview.Document = Me.Document
      End If
    End Set
  End Property

  ' ** event handlers
  Private Sub _btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _btnCancel.Click
    If Me._preview.IsRendering Then
      Me._preview.Cancel()
    Else
      MyBase.Close()
    End If
  End Sub

  Private Sub _btnFirst_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _btnFirst.Click
    Me._preview.StartPage = 0
  End Sub

  Private Sub _btnLast_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _btnLast.Click
    Me._preview.StartPage = (Me._preview.PageCount - 1)
  End Sub

  Private Sub _btnNext_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _btnNext.Click
    Me._preview.StartPage += 1
  End Sub

  Private Sub _btnPrev_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _btnPrev.Click
    Me._preview.StartPage -= 1
  End Sub

  Private Sub _btnPageSetup_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _btnPageSetup.Click
    Using dlg As PageSetupDialog = New PageSetupDialog
      dlg.Document = Me.Document
      '*** EnableMetric = True
      '*** https://support.microsoft.com/fr-fr/kb/814355
      '*** But as can be read *here*, there is a bug in the .NET framework with the PageSetupDialog margin settings when using the metric units system.
      '*** In .NET 2.0 it can be fixed by setting the PageSetupDialog.EnableMetric property to true in the tsBtnPageSettings event handler in EnhancedPrintPreviewDialog.cs
      dlg.EnableMetric = True
      If (dlg.ShowDialog(Me) = DialogResult.OK) Then
        Me._preview.RefreshPreview()
      End If
    End Using
  End Sub

  Private Sub _btnPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _btnPrint.Click
    Using dlg As PrintDialog = New PrintDialog
      dlg.AllowSomePages = True
      dlg.AllowSelection = True
      dlg.UseEXDialog = True
      dlg.Document = Me.Document
      Dim ps As PrinterSettings = dlg.PrinterSettings
      ps.MinimumPage = 1
      ps.MaximumPage = Me._preview.PageCount
      ps.FromPage = 1
      ps.ToPage = Me._preview.PageCount
      If (dlg.ShowDialog(Me) = DialogResult.OK) Then
        Me._preview.Print()
      End If
    End Using
  End Sub

  Private Sub _btnZoom_ButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles _btnZoom.ButtonClick
    Me._preview.ZoomMode = CType(IIf((Me._preview.ZoomMode = ZoomMode.ActualSize), ZoomMode.FullPage, ZoomMode.ActualSize), ZoomMode)
  End Sub

  Private Sub _btnZoom_DropDownItemClicked(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs) Handles _btnZoom.DropDownItemClicked
    If (e.ClickedItem Is Me._itemActualSize) Then
      Me._preview.ZoomMode = ZoomMode.ActualSize
    ElseIf (e.ClickedItem Is Me._itemFullPage) Then
      Me._preview.ZoomMode = ZoomMode.FullPage
    ElseIf (e.ClickedItem Is Me._itemPageWidth) Then
      Me._preview.ZoomMode = ZoomMode.PageWidth
    ElseIf (e.ClickedItem Is Me._itemTwoPages) Then
      Me._preview.ZoomMode = ZoomMode.TwoPages
    End If
    If (e.ClickedItem Is Me._item10) Then
      Me._preview.Zoom = 0.1
    ElseIf (e.ClickedItem Is Me._item100) Then
      Me._preview.Zoom = 1
    ElseIf (e.ClickedItem Is Me._item150) Then
      Me._preview.Zoom = 1.5
    ElseIf (e.ClickedItem Is Me._item200) Then
      Me._preview.Zoom = 2
    ElseIf (e.ClickedItem Is Me._item25) Then
      Me._preview.Zoom = 0.25
    ElseIf (e.ClickedItem Is Me._item50) Then
      Me._preview.Zoom = 0.5
    ElseIf (e.ClickedItem Is Me._item500) Then
      Me._preview.Zoom = 5
    ElseIf (e.ClickedItem Is Me._item75) Then
      Me._preview.Zoom = 0.75
    End If
  End Sub

  Private Sub _doc_BeginPrint(ByVal sender As Object, ByVal e As PrintEventArgs)
    Me._btnCancel.Text = "&Cancel"
    Me._btnPrint.Enabled = Me._btnPageSetup.Enabled = False
  End Sub

  Private Sub _doc_EndPrint(ByVal sender As Object, ByVal e As PrintEventArgs)
    Me._btnCancel.Text = "&Close"
    Me._btnPrint.Enabled = Me._btnPageSetup.Enabled = True
  End Sub

  Private Sub _preview_PageCountChanged(ByVal sender As Object, ByVal e As EventArgs) Handles _preview.PageCountChanged
    MyBase.Update()
    Application.DoEvents()
    Me._lblPageCount.Text = String.Format("of {0}", Me._preview.PageCount)
  End Sub

  Private Sub _preview_StartPageChanged(ByVal sender As Object, ByVal e As EventArgs) Handles _preview.StartPageChanged
    Me._txtStartPage.Text = (Me._preview.StartPage + 1).ToString
  End Sub

  Private Sub _txtStartPage_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles _txtStartPage.Enter
    Me._txtStartPage.SelectAll()
  End Sub

  Private Sub _txtStartPage_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles _txtStartPage.KeyPress
    Dim c As Char = e.KeyChar
    If (c = ChrW(13)) Then
      Me.CommitPageNumber()
      e.Handled = True
    ElseIf Not ((c <= " "c) OrElse Char.IsDigit(c)) Then
      e.Handled = True
    End If
  End Sub

  Private Sub _txtStartPage_Validating(ByVal sender As Object, ByVal e As CancelEventArgs) Handles _txtStartPage.Validating
    Me.CommitPageNumber()
  End Sub

  Private Sub CommitPageNumber()
    Dim page As Integer
    If Integer.TryParse(Me._txtStartPage.Text, page) Then
      Me._preview.StartPage = (page - 1)
    End If
  End Sub

  Private Sub CoolPrintPreviewDialog_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
    If e.Control Then
      ' It's handled
      e.Handled = True
      e.SuppressKeyPress = True

      ' [Ctrl] + [Shift] + P: Page Setup
      If e.Shift AndAlso e.KeyCode = Keys.P Then
        _btnPageSetup_Click(sender, New EventArgs())
        ' [Ctrl] + P: Print
      ElseIf e.KeyCode = Keys.P Then
        _btnPrint_Click(sender, New EventArgs())
        ' [Ctrl] + [+]: Zoom in
      ElseIf e.KeyCode = Keys.Add OrElse e.KeyCode = Keys.Oemplus Then
        ' Don't let them zoom past 1000%
        If _preview.Zoom < 10 Then
          _preview.Zoom += 0.2
        End If
        ' [Ctrl] + [-]: Zoom out
      ElseIf e.KeyCode = Keys.Subtract OrElse e.KeyCode = Keys.OemMinus Then
        ' Don't let them zoom past 10%
        If _preview.Zoom > 0.1 Then
          _preview.Zoom -= 0.2
        End If
        ' [Ctrl] + 0: Reset zoom
      ElseIf e.KeyCode = Keys.D0 OrElse e.KeyCode = Keys.NumPad0 Then
        _preview.Zoom = 1
      Else
        ' Change to that it's not handled
        e.Handled = False
        e.SuppressKeyPress = False
      End If
      ' [Esc] (by itself): Cancel
    ElseIf Not e.Shift AndAlso Not e.Alt AndAlso e.KeyCode = Keys.Escape Then
      e.Handled = True
      e.SuppressKeyPress = True
      Me.Close()
    End If

  End Sub
End Class