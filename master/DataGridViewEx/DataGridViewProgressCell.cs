using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DataGridViewEx
{
  //http://stackoverflow.com/questions/4646920/populating-a-datagridview-with-text-and-progressbars

  public class DataGridViewProgressCell : DataGridViewImageCell
  {

    // Used to make custom cell consistent with a DataGridViewImageCell
    static Image emptyImage;
    static DataGridViewProgressCell()
    {
      emptyImage = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
    }

    public DataGridViewProgressCell()
    {
      this.ValueType = typeof(int);
    }

    //Private _ColorProgress As Color = Color.FromArgb(203, 235, 108)
    ///' <summary>
    ///' Gets/Sets the color of the progressbar
    ///' </summary>
    ///' <returns></returns>
    //<Browsable(True), Description("Gets/Sets the color of the progressbar"), Category("Appearance"), DefaultValue(GetType(Color), "FFCBEB6C")>
    //Public Property ColorProgress() As Color
    //  Get
    //    Return _ColorProgress
    //  End Get
    //  Set(ByVal value As Color)
    //    _ColorProgress = value
    //  End Set
    //End Property

    //Private _DisplayText As Boolean = True
    ///' <summary>
    ///' Indicates if the text is displayed or not
    ///' </summary>
    ///' <returns></returns>
    //<Browsable(True), Description("Indicates if the text is displayed or not"), Category("Appearance"), DefaultValue(True)>
    //Public Property DisplayText() As Boolean
    //  Get
    //    Return _DisplayText
    //  End Get
    //  Set(ByVal value As Boolean)
    //    _DisplayText = value
    //  End Set
    //End Property

    // Method required to make the Progress Cell consistent with the default Image Cell. 
    // The default Image Cell assumes an Image as a value, although the value of the Progress Cell is an int.
    protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
    {
      return emptyImage;
    }

    protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle,
    DataGridViewPaintParts paintParts)
    {

      try
      {
        DataGridViewProgressColumn MyOwner = (DataGridViewProgressColumn)OwningColumn;

        int progressVal = (int)value;
        float percentage = (progressVal / 100f);
        // Need to convert to float before division; otherwise C# returns int which is 0 for anything but 100%.
        Brush backColorBrush = new SolidBrush(cellStyle.BackColor);
        Brush foreColorBrush = new SolidBrush(cellStyle.ForeColor);
        // Draws the cell grid
        base.Paint(g, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, (paintParts & ~DataGridViewPaintParts.ContentForeground));
        if (percentage > 0.0)
        {
          // Draw the progress bar and the text
          float width = Convert.ToInt32((percentage * cellBounds.Width - 4));
          // Check if value exceed the column width
          if (width > cellBounds.Width - 4)
            width = cellBounds.Width - 4;

          g.FillRectangle(new SolidBrush(MyOwner.ColorProgress), cellBounds.X + 2, cellBounds.Y + 2, width, cellBounds.Height - 4);
          if (MyOwner.DisplayText)
          {
            // draw the text
            g.DrawString(string.Format("{0} %", progressVal.ToString()), cellStyle.Font, foreColorBrush, Convert.ToSingle(cellBounds.X + (cellBounds.Width / 2) - 5), Convert.ToSingle(cellBounds.Y + 2));
          }
        }
        else
        {
          if (MyOwner.DisplayText)
          {
            // draw the text
            if (this.DataGridView.CurrentRow.Index == rowIndex)
            {
              g.DrawString(string.Format("{0} %", progressVal.ToString()), cellStyle.Font, new SolidBrush(cellStyle.SelectionForeColor), cellBounds.X + 6, cellBounds.Y + 2);
            }
            else
            {
              g.DrawString(string.Format("{0} %", progressVal.ToString()), cellStyle.Font, foreColorBrush, cellBounds.X + 6, cellBounds.Y + 2);
            }
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }

    }

    protected internal Bitmap GetPaintedCell()
    {
      System.Drawing.Graphics g = Graphics.FromImage(new Bitmap(OwningColumn.Width, OwningRow.Height));
      System.Drawing.Rectangle cellBounds = this.ContentBounds;
      try
      {
        System.Drawing.Rectangle clipBounds = this.ContentBounds;
        DataGridViewCellStyle cellStyle = this.Style;
        DataGridViewProgressColumn MyOwner = (DataGridViewProgressColumn)OwningColumn;

        if (cellStyle.Font == null)
        {
          cellStyle.Font = MyOwner.InheritedStyle.Font;
        }

        g = Graphics.FromImage(new Bitmap(OwningColumn.Width, OwningRow.Height));

        int progressVal = (int)Value;
        float percentage = (progressVal / 100f);
        // Need to convert to float before division; otherwise C# returns int which is 0 for anything but 100%.
        Brush backColorBrush = new SolidBrush(cellStyle.BackColor);
        Brush foreColorBrush = new SolidBrush(cellStyle.ForeColor);
        // Draws the cell grid

        if (percentage > 0.0)
        {
          // Draw the progress bar and the text
          float width = Convert.ToInt32((percentage * cellBounds.Width - 4));
          // Check if value exceed the column width
          if (width > cellBounds.Width - 4)
            width = cellBounds.Width - 4;

          g.FillRectangle(new SolidBrush(MyOwner.ColorProgress), cellBounds.X + 2, cellBounds.Y + 2, width, cellBounds.Height - 4);
          if (MyOwner.DisplayText)
          {
            // draw the text
            g.DrawString(string.Format("{0} %", progressVal.ToString()), cellStyle.Font, foreColorBrush, Convert.ToSingle(cellBounds.X + (cellBounds.Width / 2) - 5), Convert.ToSingle(cellBounds.Y + 2));
          }
        }
        else
        {
          if (MyOwner.DisplayText)
          {
            // draw the text
            if (this.DataGridView.CurrentRow.Index == RowIndex)
            {
              g.DrawString(string.Format("{0} %", progressVal.ToString()), cellStyle.Font, new SolidBrush(cellStyle.SelectionForeColor), cellBounds.X + 6, cellBounds.Y + 2);
            }
            else
            {
              g.DrawString(string.Format("{0} %", progressVal.ToString()), cellStyle.Font, foreColorBrush, cellBounds.X + 6, cellBounds.Y + 2);
            }
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }

      return new Bitmap(OwningColumn.Width, OwningRow.Height, g);
    }

  }

}
