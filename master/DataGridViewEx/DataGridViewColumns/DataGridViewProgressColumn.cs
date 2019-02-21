//http://stackoverflow.com/questions/4646920/populating-a-datagridview-with-text-and-progressbars
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DataGridViewEx
{
  public class DataGridViewProgressColumn : DataGridViewImageColumn
  {

    public DataGridViewProgressColumn()
    {
      CellTemplate = new DataGridViewProgressCell();
    }


    private Color _ColorProgress = Color.FromArgb(203, 235, 108);
    /// <summary>
    /// Gets/Sets the color of the progressbar
    /// </summary>
    /// <returns></returns>
    [Browsable(true), Description("Gets/Sets the color of the progressbar"), Category("Appearance"), DefaultValue(typeof(Color), "FFCBEB6C")]
    public Color ColorProgress
    {
      get { return _ColorProgress; }
      set { _ColorProgress = value; }
    }

    private bool _DisplayText = true;
    /// <summary>
    /// Indicates if the text is displayed or not
    /// </summary>
    /// <returns></returns>
    [Browsable(true), Description("Indicates if the text is displayed or not"), Category("Behavior"), DefaultValue(true)]
    public bool DisplayText
    {
      get { return _DisplayText; }
      set { _DisplayText = value; }
    }

    //Private _FormatText As String = "{0} %"
    ///' <summary>
    ///' Gets/Sets how the text is formatting ({0} is mandatory to display value)
    ///' </summary>
    ///' <returns></returns>
    //<Browsable(True), Description("Gets/Sets how the text is formatting ({0} is mandatory to display value)"), Category("Behavior"), DefaultValue("{0} %")>
    //Public Property FormatText() As String
    //  Get
    //    Return _FormatText
    //  End Get
    //  Set(ByVal value As String)
    //    _FormatText = value
    //  End Set
    //End Property
  }

}
