using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataGridViewEx
{
  //https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/how-to-host-controls-in-windows-forms-datagridview-cells
  using System.ComponentModel;
  using System.Windows.Forms;

  public class DataGridViewCalendarColumn : DataGridViewColumn
  {

    public DataGridViewCalendarColumn(): base(new DataGridViewCalendarCell())
    {
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// kevininstructor
    /// This is needed to persist our custom property DateFormat
    /// </remarks>
    public override object Clone()
    {
      DataGridViewCalendarColumn TheCopy = (DataGridViewCalendarColumn)base.Clone();
      TheCopy.DateFormat = this.DateFormat;
      return TheCopy;
    }

    public override DataGridViewCell CellTemplate
    {
      get { return base.CellTemplate; }

      set
      {
        // Ensure that the cell used for the template is a CalendarCell.
        if ((value != null) && !value.GetType().IsAssignableFrom(typeof(DataGridViewCalendarCell)))
        {
          throw new InvalidCastException("Must be a CalendarCell");
        }
        base.CellTemplate = value;

      }
    }

    private string _DateFormat = "dd/MM/yyyy";
    /// <summary>
    /// Date time format of the calendar control
    /// </summary>
    /// <returns></returns>
    [Category("Behavior"), Description("Date time format of the calendar control"), DefaultValue("dd/MM/yyyy")]
    public string DateFormat
    {
      get { return _DateFormat; }
      set
      {
        _DateFormat = value;
        DefaultCellStyle.Format = _DateFormat;
      }
    }

  }

}
