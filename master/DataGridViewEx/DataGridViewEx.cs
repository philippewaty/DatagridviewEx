using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace DataGridViewEx
{
  public class DataGridViewEx : DataGridView
  {
    #region "Enum"

    /// <summary>
    /// Indicate how the external ContextMenuStrip will be merged with the internal
    /// </summary>
    public enum ContextMenuStripMergingEnum
    {
      InsideMenuOnly = 0,
      ExternalMenuOnly,
      InsideMenuFirst,
      ExternalMenuFirst
    }

    #endregion

    #region "Properties"

    /// <summary>
    /// Indicates if the user has right clicked and that the menu has been merged or not.
    /// </summary>
    private bool _ContextMenuMerged = false;

    [Description("Display lines number"), Category("Design"), DefaultValue(true)]
    public bool DisplayLinesNumber { get; set; }

    /// <summary>
    /// Key used to start export to CSV
    /// </summary>
    /// <returns></returns>
    [Description("Key used to start export to CSV"), DefaultValue(Keys.F11)]
    public Keys ExportToCSVKey { get; set; }


    /// <summary>
    /// Filename for exporting to CSV
    /// </summary>
    /// <returns></returns>
    [Description("Filename for exporting to CSV"), DefaultValue("")]
    public string CSVFilename { get; set; }

    /// <summary>
    /// Filename for exporting to HTML
    /// </summary>
    /// <returns></returns>
    [Description("Filename for exporting to HTML"), DefaultValue("")]
    public string HTMLFilename { get; set; }

    /// <summary>
    /// Filename for exporting to XLSX
    /// </summary>
    /// <returns></returns>
    [Description("Filename for exporting to XLSX"), DefaultValue("")]
    public string XLSXFilename { get; set; }

    /// <summary>
    /// Field separator for CSV
    /// </summary>
    /// <returns></returns>
    [Description("Field separator for CSV"), DefaultValue(";")]
    public string CSVFieldSeparator { get; set; }

    /// <summary>
    /// Write columns name in CSV/XLSX file
    /// </summary>
    /// <returns></returns>
    [Description("Write columns name in CSV/XLSX file"), DefaultValue(true)]
    public bool ExportWriteColumnHeader { get; set; }

    /// <summary>
    /// Open CSV or XLSX file after export
    /// </summary>
    /// <returns></returns>
    [Description("Open CSV or XLSX file after export"), DefaultValue(true)]
    public bool ExportOpenFileAfter { get; set; }

    /// <summary>
    /// Title for printing
    /// </summary>
    /// <returns></returns>
    [Description("Title for printing"), DefaultValue("")]
    public string PrintTitle { get; set; }

    /// <summary>
    /// Merge context menu mode
    /// </summary>
    [Description("Get/set the mode of ContextMenuStrip is merged"), DefaultValue(ContextMenuStripMergingEnum.ExternalMenuFirst)]
    public ContextMenuStripMergingEnum ContextMenuStripMergeMode { get; set; }

    /// <summary>
    /// Display date on print
    /// </summary>
    /// <returns></returns>
    [Description("Display date on print"), DefaultValue(true)]
    public bool PrintDate { get; set; }

    /// <summary>
    /// XML configuration of DataGridView
    /// </summary>
    /// <returns></returns>
    [Description("XML configuration of DataGridView"), Browsable(false), DefaultValue("")]
    public string Configuration
    {
      get
      {
        StringBuilder sb = new StringBuilder();

        try
        {
          List<ColumnInfo> columns = new List<ColumnInfo>();
          for (int i = 0; i <= this.Columns.Count - 1; i++)
          {
            ColumnInfo column = new ColumnInfo();
            column.Name = this.Columns[i].Name;
            column.DisplayIndex = this.Columns[i].DisplayIndex;
            column.Width = this.Columns[i].Width;
            column.Visible = this.Columns[i].Visible;
            columns.Add(column);
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
        }

        return sb.ToString();
      }
      set
      {
        if (this.Columns == null)
          return;

        try
        {
          List<ColumnInfo> columns = new List<ColumnInfo>();
          using (TextReader reader = new StringReader(value))
          {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ColumnInfo>));
            columns = (List<ColumnInfo>)xmlSerializer.Deserialize(reader);
          }
          foreach (ColumnInfo column in columns)
          {
            if (this.Columns[column.Name] != null)
            {
              this.Columns[column.Name].DisplayIndex = column.DisplayIndex;
              this.Columns[column.Name].Width = column.Width;
              this.Columns[column.Name].Visible = column.Visible;
            }
          }

        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
        }

      }
    }

    [Description("Sets the settings for export and printing the DataGridView"), Browsable(true), Category("Misc")]
    public DGVExportSettings ExportSettings
    {
      get
      {
        if (ExportSettings == null)
          return new DGVExportSettings();
        return ExportSettings;
      }
      set
      {
        if (ExportSettings == null)
          ExportSettings = new DGVExportSettings();
        ExportSettings = value;
      }
    }

    #endregion


  }
}
