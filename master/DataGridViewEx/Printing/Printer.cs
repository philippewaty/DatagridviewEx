using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Printing;
namespace DataGridViewEx
{

  public class Printer
  {

    List<Bitmap> portraitPages = new List<Bitmap>();
    List<Bitmap> landScapePages = new List<Bitmap>();

    List<Bitmap> Pages;
    private PrintDocument document = new PrintDocument();

    int pageIndex = 0;
    int copyIndex = 1;
    bool Collate;
    int Copies;
    bool Landscape;
    int FromPage;
    int ToPage;
    string printDate;

    public Printer()
    {
      document.PrintPage += document_PrintPage;
    }

    //*
    //     * Creates a List of printed page images
    //     * both for use in the preview form and for printing
    //     
    public List<Bitmap> getPages(DataGridView mainTable, bool includeRowHeaders, bool includeColumnHeaders, bool includeHiddenColumns, bool portrait, string Title, bool displayDate)
    {
      mainTable.EndEdit();

      List<Bitmap> pageImages = new List<Bitmap>();

      int startColumn = 0;
      int startRow = 0;
      int lastColumn = -1;
      int lastRow = -1;

      Font ulFont = new Font(mainTable.Font, mainTable.Font.Style | FontStyle.Underline);

      // *** Get the columns list according to the displayIndex and visibility = true
      List<DataGridViewColumn> columnsList = (from column in mainTable.Columns.Cast<DataGridViewColumn>()
                                              where (column.Visible == true && column.Visible != includeHiddenColumns)
                                              orderby column.DisplayIndex
                                              select column).ToList();
      while (true)
      {
        //612, 792
        double sumX = (includeRowHeaders ? mainTable.RowHeadersWidth : 0);
        int textWidth = 0;
        List<xC> xCoordinates = new List<xC>();

        for (int x = startColumn; x <= columnsList.Count - 1; x++)
        {
          if ((sumX + columnsList[x].Width) < (portrait ? 765 : 1060))
          {
            xCoordinates.Add(new xC(Convert.ToInt32(sumX), columnsList[x].Width));
            sumX += columnsList[x].Width;
            if (x == columnsList.Count - 1)
            {
              lastColumn = x;
            }
          }
          else
          {
            lastColumn = x - 1;
            break; // TODO: might not be correct. Was : Exit For
          }
        }

        double sumY = (includeColumnHeaders ? mainTable.ColumnHeadersHeight : 0);
        double titlePosY = 0;
        int columnHeaderPosY = 0;
        int textHeigth = 0;
        Font titleFont = new Font(mainTable.Font.FontFamily, mainTable.Font.Size + 2, FontStyle.Bold | FontStyle.Underline);
        if (!string.IsNullOrEmpty(Title))
        {
          textHeigth = TextRenderer.MeasureText(Title, titleFont).Height;
          textWidth = TextRenderer.MeasureText(Title, titleFont).Width;
          titlePosY = sumY;
          if (includeColumnHeaders)
          {
            columnHeaderPosY = (int)(textHeigth + mainTable.ColumnHeadersHeight + 5);
          }
          sumY += columnHeaderPosY;
        }

        List<yC> yCoordinates = new List<yC>();

        for (int y = startRow; y <= mainTable.RowCount - 1; y++)
        {
          if ((sumY + mainTable.Rows[y].Height) < (portrait ? 1015 : 765))
          {
            yCoordinates.Add(new yC(Convert.ToInt32(sumY), mainTable.Rows[y].Height));
            sumY += mainTable.Rows[y].Height;
            if (y == mainTable.RowCount - 1)
            {
              lastRow = y;
            }
          }
          else
          {
            lastRow = y - 1;
            break;
          }
        }

        Bitmap img = null;
        if (portrait)
        {
          img = new Bitmap(850, 1100);
        }
        else
        {
          img = new Bitmap(1100, 850);
        }
        Graphics g = Graphics.FromImage(img);

        g.Clear(Color.White);
        g.TranslateTransform(20, 20);

        g.DrawString(Title, titleFont, Brushes.Black, Convert.ToSingle((img.Width - textWidth) / 2) - 20, (float)titlePosY);
        // get metrics from the graphics
        //Dim metrics As SizeF = g2d.getFontMetrics(mainTable.getFont())
        //Dim height As Integer = metrics.getHeight()
        StringFormat sf = new StringFormat
        {
          Alignment = StringAlignment.Near,
          LineAlignment = StringAlignment.Center
        };

        SolidBrush brush = new SolidBrush(mainTable.ColumnHeadersDefaultCellStyle.BackColor);

        if (includeColumnHeaders)
        {
          for (int x = startColumn; x <= lastColumn; x++)
          {
            Rectangle r = new Rectangle(xCoordinates[x - startColumn].X, columnHeaderPosY, xCoordinates[x - startColumn].Width, mainTable.ColumnHeadersHeight);
            g.FillRectangle(brush, r);
            g.DrawRectangle(Pens.DarkGray, r);

            r.Inflate(-2, -2);
            g.DrawString(columnsList[x].HeaderText, mainTable.Font, Brushes.Black, r, sf);
          }
        }

        if (includeRowHeaders)
        {
          for (int y = startRow - 1; y <= lastRow; y++)
          {
            if (y == startRow - 1)
            {
              Rectangle r = new Rectangle(0, columnHeaderPosY, mainTable.RowHeadersWidth, mainTable.ColumnHeadersHeight);
              g.FillRectangle(brush, r);
              g.DrawRectangle(Pens.DarkGray, r);
            }
            else
            {
              Rectangle r = new Rectangle(0, yCoordinates[y - startRow].Y, mainTable.RowHeadersWidth, yCoordinates[y - startRow].Height);
              g.FillRectangle(brush, r);
              g.DrawRectangle(Pens.DarkGray, r);
              r.Inflate(-2, -2);
              object o = mainTable.Rows[y].HeaderCell.Value;
              g.DrawString(o != null ? o.ToString() : "", mainTable.Font, Brushes.Black, r, sf);
            }
          }
        }

        for (int x = startColumn; x <= lastColumn; x++)
        {
          for (int y = startRow; y <= lastRow; y++)
          {
            Rectangle r = new Rectangle(xCoordinates[x - startColumn].X, yCoordinates[y - startRow].Y, xCoordinates[x - startColumn].Width, yCoordinates[y - startRow].Height);
            Rectangle r2 = new Rectangle(r.Left, r.Top, r.Width, r.Height);
            r2.Inflate(-2, -2);

            //DataGridViewCalendarColumn
            //DataGridViewPasswordColumn
            //DataGridViewProgressColumn
            //DataGridViewButtonColumn
            //DataGridViewCheckBoxColumn
            //DataGridViewComboBoxColumn
            //DataGridViewImageColumn
            //DataGridViewLinkColumn
            //DataGridViewTextBoxColumn
            if (mainTable[x, y] is DataGridViewCalendarCell)
            {
              if (!mainTable[x, y].Style.BackColor.Equals(Color.Empty))
              {
                g.FillRectangle(new SolidBrush(mainTable[x, y].Style.BackColor), r);
              }
              else
              {
                if (!mainTable[x, y].OwningColumn.DefaultCellStyle.BackColor.Equals(Color.Empty))
                {
                  g.FillRectangle(new SolidBrush(mainTable[x, y].OwningColumn.DefaultCellStyle.BackColor), r);
                }
                if (!mainTable[x, y].OwningRow.DefaultCellStyle.BackColor.Equals(Color.Empty))
                {
                  g.FillRectangle(new SolidBrush(mainTable[x, y].OwningRow.DefaultCellStyle.BackColor), r);
                }
                if (!mainTable.AlternatingRowsDefaultCellStyle.BackColor.Equals(Color.Empty) && y % 2 == 1)
                {
                  g.FillRectangle(new SolidBrush(mainTable.AlternatingRowsDefaultCellStyle.BackColor), r);
                }
              }

              DataGridViewCalendarCell cel = (DataGridViewCalendarCell)mainTable[x, y];
              string cellValue = "";
              if (mainTable[x, y].Value != null)
              {
                cellValue = mainTable[x, y].Value.ToString();
                if (string.IsNullOrEmpty(cel.OwningColumn.DefaultCellStyle.Format))
                {
                  g.DrawString(cellValue, mainTable.Font, Brushes.Black, r2, sf);
                }
                else
                {
                  DateTime dateResult;
                  if (DateTime.TryParse(cellValue, out dateResult))
                  {
                    g.DrawString(dateResult.ToString(cel.OwningColumn.DefaultCellStyle.Format), mainTable.Font, Brushes.Black, r2, sf);
                  }
                  else
                  {
                    g.DrawString(cellValue, mainTable.Font, Brushes.Black, r2, sf);
                  }
                }
              }

            }
            else if (mainTable[x, y] is DataGridViewPasswordTextBoxCell)
            {
              if (!mainTable[x, y].Style.BackColor.Equals(Color.Empty))
              {
                g.FillRectangle(new SolidBrush(mainTable[x, y].Style.BackColor), r);
              }
              else
              {
                if (!mainTable[x, y].OwningColumn.DefaultCellStyle.BackColor.Equals(Color.Empty))
                {
                  g.FillRectangle(new SolidBrush(mainTable[x, y].OwningColumn.DefaultCellStyle.BackColor), r);
                }
                if (!mainTable[x, y].OwningRow.DefaultCellStyle.BackColor.Equals(Color.Empty))
                {
                  g.FillRectangle(new SolidBrush(mainTable[x, y].OwningRow.DefaultCellStyle.BackColor), r);
                }
                if (!mainTable.AlternatingRowsDefaultCellStyle.BackColor.Equals(Color.Empty) && y % 2 == 1)
                {
                  g.FillRectangle(new SolidBrush(mainTable.AlternatingRowsDefaultCellStyle.BackColor), r);
                }
              }

              string cellValue = "";
              if (mainTable[x, y].Value != null)
              {
                cellValue = mainTable[x, y].FormattedValue.ToString();
                g.DrawString(cellValue, mainTable.Font, Brushes.Black, r2, sf);
              }
            }
            else if (mainTable[x, y] is DataGridViewTextBoxCell)
            {
              if (!mainTable[x, y].Style.BackColor.Equals(Color.Empty))
              {
                g.FillRectangle(new SolidBrush(mainTable[x, y].Style.BackColor), r);
              }
              else
              {
                if (!mainTable[x, y].OwningColumn.DefaultCellStyle.BackColor.Equals(Color.Empty))
                {
                  g.FillRectangle(new SolidBrush(mainTable[x, y].OwningColumn.DefaultCellStyle.BackColor), r);
                }
                if (!mainTable[x, y].OwningRow.DefaultCellStyle.BackColor.Equals(Color.Empty))
                {
                  g.FillRectangle(new SolidBrush(mainTable[x, y].OwningRow.DefaultCellStyle.BackColor), r);
                }
                if (!mainTable.AlternatingRowsDefaultCellStyle.BackColor.Equals(Color.Empty) && y % 2 == 1)
                {
                  g.FillRectangle(new SolidBrush(mainTable.AlternatingRowsDefaultCellStyle.BackColor), r);
                }
              }

              string cellValue = "";
              if (mainTable[x, y].Value != null)
              {
                cellValue = mainTable[x, y].Value.ToString();
                g.DrawString(cellValue, mainTable.Font, Brushes.Black, r2, sf);
              }
            }
            else if (mainTable[x, y] is DataGridViewLinkCell)
            {
              if (!mainTable[x, y].Style.BackColor.Equals(Color.Empty))
              {
                g.FillRectangle(new SolidBrush(mainTable[x, y].Style.BackColor), r);
              }
              else
              {
                if (!mainTable[x, y].OwningColumn.DefaultCellStyle.BackColor.Equals(Color.Empty))
                {
                  g.FillRectangle(new SolidBrush(mainTable[x, y].OwningColumn.DefaultCellStyle.BackColor), r);
                }
                if (!mainTable[x, y].OwningRow.DefaultCellStyle.BackColor.Equals(Color.Empty))
                {
                  g.FillRectangle(new SolidBrush(mainTable[x, y].OwningRow.DefaultCellStyle.BackColor), r);
                }
                if (!mainTable.AlternatingRowsDefaultCellStyle.BackColor.Equals(Color.Empty) && y % 2 == 1)
                {
                  g.FillRectangle(new SolidBrush(mainTable.AlternatingRowsDefaultCellStyle.BackColor), r);
                }
              }

              string cellValue = "";
              if (mainTable[x, y].Value != null)
              {
                cellValue = mainTable[x, y].Value.ToString();
                Color c = ((DataGridViewLinkCell)mainTable[x, y]).LinkVisited ? Color.Purple : Color.Blue;
                g.DrawString(cellValue, ulFont, new SolidBrush(c), r2, sf);
              }
            }
            else if (mainTable[x, y] is DataGridViewComboBoxCell)
            {
              ComboBoxRenderer.DrawDropDownButton(g, new Rectangle(r.X + r.Width - 16, r.Top, 16, r.Height), System.Windows.Forms.VisualStyles.ComboBoxState.Normal);
              string cellValue = "";
              if (mainTable[x, y].Value != null)
              {
                cellValue = mainTable[x, y].Value.ToString();
                g.DrawString(cellValue, mainTable.Font, Brushes.Black, r2, sf);
              }
            }
            else if (mainTable[x, y] is DataGridViewCheckBoxCell)
            {
              if (!mainTable[x, y].Style.BackColor.Equals(Color.Empty))
              {
                g.FillRectangle(new SolidBrush(mainTable[x, y].Style.BackColor), r);
              }
              else
              {
                if (!mainTable[x, y].OwningColumn.DefaultCellStyle.BackColor.Equals(Color.Empty))
                {
                  g.FillRectangle(new SolidBrush(mainTable[x, y].OwningColumn.DefaultCellStyle.BackColor), r);
                }
                if (!mainTable[x, y].OwningRow.DefaultCellStyle.BackColor.Equals(Color.Empty))
                {
                  g.FillRectangle(new SolidBrush(mainTable[x, y].OwningRow.DefaultCellStyle.BackColor), r);
                }
                if (!mainTable.AlternatingRowsDefaultCellStyle.BackColor.Equals(Color.Empty) && y % 2 == 1)
                {
                  g.FillRectangle(new SolidBrush(mainTable.AlternatingRowsDefaultCellStyle.BackColor), r);
                }
              }

              bool b = false;
              if (bool.TryParse(mainTable[x, y].FormattedValue.ToString(), out b) && b)
              {
                CheckBoxRenderer.DrawCheckBox(g, new Point(r.Left + Convert.ToInt32((r.Width - 12) / 2), r.Top + Convert.ToInt32((r.Height - 12) / 2)), System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal);
              }
              else
              {
                CheckBoxRenderer.DrawCheckBox(g, new Point(r.Left + Convert.ToInt32((r.Width - 12) / 2), r.Top + Convert.ToInt32((r.Height - 12) / 2)), System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
              }
            }
            else if (mainTable[x, y] is DataGridViewButtonCell)
            {
              ButtonRenderer.DrawButton(g, r2, System.Windows.Forms.VisualStyles.PushButtonState.Normal);
              sf.Alignment = StringAlignment.Center;
              object o = mainTable[x, y].Value;
              if (o != null)
              {
                g.DrawString(o.ToString(), mainTable.Font, Brushes.Black, r2, sf);
              }
            }
            else if (mainTable[x, y] is DataGridViewProgressCell)
            {
              if (y != mainTable.NewRowIndex)
              {
                if ((mainTable[x, y].Value != null))
                {
                  g.DrawImage((Bitmap)mainTable[x, y].FormattedValue, r);
                }
              }
            }
            else if (mainTable[x, y] is DataGridViewImageCell)
            {
              if (y != mainTable.NewRowIndex)
              {
                if ((mainTable[x, y].Value != null))
                {
                  g.DrawImage((Bitmap)mainTable[x, y].FormattedValue, r);
                }
              }
            }
            g.DrawRectangle(Pens.DarkGray, r);
          }
        }

        string footer = "Page " + (pageImages.Count() + 1).ToString();
        textWidth = TextRenderer.MeasureText(footer, mainTable.Font).Width;
        g.DrawString(footer, mainTable.Font, Brushes.Black, Convert.ToSingle((img.Width - textWidth) / 2) - 20, Convert.ToSingle(img.Height - 85));

        //*** Display date to left
        //g.DrawString(printDate, mainTable.Font, Brushes.Black, xCoordinates[0].X, Convert.ToSingle(img.Height - 85));
        //*** Display date to right
        g.DrawString(printDate, mainTable.Font, Brushes.Black, img.Width - TextRenderer.MeasureText(printDate, mainTable.Font).Width - xCoordinates[0].X, Convert.ToSingle(img.Height - 85));

        pageImages.Add(img);

        if (lastColumn < mainTable.ColumnCount - 1)
        {
          startColumn = lastColumn + 1;
        }
        else
        {
          if (lastRow < mainTable.RowCount - 1)
          {
            startColumn = 0;
            startRow = lastRow + 1;
          }
          else
          {
            break; // TODO: might not be correct. Was : Exit While
          }
        }
      }

      return pageImages;

    }

    public void startPrint(DataGridView mainTable, bool includeRowHeaders, bool includeColumnHeaders, bool includeHiddenColumns, bool preview, string JobName, string Title, bool displayDate)
    {
      printDate = $"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}";
      portraitPages = getPages(mainTable, includeRowHeaders, includeColumnHeaders, includeHiddenColumns, true, Title, displayDate);
      landScapePages = getPages(mainTable, includeRowHeaders, includeColumnHeaders, includeHiddenColumns, false, Title, displayDate);

      document.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
      document.OriginAtMargins = true;
      document.DocumentName = JobName;

      altPrintDialog frm = new altPrintDialog(document, portraitPages.Count, landScapePages.Count);

      if (frm.ShowDialog() == DialogResult.OK)
      {
        Collate = document.PrinterSettings.Collate;
        Copies = document.PrinterSettings.Copies;
        Landscape = document.DefaultPageSettings.Landscape;
        FromPage = document.PrinterSettings.FromPage;
        ToPage = document.PrinterSettings.ToPage;
        document.PrinterSettings.PrintRange = PrintRange.SomePages;

        pageIndex = FromPage - 1;
        copyIndex = 1;
        if (Landscape)
        {
          Pages = landScapePages;
        }
        else
        {
          Pages = portraitPages;
        }
        //
        if (preview)
        {
          //PrintPreviewDialog ppd = new PrintPreviewDialog();
          CoolPrintPreview.CoolPrintPreviewDialog ppd = new CoolPrintPreview.CoolPrintPreviewDialog
          {
            Document = document,
            WindowState = FormWindowState.Maximized
          };
          ppd.ShowDialog();
        }
        else
        {
          document.Print();
        }
      }

    }


    private void document_PrintPage(object sender, PrintPageEventArgs e)
    {
      e.Graphics.DrawImage(Pages[pageIndex], e.MarginBounds);

      if (!Collate)
      {
        copyIndex += 1;
        if (copyIndex > Copies)
        {
          pageIndex += 1;
          if (pageIndex < ToPage)
          {
            copyIndex = 1;
            e.HasMorePages = true;
          }
        }
        else
        {
          e.HasMorePages = true;
        }
      }
      else
      {
        pageIndex += 1;
        if (pageIndex == ToPage)
        {
          copyIndex += 1;
          pageIndex = FromPage - 1;
          if (copyIndex <= Copies)
          {
            e.HasMorePages = true;
          }
        }
        else
        {
          e.HasMorePages = true;
        }
      }

    }

  }
}
