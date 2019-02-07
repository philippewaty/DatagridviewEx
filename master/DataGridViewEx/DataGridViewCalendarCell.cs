using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataGridViewEx
{

  public class DataGridViewCalendarCell : DataGridViewTextBoxCell
  {

    public DateTime MinDate { get; set; }

    public DateTime MaxDate { get; set; }
    
    public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
    {
      // Set the value of the editing control to the current cell value.
      base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

      CalendarEditingControl ctl = (CalendarEditingControl)DataGridView.EditingControl;
      ctl.Value = (DateTime)this.Value;
      if ((MinDate > ctl.MaxDate))
        ctl.MaxDate = DateTimePicker.MaximumDateTime;
      if ((MinDate < DateTimePicker.MinimumDateTime))
        MinDate = DateTimePicker.MinimumDateTime;
      if ((MaxDate > DateTimePicker.MaximumDateTime))
        MaxDate = DateTimePicker.MaximumDateTime;
      if ((MaxDate < ctl.MinDate))
        MaxDate = DateTimePicker.MaximumDateTime;
      ctl.MinDate = MinDate;
      ctl.MaxDate = MaxDate;

      DataGridViewCalendarColumn MyOwner = (DataGridViewCalendarColumn)OwningColumn;
      this.Style.Format = MyOwner.DateFormat;
      ctl.Format = DateTimePickerFormat.Custom;
      ctl.CustomFormat = MyOwner.DateFormat;
    }

    public override Type EditType
    {
      // Return the type of the editing contol that CalendarCell uses.
      get { return typeof(CalendarEditingControl); }
    }

    public override Type ValueType
    {
      // Return the type of the value that CalendarCell contains.
      get { return typeof(DateTime); }
    }

    public override object DefaultNewRowValue
    {
      // Use the current date and time as the default value.
      get { return DateTime.Now; }
    }

  }

  class CalendarEditingControl : DateTimePicker, IDataGridViewEditingControl
  {

    private DataGridView dataGridViewControl;
    private bool valueIsChanged = false;

    private int rowIndexNum;
    public CalendarEditingControl()
    {
      //Me.Format = DateTimePickerFormat.Short
      this.Format = DateTimePickerFormat.Custom;
    }

    public object EditingControlFormattedValue
    {

      get { return this.Value.ToString(this.CustomFormat); }

      set
      {
        if (value is string)
        {
          this.Value = DateTime.Parse((string)value);
        }
      }
    }


    public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
    {

      return this.Value.ToString(this.CustomFormat);

    }


    public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
    {
      this.Font = dataGridViewCellStyle.Font;
      this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
      this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;

    }

    public int EditingControlRowIndex
    {

      get { return rowIndexNum; }
      set { rowIndexNum = value; }
    }


    public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
    {
      // Let the DateTimePicker handle the keys listed.
      switch (key & Keys.KeyCode)
      {
        case Keys.Left:
        case Keys.Up:
        case Keys.Down:
        case Keys.Right:
        case Keys.Home:
        case Keys.End:
        case Keys.PageDown:
        case Keys.PageUp:
          return true;
        default:
          return false;
      }

    }

    public void PrepareEditingControlForEdit(bool selectAll)
    {
      // No preparation needs to be done.

    }

    public bool RepositionEditingControlOnValueChange
    {

      get { return false; }
    }


    public DataGridView EditingControlDataGridView
    {

      get { return dataGridViewControl; }
      set { dataGridViewControl = value; }
    }


    public bool EditingControlValueChanged
    {

      get { return valueIsChanged; }
      set { valueIsChanged = value; }
    }


    public Cursor EditingControlCursor
    {

      get { return base.Cursor; }
    }

    // Implements the IDataGridViewEditingControl
    // .EditingPanelCursor property.
    public Cursor EditingPanelCursor
    {
      get
      {
        return base.Cursor;
      }
    }

    protected override void OnValueChanged(EventArgs eventargs)
    {
      // Notify the DataGridView that the contents of the cell have changed.
      valueIsChanged = true;
      this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
      base.OnValueChanged(eventargs);

    }

  }


}
