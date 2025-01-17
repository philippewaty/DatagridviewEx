﻿Public Class DataGridViewCalendarCell

  Inherits DataGridViewTextBoxCell

  Private _MinDate As DateTime
  Public Property MinDate() As DateTime
    Get
      Return _MinDate
    End Get
    Set(ByVal value As DateTime)
      _MinDate = value
    End Set
  End Property

  Private _MaxDate As DateTime
  Public Property MaxDate() As DateTime
    Get
      Return _MaxDate
    End Get
    Set(ByVal value As DateTime)
      _MaxDate = value
    End Set
  End Property

  Public Sub New()
  End Sub

  Public Overrides Sub InitializeEditingControl(ByVal rowIndex As Integer, _
      ByVal initialFormattedValue As Object, _
      ByVal dataGridViewCellStyle As DataGridViewCellStyle)

    ' Set the value of the editing control to the current cell value.
    MyBase.InitializeEditingControl(rowIndex, initialFormattedValue, _
        dataGridViewCellStyle)

    Dim ctl As CalendarEditingControl = _
        CType(DataGridView.EditingControl, CalendarEditingControl)
    ctl.Value = CType(Me.Value, DateTime)
    If (MinDate > ctl.MaxDate) Then ctl.MaxDate = DateTimePicker.MaximumDateTime
    If (MinDate < DateTimePicker.MinimumDateTime) Then MinDate = DateTimePicker.MinimumDateTime
    If (MaxDate > DateTimePicker.MaximumDateTime) Then MaxDate = DateTimePicker.MaximumDateTime
    If (MaxDate < ctl.MinDate) Then MaxDate = DateTimePicker.MaximumDateTime
    ctl.MinDate = MinDate
    ctl.MaxDate = MaxDate

    Dim MyOwner As DataGridViewCalendarColumn = CType(OwningColumn, DataGridViewCalendarColumn)
    Me.Style.Format = MyOwner.DateFormat
    ctl.Format = DateTimePickerFormat.Custom
    ctl.CustomFormat = MyOwner.DateFormat
  End Sub

  Public Overrides ReadOnly Property EditType() As Type
    Get
      ' Return the type of the editing contol that CalendarCell uses.
      Return GetType(CalendarEditingControl)
    End Get
  End Property

  Public Overrides ReadOnly Property ValueType() As Type
    Get
      ' Return the type of the value that CalendarCell contains.
      Return GetType(DateTime)
    End Get
  End Property

  Public Overrides ReadOnly Property DefaultNewRowValue() As Object
    Get
      ' Use the current date and time as the default value.
      Return DateTime.Now
    End Get
  End Property

End Class

Class CalendarEditingControl
  Inherits DateTimePicker
  Implements IDataGridViewEditingControl

  Private dataGridViewControl As DataGridView
  Private valueIsChanged As Boolean = False
  Private rowIndexNum As Integer

  Public Sub New()
    'Me.Format = DateTimePickerFormat.Short
    Me.Format = DateTimePickerFormat.Custom
  End Sub

  Public Property EditingControlFormattedValue() As Object _
      Implements IDataGridViewEditingControl.EditingControlFormattedValue

    Get
      Return Me.Value.ToString(Me.CustomFormat)
    End Get

    Set(ByVal value As Object)
      If TypeOf value Is String Then
        Me.Value = DateTime.Parse(CStr(value))
      End If
    End Set

  End Property

  Public Function GetEditingControlFormattedValue(ByVal context _
      As DataGridViewDataErrorContexts) As Object _
      Implements IDataGridViewEditingControl.GetEditingControlFormattedValue

    Return Me.Value.ToString(Me.CustomFormat)

  End Function

  Public Sub ApplyCellStyleToEditingControl(ByVal dataGridViewCellStyle As  _
      DataGridViewCellStyle) _
      Implements IDataGridViewEditingControl.ApplyCellStyleToEditingControl

    Me.Font = dataGridViewCellStyle.Font
    Me.CalendarForeColor = dataGridViewCellStyle.ForeColor
    Me.CalendarMonthBackground = dataGridViewCellStyle.BackColor

  End Sub

  Public Property EditingControlRowIndex() As Integer _
      Implements IDataGridViewEditingControl.EditingControlRowIndex

    Get
      Return rowIndexNum
    End Get
    Set(ByVal value As Integer)
      rowIndexNum = value
    End Set

  End Property

  Public Function EditingControlWantsInputKey(ByVal key As Keys, _
      ByVal dataGridViewWantsInputKey As Boolean) As Boolean _
      Implements IDataGridViewEditingControl.EditingControlWantsInputKey

    ' Let the DateTimePicker handle the keys listed.
    Select Case key And Keys.KeyCode
      Case Keys.Left, Keys.Up, Keys.Down, Keys.Right, _
          Keys.Home, Keys.End, Keys.PageDown, Keys.PageUp

        Return True

      Case Else
        Return False
    End Select

  End Function

  Public Sub PrepareEditingControlForEdit(ByVal selectAll As Boolean) _
      Implements IDataGridViewEditingControl.PrepareEditingControlForEdit

    ' No preparation needs to be done.

  End Sub

  Public ReadOnly Property RepositionEditingControlOnValueChange() _
      As Boolean Implements _
      IDataGridViewEditingControl.RepositionEditingControlOnValueChange

    Get
      Return False
    End Get

  End Property

  Public Property EditingControlDataGridView() As DataGridView _
      Implements IDataGridViewEditingControl.EditingControlDataGridView

    Get
      Return dataGridViewControl
    End Get
    Set(ByVal value As DataGridView)
      dataGridViewControl = value
    End Set

  End Property

  Public Property EditingControlValueChanged() As Boolean _
      Implements IDataGridViewEditingControl.EditingControlValueChanged

    Get
      Return valueIsChanged
    End Get
    Set(ByVal value As Boolean)
      valueIsChanged = value
    End Set

  End Property

  Public ReadOnly Property EditingControlCursor() As Cursor _
      Implements IDataGridViewEditingControl.EditingPanelCursor

    Get
      Return MyBase.Cursor
    End Get

  End Property

  Protected Overrides Sub OnValueChanged(ByVal eventargs As EventArgs)

    ' Notify the DataGridView that the contents of the cell have changed.
    valueIsChanged = True
    Me.EditingControlDataGridView.NotifyCurrentCellDirty(True)
    MyBase.OnValueChanged(eventargs)

  End Sub

End Class

