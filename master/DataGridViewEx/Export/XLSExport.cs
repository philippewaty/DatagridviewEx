using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataGridViewEx.Export
{
  internal class XLSExport
  {

    /// <summary>
    /// Export a DataGridView to XLSX file
    /// </summary>
    /// <param name="dgv">The DataGridViewEx</param>
    /// <param name="XLSXFileName">XLSX file</param>
    /// <param name="WriteColumnHeaderNames">Write header</param>
    /// <returns>Empty string if no error otherwise returns error message</returns>
    /// <remarks></remarks>
    public string ExportToXLSX(DataGridViewEx dgv, string XLSXFileName, bool WriteColumnHeaderNames = false)
    {
      int columnCount = dgv.Columns.Count - 1;
      int rowsCount = dgv.RowCount - 1;

      if (string.IsNullOrEmpty(XLSXFileName))
        XLSXFileName = System.IO.Path.GetTempFileName().Replace(".tmp", ".xlsx");//temp file name , not really sure 100% is unique.

      //*** Test si adding column is allowed
      //*** If yes, there is an empty line that we doesn't export
      if (dgv.AllowUserToAddRows)
        rowsCount -= 1;

      try
      {
        if (System.IO.File.Exists(XLSXFileName))
          System.IO.File.Delete(XLSXFileName);

        using (ExcelPackage pck = new ExcelPackage(new FileInfo(XLSXFileName)))
        {
          ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");

          //*** Get the columns list according to the displayIndex and visibility = true
          List<string> columnsList = (from column in dgv.Columns.Cast<DataGridViewColumn>()
                                      where column.Visible == true
                                      orderby column.DisplayIndex
                                      select column.Name).ToList();

          if (WriteColumnHeaderNames)
          {
            //*** Export headers
            for (int col = 0; col <= columnsList.Count - 1; col++)
            {
              if ((dgv.Columns[col] is DataGridViewProgressCell) && !dgv.ExportSettings.ProgressBarValue)
                continue;
              //*** Add column text
              ws.Cells[1, col + 1].Value = dgv.Columns[columnsList[col]].HeaderText;
            }
            ws.Cells[1, 1, 1, columnsList.Count].AutoFilter = true;
          }

          //*** Export data
          for (int row = 0; row <= rowsCount - 1; row++)
          {
            for (int col = 0; col <= columnsList.Count - 1; col++)
            {
              //*** Add data
              if (dgv.Rows[row].Cells[columnsList[col]].Value == null)
                ws.Cells[row + 2, col + 1].Value = string.Empty;
              else if ((dgv.Rows[row].Cells[columnsList[col]]) is DataGridViewCalendarCell)
              {
                //*** Get the date format from the columnsList(col)
                if (string.IsNullOrEmpty(dgv.Columns[columnsList[col]].DefaultCellStyle.Format))
                  ws.Cells[row + 2, col + 1].Value = dgv.Rows[row].Cells[columnsList[col]].Value?.ToString();
                else
                {
                  DateTime dateResult = DateTime.MinValue;
                  if ((dgv.Rows[row].Cells[columnsList[col]].Value != null) && DateTime.TryParse(dgv.Rows[row].Cells[columnsList[col]].Value.ToString(), out dateResult))
                    ws.Cells[row + 2, col + 1].Value = dateResult.ToString(dgv.Columns[col].DefaultCellStyle.Format);
                  else
                    ws.Cells[row + 2, col + 1].Value = string.Empty;
                }
              }
              else if ((dgv.Rows[row].Cells[columnsList[col]]) is DataGridViewPasswordTextBoxCell)
                ws.Cells[row + 2, col + 1].Value = dgv.Rows[row].Cells[columnsList[col]].FormattedValue.ToString();
              else if ((dgv.Rows[row].Cells[columnsList[col]]) is DataGridViewProgressCell)
              {
                if (dgv.ExportSettings.ProgressBarValue)
                  ws.Cells[row + 2, col + 1].Value = dgv.Rows[row].Cells[columnsList[col]].Value;
              }
              else if ((dgv.Rows[row].Cells[columnsList[col]]) is DataGridViewImageCell)
              {
                //*** http://www.codeproject.com/Articles/680421/Create-Read-Edit-Advance-Excel-Report-in
                Image cellImage = (Image)dgv.Rows[row].Cells[columnsList[col]].FormattedValue;
                if (cellImage != null)
                {
                  OfficeOpenXml.Drawing.ExcelPicture excelImage;
                  excelImage = ws.Drawings.AddPicture("imageC" + col + 1 + "R" + row + 2, cellImage);
                  excelImage.From.Row = row + 1;
                  excelImage.From.Column = col;
                  excelImage.SetSize(cellImage.Width, cellImage.Height);
                  //2x2 px space for better alignment
                  excelImage.From.ColumnOff = Pixel2MTU(2);
                  excelImage.From.RowOff = Pixel2MTU(2);
                }
              }
              else
                ws.Cells[row + 2, col + 1].Value = dgv.Rows[row].Cells[columnsList[col]].Value;
            }
          }

          //*** Put color on header
          using (ExcelRange rng = ws.Cells[1, 1, 1, dgv.Columns.Count])
          {
            rng.Style.Font.Bold = true;
            rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
            rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(79, 129, 189));
            rng.Style.Font.Color.SetColor(System.Drawing.Color.White);
          }

          ws.Cells.AutoFitColumns();

          pck.Save();
        }

        if (System.IO.File.Exists(XLSXFileName))
        {
          Process myProcess = new Process();

          myProcess.StartInfo.FileName = XLSXFileName;
          myProcess.Start();
        }

        return string.Empty;
      }
      catch (Exception ex)
      {
        return ex.Message;
      }
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
  }
}

