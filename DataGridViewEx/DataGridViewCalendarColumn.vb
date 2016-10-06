'http://msdn.microsoft.com/en-us/library/7tas5c80(v=vs.80).aspx
Imports System.ComponentModel

Public Class DataGridViewCalendarColumn
  Inherits DataGridViewColumn

  Public Sub New()
    MyBase.New(New DataGridViewCalendarCell())
  End Sub

  ''' <summary>
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks>
  ''' kevininstructor
  ''' This is needed to persist our custom property DateFormat
  ''' </remarks>
  Public Overrides Function Clone() As Object
    Dim TheCopy As DataGridViewCalendarColumn = DirectCast(MyBase.Clone(), DataGridViewCalendarColumn)
    TheCopy.DateFormat = Me.DateFormat
    Return TheCopy
  End Function

  Public Overrides Property CellTemplate() As DataGridViewCell
    Get
      Return MyBase.CellTemplate
    End Get
    Set(ByVal value As DataGridViewCell)

      ' Ensure that the cell used for the template is a CalendarCell.
      If (value IsNot Nothing) AndAlso
          Not value.GetType().IsAssignableFrom(GetType(DataGridViewCalendarCell)) _
          Then
        Throw New InvalidCastException("Must be a CalendarCell")
      End If
      MyBase.CellTemplate = value

    End Set
  End Property

  Private _DateFormat As String = "dd/MM/yyyy"
  ''' <summary>
  ''' Date time format of the calendar control
  ''' </summary>
  ''' <returns></returns>
  <Category("Behavior"), Description("Date time format of the calendar control"), DefaultValue("dd/MM/yyyy")>
  Public Property DateFormat() As String
    Get
      Return _DateFormat
    End Get
    Set(ByVal value As String)
      _DateFormat = value
      DefaultCellStyle.Format = _DateFormat
    End Set
  End Property

End Class
