using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
using System.Data;
using System.Reflection;

namespace DataGridViewEx
{
  public class DataGridViewEx : DataGridView
  {
    internal PrintDialog PrintDialog1;
    internal System.Drawing.Printing.PrintDocument PrintDocument1;
    internal PageSetupDialog PageSetupDialog1;
    private ContextMenuStrip ContextMenuStrip1;
    private IContainer components;
    internal ToolStripMenuItem mnuPrint;
    internal ToolStripMenuItem mnuPrintPreview;
    internal ToolStripSeparator ToolStripSeparator1;
    internal ToolStripMenuItem mnuExport;
    internal ToolStripMenuItem mnuExportCSV;
    internal ToolStripMenuItem mnuExportExcel;
    internal ToolStripMenuItem mnuExportHTML;
    internal ToolStripSeparator ToolStripSeparator2;
    internal ToolStripMenuItem mnuColumnSettings;
    Printer printer = new Printer();

    [System.Diagnostics.DebuggerNonUserCode()]
    public DataGridViewEx()
    {
      InitializeComponent();
      this.GetType().InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, this, new object[] { true });
      _ExportSettings = new ExportSettings();
    }

    //[System.Diagnostics.DebuggerNonUserCode()]
    public DataGridViewEx(System.ComponentModel.IContainer container) : base()
    {
      InitializeComponent();
      this.GetType().InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, this, new object[] { true });
      _ExportSettings = new ExportSettings();
      //Required for Windows.Forms Class Composition Designer support
      if ((container != null))
      {
        container.Add(this);
      }

    }

    #region "Enum"

    ///<summary>
    ///Indicate how the external ContextMenuStrip will be merged with the internal
    ///</summary>
    public enum ContextMenuStripMergingEnum
    {
      InsideMenuOnly = 0,
      ExternalMenuOnly,
      InsideMenuFirst,
      ExternalMenuFirst
    }

    #endregion

    #region "Properties"

    ///<summary>
    ///Indicates if the user has right clicked and that the menu has been merged or not.
    ///</summary>
    private bool _ContextMenuMerged = false;

    [Description("Display lines number"), Category("Design"), DefaultValue(true)]
    public bool DisplayLinesNumber { get; set; } = true;

    ///<summary>
    ///Key used to start export to CSV
    ///</summary>
    ///<returns></returns>
    [Description("Key used to start export to CSV"), DefaultValue(Keys.F11)]
    public Keys ExportToCSVKey { get; set; } = Keys.F11;

    ///<summary>
    ///Key used to start export to XLSX
    ///</summary>
    ///<returns></returns>
    [Description("Key used to start export to XLS"), DefaultValue(Keys.F12)]
    public Keys ExportToXLSXKey { get; set; } = Keys.F12;

    ///<summary>
    ///Filename for exporting to CSV
    ///</summary>
    ///<returns></returns>
    [Description("Filename for exporting to CSV"), DefaultValue("")]
    public string CSVFilename { get; set; }

    ///<summary>
    ///Filename for exporting to HTML
    ///</summary>
    ///<returns></returns>
    [Description("Filename for exporting to HTML"), DefaultValue("")]
    public string HTMLFilename { get; set; }

    ///<summary>
    ///Filename for exporting to XLSX
    ///</summary>
    ///<returns></returns>
    [Description("Filename for exporting to XLSX"), DefaultValue("")]
    public string XLSXFilename { get; set; }

    ///<summary>
    ///Field separator for CSV
    ///</summary>
    ///<returns></returns>
    [Description("Field separator for CSV"), DefaultValue(";")]
    public string CSVFieldSeparator { get; set; } = ";";

    ///<summary>
    ///Write columns name in CSV/XLSX file
    ///</summary>
    ///<returns></returns>
    [Description("Write columns name in CSV/XLSX file"), DefaultValue(true)]
    public bool ExportWriteColumnHeader { get; set; } = true;

    ///<summary>
    ///Open CSV, XLSX or HTML file after export
    ///</summary>
    ///<returns></returns>
    [Description("Open CSV, XLSX or HTML file after export"), DefaultValue(true)]
    public bool ExportOpenFileAfter { get; set; } = true;
    //TODO gérer la propriété ExportOpenFileAfter

    ///<summary>
    ///Title for printing
    ///</summary>
    ///<returns></returns>
    [Description("Title for printing"), DefaultValue("")]
    public string PrintTitle { get; set; }

    ///<summary>
    ///Merge context menu mode
    ///</summary>
    [Description("Get/set the mode of ContextMenuStrip is merged"), DefaultValue(ContextMenuStripMergingEnum.ExternalMenuFirst)]
    public ContextMenuStripMergingEnum ContextMenuStripMergeMode { get; set; }

    ///<summary>
    ///Display date on print
    ///</summary>
    ///<returns></returns>
    [Description("Display date on print"), DefaultValue(true)]
    public bool PrintDate { get; set; }

    ///<summary>
    ///XML configuration of DataGridView
    ///</summary>
    ///<returns></returns>
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
            ColumnInfo column = new ColumnInfo
            {
              Name = this.Columns[i].Name,
              DisplayIndex = this.Columns[i].DisplayIndex,
              Width = this.Columns[i].Width,
              Visible = this.Columns[i].Visible
            };
            columns.Add(column);
          }
          //*** Serialize list<ColumnInfo>
          using (TextWriter writer = new StringWriter(sb))
          {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ColumnInfo>));
            xmlSerializer.Serialize(writer, columns);
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

    private static ExportSettings _ExportSettings = new ExportSettings();

    [Description("Sets the settings for export and printing the DataGridView"), Browsable(true), Category("Misc")]
    public ExportSettings ExportSettings
    {
      get
      {
        if (_ExportSettings == null)
          _ExportSettings = new ExportSettings();
        return _ExportSettings;
      }
      set
      {
        if (_ExportSettings == null)
          _ExportSettings = new ExportSettings();
        _ExportSettings = value;
      }
    }

    #endregion

    #region Public methods
    ///<summary>
    ///Clear datagridview lines and columns
    ///</summary>
    ///<param name="clearColumns">Indicate if we clear columns</param>
    ///<remarks></remarks>
    public void Clear(bool clearColumns = false)
    {
      if (clearColumns)
        this.Columns.Clear();
      this.Rows.Clear();
    }

    ///<summary>
    ///Puts/removes readonly on columns from col1 to col2 (-1 pour les 2 = bloc all)
    ///</summary>
    ///<param name="value">TRUE to put in readonly, FALSE to remove readonly</param>
    ///<param name="col1">Start column</param>
    ///<param name="col2">End column</param>
    ///<remarks></remarks>
    public void Lock(bool value, int col1 = -1, int col2 = -1)
    {
      if (col1 == -1)
        col1 = 0;
      if (col2 == -1)
        col2 = this.ColumnCount - 1;
      for (int i = col1; i <= col2; i++)
      {
        this.Columns[i].ReadOnly = value;
      }
    }

    ///<summary>
    ///Prepare a datagridview with an array for the width and an array for columns name
    ///</summary>
    ///<param name="pColWidth">Array for columns width</param>
    ///<param name="pColName">Array for columns name</param>
    ///<remarks></remarks>
    [Obsolete()]
    public void PrepareDGV(ref int[] pColWidth, ref string[] pColName)
    {
      int index;

      this.SuspendLayout();
      this.Clear(true);
      for (int i = 0; i <= pColWidth.Length - 1; i++)
      {
        index = this.Columns.Add($"Column{i}", pColName[i]);
        this.Columns[index].Width = pColWidth[i];
      }
      this.ResumeLayout();

    }

    ///<summary>
    ///Merge the internal contextmenuString with the context menu from the application.
    ///</summary>
    private void ContextMenuStripMerge()
    {
      if (_ContextMenuMerged)
        return;

      //*** First call to ContextMenuStripMerge method
      switch (ContextMenuStripMergeMode)
      {
        case ContextMenuStripMergingEnum.InsideMenuOnly:

          this.ContextMenuStrip = ContextMenuStrip1;
          break;

        case ContextMenuStripMergingEnum.InsideMenuFirst:
          ContextMenuStrip newCtxMenu = new ContextMenuStrip();
          //*** url : http://stackoverflow.com/questions/16760150/transfer-items-between-contextmenustrips-vb-net
          ToolStripItem[] tsiIMF = new ToolStripItem[ContextMenuStrip1.Items.Count - 2];

          this.ContextMenuStrip1.Items.CopyTo(tsiIMF, 0);
          newCtxMenu.Items.AddRange(tsiIMF);
          if (this.ContextMenuStrip != null)
          {
            //*** Add separator only if there is a ContextMenuStrip
            this.ContextMenuStrip.Items.Add("-");
          }
          else
          {
            //*** Initialize ContextMenuStrip
            this.ContextMenuStrip = new ContextMenuStrip();
          }
          Array.Resize<ToolStripItem>(ref tsiIMF, this.ContextMenuStrip.Items.Count);
          this.ContextMenuStrip.Items.CopyTo(tsiIMF, 0);
          newCtxMenu.Items.AddRange(tsiIMF);

          this.ContextMenuStrip = newCtxMenu;
          break;

        case ContextMenuStripMergingEnum.ExternalMenuOnly:
          //*** Do nothing with the internal menu
          break;

        case ContextMenuStripMergingEnum.ExternalMenuFirst:
          if (this.ContextMenuStrip != null)
          {
            //*** Add separator only if there is a ContextMenuStrip
            this.ContextMenuStrip.Items.Add("-");
          }
          else
          {
            //*** Initialize ContextMenuStrip
            this.ContextMenuStrip = new ContextMenuStrip();
          }
          ToolStripItem[] tsiEMF = new ToolStripItem[ContextMenuStrip1.Items.Count - 2];

          this.ContextMenuStrip1.Items.CopyTo(tsiEMF, 0);
          ContextMenuStrip.Items.AddRange(tsiEMF);
          break;

        default:
          throw new Exception("Unexpected Case");
      }
      _ContextMenuMerged = true;
    }

    ///<summary>
    ///Show/hide a column
    ///</summary>
    ///<param name="columnIndex">Column index</param>
    ///<param name="value">TRUE : hide or FALSE : show</param>
    ///<remarks></remarks>
    public void HideColumn(Int32 columnIndex, bool value)
    {
      this.Columns[columnIndex].Visible = !value;
    }

    //Public Function AddComboBoxColumn(ByVal HeaderText As String, ByVal DisplayStyle As DataGridViewComboBoxDisplayStyle, ByVal ValueType As Type, ByVal Items() As Object, Optional ByVal ColumnIndex As Integer = -1, Optional ByVal DataPropertyName As String = "") As Integer
    public int AddComboBoxColumn(string columnName, string HeaderText, DataGridViewComboBoxDisplayStyle DisplayStyle, object[] Items, int ColumnIndex = -1, string DataPropertyName = "", int Width = -1)
    {
      //http://fikou-sama.freevar.com/mes-articles.php?ID=1
      //https://msdn.microsoft.com/en-us/library/system.windows.forms.datagridviewcolumn.valuetype%28v=vs.110%29.aspx
      DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn();
      int index = ColumnIndex;

      col.Name = columnName;
      col.HeaderText = HeaderText;
      col.DisplayStyle = DisplayStyle;
      if (Items != null)
      {
        col.Items.AddRange(Items);
      }
      col.DataPropertyName = DataPropertyName;
      if (Width != -1)
        col.Width = Width;
      if (ColumnIndex != -1)
      {
        this.Columns.Insert(ColumnIndex, col);
      }
      else
      {
        index = this.Columns.Add(col);
      }
      return index;
    }

    public int AddLinkColumn(string columnName, string HeaderText, int ColumnIndex = -1, string DataPropertyName = "", int Width = -1)
    {
      DataGridViewLinkColumn col = new DataGridViewLinkColumn();
      int index = ColumnIndex;

      col.Name = columnName;
      col.HeaderText = HeaderText;
      col.DataPropertyName = DataPropertyName;
      if (Width != -1)
        col.Width = Width;
      if (ColumnIndex != -1)
      {
        this.Columns.Insert(ColumnIndex, col);
      }
      else
      {
        index = this.Columns.Add(col);
      }
      return index;
    }

    public int AddCheckBoxColumn(string columnName, string HeaderText, int ColumnIndex = -1, string DataPropertyName = "", int Width = -1)
    {
      DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn();
      int index = ColumnIndex;

      col.Name = columnName;
      col.HeaderText = HeaderText;
      col.DataPropertyName = DataPropertyName;
      if (Width != -1)
        col.Width = Width;
      if (ColumnIndex != -1)
      {
        this.Columns.Insert(ColumnIndex, col);
      }
      else
      {
        index = this.Columns.Add(col);
      }
      return index;
    }

    public int AddButtonColumn(string columnName, string HeaderText, string ButtonText, int ColumnIndex = -1, int Width = -1)
    {
      DataGridViewButtonColumn col = new DataGridViewButtonColumn();
      int index = ColumnIndex;

      col.Name = columnName;
      col.HeaderText = HeaderText;
      col.Text = ButtonText;
      col.UseColumnTextForButtonValue = true;
      if (Width != -1)
        col.Width = Width;
      if (ColumnIndex != -1)
      {
        this.Columns.Insert(ColumnIndex, col);
      }
      else
      {
        index = this.Columns.Add(col);
      }
      return index;
    }

    public int AddCalendarColumn(string columnName, string HeaderText, int ColumnIndex = -1, string DataPropertyName = "", int Width = -1)
    {
      DataGridViewCalendarColumn col = new DataGridViewCalendarColumn();
      int index = ColumnIndex;

      col.Name = columnName;
      col.HeaderText = HeaderText;
      col.DataPropertyName = DataPropertyName;
      if (Width != -1)
        col.Width = Width;
      if (ColumnIndex != -1)
      {
        this.Columns.Insert(ColumnIndex, col);
      }
      else
      {
        index = this.Columns.Add(col);
      }
      return index;
    }

    public int AddImageColumn(string columnName, string HeaderText, string Description, int bitmapPadding = 6, int ColumnIndex = -1, string DataPropertyName = "", int Width = -1)
    {
      DataGridViewImageColumn col = new DataGridViewImageColumn();
      int index = ColumnIndex;

      col.Name = columnName;
      col.ImageLayout = DataGridViewImageCellLayout.NotSet;
      col.Description = Description;
      col.HeaderText = HeaderText;
      col.DataPropertyName = DataPropertyName;
      if (Width != -1)
        col.Width = Width;
      if (ColumnIndex != -1)
      {
        this.Columns.Insert(ColumnIndex, col);
      }
      else
      {
        index = this.Columns.Add(col);
      }
      return index;
    }

    public int AddPasswordColumn(string columnName, string HeaderText, int ColumnIndex = -1, string DataPropertyName = "", int Width = -1)
    {
      DataGridViewPasswordTextBoxColumn col = new DataGridViewPasswordTextBoxColumn();
      int index = ColumnIndex;

      col.Name = columnName;
      col.HeaderText = HeaderText;
      col.DataPropertyName = DataPropertyName;
      if (Width != -1)
        col.Width = Width;
      if (ColumnIndex != -1)
      {
        this.Columns.Insert(ColumnIndex, col);
      }
      else
      {
        index = this.Columns.Add(col);
      }
      return index;
    }

    public int AddProgressColumn(string columnName, string HeaderText, int ColumnIndex = -1, string DataPropertyName = "", int Width = -1)
    {
      DataGridViewProgressColumn col = new DataGridViewProgressColumn();
      int index = ColumnIndex;

      col.Name = columnName;
      col.HeaderText = HeaderText;
      col.DataPropertyName = DataPropertyName;
      if (Width != -1)
        col.Width = Width;
      if (ColumnIndex != -1)
      {
        this.Columns.Insert(ColumnIndex, col);
      }
      else
      {
        index = this.Columns.Add(col);
      }
      return index;
    }

    public int AddTextBoxColumn(string columnName, string HeaderText, int ColumnIndex = -1, string DataPropertyName = "", int Width = -1)
    {
      DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
      int index = ColumnIndex;

      col.Name = columnName;
      col.HeaderText = HeaderText;
      col.DataPropertyName = DataPropertyName;
      if (Width != -1)
        col.Width = Width;
      if (ColumnIndex != -1)
      {
        this.Columns.Insert(ColumnIndex, col);
      }
      else
      {
        index = this.Columns.Add(col);
      }
      return index;
    }

    public void RemoveColumn(int index)
    {
      this.Columns.RemoveAt(index);
    }

    ///<summary>
    ///Export a DataGridView to CSV file in temporaray folder
    ///</summary>
    ///<returns>Empty string if no error otherwise returns error message</returns>
    ///<remarks></remarks>
    public string ExportToCSV()
    {
      return ExportToCSV(CSVFilename, this.ExportWriteColumnHeader, this.CSVFieldSeparator);
    }

    /// <summary>
    /// Export a DataGridView to CSV file
    /// </summary>
    /// <param name="CSVFileName">CSV file</param>
    /// <param name="WriteColumnHeaderNames">Write header</param>
    /// <param name="DelimiterFormat">Field separator (; or , or ...)</param>
    /// <returns>Empty string if no error otherwise returns error message</returns>
    /// <remarks></remarks>
    public string ExportToCSV(string CSVFileName, bool WriteColumnHeaderNames = false, string DelimiterFormat = ";")
    {
      Export.CSVExport export = new Export.CSVExport();
      return export.ExportToCSV(this, CSVFilename, WriteColumnHeaderNames, DelimiterFormat);
    }

    ///<summary>
    /// Export a DataGridView to XLSX file to temporary folder
    /// </summary>
    /// <returns>Empty string if no error otherwise returns error message</returns>
    /// <remarks></remarks>
    public string ExportToXLSX()
    {
      Export.XLSExport export = new Export.XLSExport();
      return export.ExportToXLSX(this, XLSXFilename, ExportWriteColumnHeader);
    }

    /// <summary>
    /// Export a DataGridView to XLSX file
    /// </summary>
    /// <param name="XLSXFileName">XLSX file</param>
    /// <param name="WriteColumnHeaderNames">Write header</param>
    /// <returns>Empty string if no error otherwise returns error message</returns>
    /// <remarks></remarks>
    public string ExportToXLSX(string XLSXFileName, bool WriteColumnHeaderNames = false)
    {
      Export.XLSExport export = new Export.XLSExport();
      return export.ExportToXLSX(this, XLSXFilename, ExportWriteColumnHeader);
    }

    ///<summary>
    ///Calculate a margin
    ///</summary>
    ///<param name="pixels"></param>
    ///<returns></returns>
    private static int Pixel2MTU(int pixels)
    {
      int mtus = pixels * 9525;
      return mtus;
    }

    ///<summary>
    ///Export a DataGridView to HTML file
    ///</summary>
    ///<param name="HTMLFilename">HTML file</param>
    ///<returns>Empty string if no error otherwise returns error message</returns>
    ///<remarks></remarks>
    public string ExportToHTML(string HTMLFilename)
    {
      Export.HTMLExport export = new Export.HTMLExport();
      return export.ExportToHTML(this, this.HTMLFilename);
    }

    ///<summary>
    ///Allows you to change visibility of columns
    ///</summary>
    ///<url>http://www.extensionmethod.net/csharp/datagridview/datagridview-columns-visibility-configuration-window</url>
    public void ShowConfigurationWindow()
    {
      using (var frmConfig = new FormColumnsConfig(this))
      {
        frmConfig.CheckInCheckbox = true;
        frmConfig.ShowDialog();
      }
    }

    #endregion

    #region "Private methods"

    //http://stackoverflow.com/questions/1805626/how-to-fill-a-datatable-with-a-listof-t-or-convert-a-listof-t-to-a-datatable
    private static DataTable ConvertToDataTable<T>(IList<T> list)
    {
      DataTable table = new DataTable();
      FieldInfo[] fields = typeof(T).GetFields();
      foreach (FieldInfo field in fields)
      {
        table.Columns.Add(field.Name, field.FieldType);
      }
      foreach (T item in list)
      {
        DataRow row = table.NewRow();
        foreach (FieldInfo field in fields)
        {
          row[field.Name] = field.GetValue(item);
        }
        table.Rows.Add(row);
      }
      return table;
    }

    #endregion

    #region "Events"

    private void DataGridViewEx_KeyUp(object sender, KeyEventArgs e)
    {
      string ret = string.Empty;

      if (e.KeyCode == ExportToCSVKey)
      {
        ret = this.ExportToCSV(CSVFilename, ExportWriteColumnHeader, CSVFieldSeparator);
        if (!string.IsNullOrEmpty(ret))
        {
          MessageBox.Show(ret, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return;
      }

      if (e.KeyCode == ExportToXLSXKey)
      {
        Export.XLSExport export = new Export.XLSExport();
        ret = export.ExportToXLSX(this, this.XLSXFilename, this.ExportWriteColumnHeader);
        if (!string.IsNullOrEmpty(ret))
        {
          MessageBox.Show(ret, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return;
      }

      if (e.KeyCode == Keys.C && e.Control)
      {
        DataObject d = this.GetClipboardContent();
        Clipboard.SetDataObject(d);
      }
    }

    //*** http://stackoverflow.com/questions/9581626/show-row-number-in-row-header-of-a-datagridview
    private void DataGridViewEx_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
    {
      if (!DisplayLinesNumber | !RowHeadersVisible)
        return;
      DataGridView grid = sender as DataGridView;
      string rowIdx = (e.RowIndex + 1).ToString();

      //right alignment might actually make more sense for numbers
      StringFormat centerFormat = new StringFormat
      {
        Alignment = StringAlignment.Center,
        LineAlignment = StringAlignment.Center
      };

      Rectangle headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
      e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
    }

    private void mnuColumnSettings_Click(object sender, EventArgs e)
    {
      this.ShowConfigurationWindow();
    }

    private void mnuExportCSV_Click(object sender, EventArgs e)
    {
      using (SaveFileDialog dlg = new SaveFileDialog())
      {
        dlg.Filter = "CSV file (*.csv)|*.csv";
        if (dlg.ShowDialog() == DialogResult.Cancel)
          return;
        this.ExportToCSV(dlg.FileName, true);
      }
    }

    //ERROR: Handles clauses are not supported in C#
    private void mnuExportExcel_Click(object sender, EventArgs e)
    {
      using (SaveFileDialog dlg = new SaveFileDialog())
      {
        dlg.Filter = "Excel file (*.xlsx)|*.xlsx";
        if (dlg.ShowDialog() == DialogResult.Cancel)
          return;

        Export.XLSExport export = new Export.XLSExport();
        export.ExportToXLSX(this, this.XLSXFilename, this.ExportWriteColumnHeader);
      }
    }

    private void mnuPrint_Click(object sender, EventArgs e)
    {
      this.Print(this.Name, PrintTitle, PrintDate, false);
    }

    private void mnuPrintPreview_Click(object sender, EventArgs e)
    {
      this.Print(this.Name, PrintTitle, PrintDate, true);
    }

    private void DataGridViewEx_MouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        if (this.ContextMenuStrip == null)
        {
          if (ContextMenuStripMergeMode != ContextMenuStripMergingEnum.ExternalMenuOnly)
          {
            ContextMenuStripMerge();
            this.ContextMenuStrip.Show(this, e.X, e.Y);
          }
        }
        else
        {
          //*** Merge menu
          ContextMenuStripMerge();
          this.ContextMenuStrip.Show(this, e.X, e.Y);
        }
      }
    }

    private void mnuExportHTML_Click(object sender, EventArgs e)
    {
      using (SaveFileDialog dlg = new SaveFileDialog())
      {
        dlg.Filter = "HTML file (*.html)|*.html";
        if (dlg.ShowDialog() == DialogResult.Cancel)
          return;
        this.ExportToHTML(dlg.FileName);
      }
    }

    #endregion

    public void Print(string JobName, string Title, bool displayDate, bool Preview)
    {
      printer.startPrint(this, true, true, false, Preview, JobName, Title, displayDate);
    }

    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataGridViewEx));
      this.PrintDialog1 = new System.Windows.Forms.PrintDialog();
      this.PrintDocument1 = new System.Drawing.Printing.PrintDocument();
      this.PageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
      this.ContextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuPrint = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuPrintPreview = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuExport = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuExportCSV = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuExportExcel = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuExportHTML = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuColumnSettings = new System.Windows.Forms.ToolStripMenuItem();
      this.ContextMenuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      this.SuspendLayout();
      //
      // PrintDialog1
      //
      this.PrintDialog1.UseEXDialog = true;
      //
      // PrintDocument1
      // 
      this.PrintDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.PrintDocument1_BeginPrint);
      this.PrintDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocument1_PrintPage);
      // 
      // ContextMenuStrip1
      // 
      this.ContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPrint,
            this.mnuPrintPreview,
            this.ToolStripSeparator1,
            this.mnuExport,
            this.ToolStripSeparator2,
            this.mnuColumnSettings});
      this.ContextMenuStrip1.Name = "ContextMenuStrip1";
      this.ContextMenuStrip1.Size = new System.Drawing.Size(171, 104);
      // 
      // mnuPrint
      // 
      this.mnuPrint.Image = global::DataGridViewEx.Properties.Resources.fileprint;
      this.mnuPrint.Name = "mnuPrint";
      this.mnuPrint.Size = new System.Drawing.Size(170, 22);
      this.mnuPrint.Text = "Print";
      this.mnuPrint.Click += new System.EventHandler(this.mnuPrint_Click);
      // 
      // mnuPrintPreview
      // 
      this.mnuPrintPreview.Image = global::DataGridViewEx.Properties.Resources.document_preview;
      this.mnuPrintPreview.Name = "mnuPrintPreview";
      this.mnuPrintPreview.Size = new System.Drawing.Size(170, 22);
      this.mnuPrintPreview.Text = "Print preview";
      this.mnuPrintPreview.Click += new System.EventHandler(this.mnuPrintPreview_Click);
      // 
      // ToolStripSeparator1
      // 
      this.ToolStripSeparator1.Name = "ToolStripSeparator1";
      this.ToolStripSeparator1.Size = new System.Drawing.Size(167, 6);
      // 
      // mnuExport
      // 
      this.mnuExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExportCSV,
            this.mnuExportExcel,
            this.mnuExportHTML});
      this.mnuExport.Name = "mnuExport";
      this.mnuExport.Size = new System.Drawing.Size(170, 22);
      this.mnuExport.Text = "Export";
      // 
      // mnuExportCSV
      // 
      this.mnuExportCSV.Image = global::DataGridViewEx.Properties.Resources.page_excel;
      this.mnuExportCSV.Name = "mnuExportCSV";
      this.mnuExportCSV.Size = new System.Drawing.Size(116, 22);
      this.mnuExportCSV.Text = "CSV...";
      this.mnuExportCSV.Click += new System.EventHandler(this.mnuExportCSV_Click);
      // 
      // mnuExportExcel
      // 
      this.mnuExportExcel.Image = global::DataGridViewEx.Properties.Resources.page_excel;
      this.mnuExportExcel.Name = "mnuExportExcel";
      this.mnuExportExcel.Size = new System.Drawing.Size(116, 22);
      this.mnuExportExcel.Text = "Excel...";
      this.mnuExportExcel.Click += new System.EventHandler(this.mnuExportExcel_Click);
      // 
      // mnuExportHTML
      // 
      this.mnuExportHTML.Image = ((System.Drawing.Image)(resources.GetObject("mnuExportHTML.Image")));
      this.mnuExportHTML.Name = "mnuExportHTML";
      this.mnuExportHTML.Size = new System.Drawing.Size(116, 22);
      this.mnuExportHTML.Text = "HTML...";
      this.mnuExportHTML.Click += new System.EventHandler(this.mnuExportHTML_Click);
      // 
      // ToolStripSeparator2
      // 
      this.ToolStripSeparator2.Name = "ToolStripSeparator2";
      this.ToolStripSeparator2.Size = new System.Drawing.Size(167, 6);
      // 
      // mnuColumnSettings
      // 
      this.mnuColumnSettings.Image = global::DataGridViewEx.Properties.Resources.package_settings1;
      this.mnuColumnSettings.Name = "mnuColumnSettings";
      this.mnuColumnSettings.Size = new System.Drawing.Size(170, 22);
      this.mnuColumnSettings.Text = "Column settings...";
      this.mnuColumnSettings.Click += new System.EventHandler(this.mnuColumnSettings_Click);
      // 
      // DataGridViewEx
      // 
      this.AllowUserToAddRows = false;
      this.AllowUserToDeleteRows = false;
      this.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DataGridViewEx_DataError);
      this.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DataGridViewEx_RowPostPaint);
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DataGridViewEx_KeyUp);
      this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DataGridViewEx_MouseUp);
      this.ContextMenuStrip1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
      this.ResumeLayout(false);

    }

    private void PrintDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

    }

    private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {

    }

    private void DataGridViewEx_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
      return;
    }
  }
}
