using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataGridViewEx.Export
{
  internal class CSVExport
  {
    /// <summary>
    /// Export a DataGridView to CSV file
    /// </summary>
    /// <param name="dgv">The DataGridViewEx</param>
    /// <param name="CSVFileName">CSV file</param>
    /// <param name="WriteColumnHeaderNames">Write header</param>
    /// <param name="DelimiterFormat">Field separator (; or , or ...)</param>
    /// <returns>Empty string if no error otherwise returns error message</returns>
    /// <remarks></remarks>
    public string ExportToCSV(DataGridViewEx dgv, string CSVFileName, bool WriteColumnHeaderNames = false, string DelimiterFormat = ";")
    {
      StreamWriter sr = null;
      string delimiter = DelimiterFormat;
      int columnCount = dgv.Columns.Count - 1;
      int rowsCount = dgv.RowCount - 1;
      string rowData = "";

      if (string.IsNullOrEmpty(CSVFileName))
        CSVFileName = System.IO.Path.GetTempFileName().Replace(".tmp", ".csv");//temp file name , not really sure 100% is unique.
      //*** Test si adding column is allowed
      //*** If yes, there is an empty line that we doesn't export
      if (dgv.AllowUserToAddRows)
        rowsCount -= 1;

      try
      {
        //*** Get the columns list according to the displayIndex and visibility = true
        List<string> columnsList = (from column in dgv.Columns.Cast<DataGridViewColumn>()
                                    where column.Visible == true
                                    orderby column.DisplayIndex
                                    select column.Name).ToList();

        sr = new StreamWriter(CSVFileName, false, System.Text.Encoding.Default);
        sr.AutoFlush = true;
        if (WriteColumnHeaderNames)
        {
          //*** Export headers
          for (int col = 0; col <= columnCount; col++)
          {
            if (!((dgv.Columns[columnsList[col]]) is DataGridViewImageColumn) | (dgv.Columns[columnsList[col]]) is DataGridViewProgressColumn)
            {
              //***Add text in column
              if (dgv.Columns[columnsList[col]].HeaderText.Contains(delimiter))
                rowData += "\"" + dgv.Columns[columnsList[col]].HeaderText + "\"";
              else
                rowData += dgv.Columns[columnsList[col]].HeaderText;
              //*** Test adding field separator
              if (col < dgv.ColumnCount)
                rowData += delimiter;
              else
                rowData += "";
            }
          }
          sr.WriteLine(rowData);
        }

        //*** Export data
        for (int row = 0; row <= rowsCount; row++)
        {
          rowData = "";
          for (int col = 0; col <= columnCount; col++)
          {
            if (!((dgv.Columns[columnsList[col]]) is DataGridViewImageColumn) | (dgv.Columns[columnsList[col]]) is DataGridViewProgressColumn)
            {
              //*** Add data
              if (dgv.Rows[row].Cells[columnsList[col]].Value == null)
                rowData += "";
              else if ((dgv.Rows[row].Cells[columnsList[col]]) is DataGridViewCalendarCell)
              {
                //*** Get the date format from the column
                if (string.IsNullOrEmpty(dgv.Columns[col].DefaultCellStyle.Format))
                  rowData += dgv.Rows[row].Cells[columnsList[col]].Value?.ToString();
                else
                {
                  DateTime dateResult = DateTime.MinValue;
                  if ((dgv.Rows[row].Cells[columnsList[col]].Value != null) && DateTime.TryParse(dgv.Rows[row].Cells[columnsList[col]].Value.ToString(), out dateResult))
                    rowData += dateResult.ToString(dgv.Columns[col].DefaultCellStyle.Format);
                  else
                    rowData += string.Empty;
                }
              }
              else if ((dgv.Rows[row].Cells[columnsList[col]]) is DataGridViewPasswordTextBoxCell)
                rowData += dgv.Rows[row].Cells[columnsList[col]].FormattedValue.ToString();
              else if (dgv.Rows[row].Cells[columnsList[col]].Value != null && dgv.Rows[row].Cells[columnsList[col]].Value.ToString().Contains(delimiter))
                rowData += "\"" + dgv.Rows[row].Cells[columnsList[col]].Value?.ToString() + "\"";
              else
                rowData += dgv.Rows[row].Cells[columnsList[col]].Value?.ToString();
              //*** Test adding field separator
              if (col < dgv.ColumnCount)
                rowData += delimiter;
              else
                rowData += "";
            }
          }
          if (!string.IsNullOrEmpty(rowData))
            sr.WriteLine(rowData);
        }
        sr.Close();

        if (System.IO.File.Exists(CSVFileName))
        {
          Process myProcess = new Process();

          myProcess.StartInfo.FileName = CSVFileName;
          myProcess.Start();
        }

        return string.Empty;
      }
      catch (Exception ex)
      {
        if (sr != null)
          sr.Close();
        return ex.Message;
      }

      finally
      {
        //*** Cleaning
        if (sr != null)
          sr.Dispose();
        sr = null;
      }
    }
  }
}
