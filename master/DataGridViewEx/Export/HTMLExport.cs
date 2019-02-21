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
  internal class HTMLExport
  {
    ///<summary>
    ///Export a DataGridView to HTML file
    ///</summary>
    /// <param name="dgv">The DataGridViewEx</param>
    ///<param name="HTMLFilename">HTML file</param>
    ///<returns>Empty string if no error otherwise returns error message</returns>
    ///<remarks></remarks>
    public string ExportToHTML(DataGridViewEx dgv, string HTMLFilename)
    {
      int columnCount = dgv.Columns.Count - 1;
      int rowsCount = dgv.RowCount - 1;
      string filesFolder;
      string filesFolderName;

      //*** Test si adding column is allowed
      //*** If yes, there is an empty line that we doesn't export
      if (dgv.AllowUserToAddRows)
        rowsCount -= 1;

      filesFolderName = Path.GetFileNameWithoutExtension(HTMLFilename) + "_files";
      filesFolder = Path.Combine(Path.GetDirectoryName(HTMLFilename), filesFolderName);
      try
      {
        //*** Delete HTML file and image folder
        if (File.Exists(HTMLFilename))
        {
          File.Delete(HTMLFilename);
        }
        try
        {
          if (Directory.Exists(filesFolder))
          {
            Directory.Delete(filesFolder, true);
          }
          Directory.CreateDirectory(filesFolder);
        }
        catch (Exception ex)
        {
          Debug.WriteLine(ex.Message);
        }

        //*** Get the columns list according to the displayIndex and visibility = true
        List<string> columnsList = (from column in dgv.Columns.Cast<DataGridViewColumn>() where column.Visible == true orderby column.DisplayIndex select column.Name).ToList();

        StreamWriter writer = new StreamWriter(HTMLFilename, false, System.Text.Encoding.UTF8);
        writer.AutoFlush = true;
        writer.WriteLine(Properties.Resources.HTMLHeader);
        //*** Export headers
        writer.WriteLine("    <table>");
        writer.WriteLine("      <thead>");
        writer.WriteLine("        <tr>");
        foreach (string col in columnsList)
        {
          //*** Add column text
          writer.WriteLine("          <th>{0}</th>", dgv.Columns[col].HeaderText);
        }
        writer.WriteLine("        </tr>");
        writer.WriteLine("      </thead>");

        //*** Export data
        for (int row = 0; row <= rowsCount; row++)
        {
          writer.WriteLine("      <tr>");
          foreach (string col in columnsList)
          {
            writer.Write("        <td>");
            //*** Add data
            if (dgv.Rows[row].Cells[col].Value == null)
            {
              writer.Write(string.Empty);
            }
            else
            {
              if ((dgv.Rows[row].Cells[col]) is DataGridViewCalendarCell)
              {
                //*** Get the date format from the column
                if (string.IsNullOrEmpty(dgv.Columns[col].DefaultCellStyle.Format))
                  writer.Write(dgv.Rows[row].Cells[col].Value?.ToString());
                else
                {
                  DateTime dateResult = DateTime.MinValue;
                  if ((dgv.Rows[row].Cells[col].Value != null) && DateTime.TryParse(dgv.Rows[row].Cells[col].Value.ToString(), out dateResult))
                    writer.Write(dateResult.ToString(dgv.Columns[col].DefaultCellStyle.Format));
                  else
                    writer.Write(string.Empty);
                }
              }
              else if ((dgv.Rows[row].Cells[col]) is DataGridViewPasswordTextBoxCell)
                writer.Write(dgv.Rows[row].Cells[col].FormattedValue.ToString());
              else if ((dgv.Rows[row].Cells[col]) is DataGridViewProgressCell)
              {
                writer.Write(dgv.Rows[row].Cells[col].Value);

                Image cellImage = ((DataGridViewProgressCell)dgv.Rows[row].Cells[col]).GetPaintedCell();
                if (cellImage != null)
                {
                  try
                  {
                    //*** Save image to files folder
                    string imageFilename = $"image{row}-{dgv.Columns[col].DisplayIndex}.png";
                    cellImage.Save(System.IO.Path.Combine(filesFolder, imageFilename), System.Drawing.Imaging.ImageFormat.Png);
                  }
                  catch (Exception ex)
                  {
                    Debug.Print(ex.Message);
                  }
                }
              }
              else if ((dgv.Rows[row].Cells[col]) is DataGridViewImageCell)
              {
                //*** http://www.codeproject.com/Articles/680421/Create-Read-Edit-Advance-Excel-Report-in
                Image cellImage = (Image)dgv.Rows[row].Cells[col].FormattedValue;
                if (cellImage != null)
                {
                  try
                  {
                    //*** Save image to files folder
                    string imageFilename = $"image{row}-{dgv.Columns[col].DisplayIndex}.png";
                    cellImage.Save(Path.Combine(filesFolder, imageFilename), System.Drawing.Imaging.ImageFormat.Png);
                    writer.Write("<img src=\"" + Path.Combine(filesFolderName, imageFilename) + "\"/>");
                  }
                  catch (Exception ex)
                  {
                    Debug.Print(ex.Message);
                  }
                }
              }
              else
                writer.Write(dgv.Rows[row].Cells[col].Value);
            }
            writer.WriteLine("</td>");
          }
          writer.WriteLine("      </tr>");
        }
        writer.WriteLine(Properties.Resources.HTMLFooter);
        writer.Flush();
        writer.Close();

        if (System.IO.File.Exists(HTMLFilename))
        {
          Process myProcess = new Process();

          myProcess.StartInfo.FileName = HTMLFilename;
          myProcess.Start();
        }

      }
      catch (Exception ex)
      {
        return ex.Message;
      }
      return string.Empty;
    }
  }
}
