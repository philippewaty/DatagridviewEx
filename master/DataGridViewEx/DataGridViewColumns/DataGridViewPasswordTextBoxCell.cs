using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataGridViewEx
{
  public class DataGridViewPasswordTextBoxCell : System.Windows.Forms.DataGridViewTextBoxCell
  {


    private char editingControlPasswordChar;

    private bool editingControlUseSystemPasswordChar;
    public char PasswordChar { get; set; } = '*';

    public bool UseSystemPasswordChar { get; set; }

    public override object Clone()
    {
      DataGridViewPasswordTextBoxCell copy = (DataGridViewPasswordTextBoxCell)base.Clone();

      copy.PasswordChar = this.PasswordChar;
      copy.UseSystemPasswordChar = this.UseSystemPasswordChar;

      return copy;
    }

    protected override object GetFormattedValue(object value, int rowIndex, ref System.Windows.Forms.DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter valueTypeConverter, System.ComponentModel.TypeConverter formattedValueTypeConverter, System.Windows.Forms.DataGridViewDataErrorContexts context)
    {
      object formattedValue;

      if (this.UseSystemPasswordChar && value != null)
      {
        //Display the system password character in place of each actual character.
        //TODO: Determine the actual system password character instead of hard-coding this value.
        formattedValue = new string(Convert.ToChar(0x25cf), ((string)value).Length);
      }
      else if (this.PasswordChar != char.MinValue && value != null)
      {
        //Display the user-defined password character in place of each actual character.
        formattedValue = new string(this.PasswordChar, ((string)value).Length);
      }
      else
      {
        //Display the value as is.
        formattedValue = base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
      }

      return formattedValue;
    }

    public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle)
    {
      base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

      TextBox txtBox = ((TextBox)this.DataGridView.EditingControl);
      //Remember the current password properties of the editing control.
      this.editingControlPasswordChar = txtBox.PasswordChar;
      this.editingControlUseSystemPasswordChar = txtBox.UseSystemPasswordChar;

      //Set the new password properties of the editing control.
      txtBox.PasswordChar = this.PasswordChar;
      txtBox.UseSystemPasswordChar = this.UseSystemPasswordChar;
    }

    public override void DetachEditingControl()
    {
      base.DetachEditingControl();

      //Reset the old password properties of the editing control.
      TextBox txtBox = ((TextBox)this.DataGridView.EditingControl);
      txtBox.PasswordChar = this.editingControlPasswordChar;
      txtBox.UseSystemPasswordChar = this.editingControlUseSystemPasswordChar;
    }

  }

}
