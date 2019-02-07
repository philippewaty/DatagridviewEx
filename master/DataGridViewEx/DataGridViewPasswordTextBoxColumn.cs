using System.ComponentModel;
using System.Windows.Forms;

namespace DataGridViewEx
{

  public class DataGridViewPasswordTextBoxColumn : System.Windows.Forms.DataGridViewColumn
  {

    private char _passwordChar;

    private bool _useSystemPasswordChar;
    [Category("Password")]
    public char PasswordChar
    {
      get { return this._passwordChar; }
      set
      {
        if (this._passwordChar != value)
        {
          this._passwordChar = value;

          DataGridViewPasswordTextBoxCell cell = this.CellTemplate as DataGridViewPasswordTextBoxCell;

          if (cell != null)
          {
            //Update the template cell.
            cell.PasswordChar = value;
          }

          if (this.DataGridView != null)
          {
            //Update each existing cell in the column.
            foreach (DataGridViewRow row in this.DataGridView.Rows)
            {
              cell = row.Cells[this.Index] as DataGridViewPasswordTextBoxCell;

              if (cell != null)
              {
                cell.PasswordChar = value;
              }
            }

            //Force a repaint so the grid reflects the current property value.
            this.DataGridView.Refresh();
          }
        }
      }
    }

    [Category("Password")]
    public bool UseSystemPasswordChar
    {
      get { return this._useSystemPasswordChar; }
      set
      {
        if (this._useSystemPasswordChar != value)
        {
          this._useSystemPasswordChar = value;

          DataGridViewPasswordTextBoxCell cell = this.CellTemplate as DataGridViewPasswordTextBoxCell;

          if (cell != null)
          {
            //Update the template cell.
            cell.UseSystemPasswordChar = value;
          }

          if (this.DataGridView != null)
          {
            //Update each existing cell in the column.
            foreach (DataGridViewRow row in this.DataGridView.Rows)
            {
              cell = row.Cells[this.Index] as DataGridViewPasswordTextBoxCell;

              if (cell != null)
              {
                cell.UseSystemPasswordChar = value;
              }
            }

            //Force a repaint so the grid reflects the current property value.
            this.DataGridView.Refresh();
          }
        }
      }
    }

    public DataGridViewPasswordTextBoxColumn(): base(new DataGridViewPasswordTextBoxCell())
    {
    }

    public override object Clone()
    {
      DataGridViewPasswordTextBoxColumn copy = (DataGridViewPasswordTextBoxColumn)base.Clone();

      copy.PasswordChar = this._passwordChar;
      copy.UseSystemPasswordChar = this._useSystemPasswordChar;

      return copy;
    }

  }

}
