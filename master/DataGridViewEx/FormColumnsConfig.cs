using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataGridViewEx
{
  public partial class FormColumnsConfig : Form
  {


    /// <summary>
    /// Class used to store DataGridView column info
    /// </summary>
    private class ItemData
    {

      private string _columnName;

      private string _Text;
      public ItemData(string columnName, string text)
      {
        _columnName = columnName;
        _Text = text;
      }

      public string ColumnName
      {
        get { return _columnName; }
        set { _columnName = value; }
      }

      public string Text
      {
        get { return _Text; }
        set { _Text = value; }
      }

      public override string ToString()
      {
        return _Text;
      }

    }

    private DataGridViewEx _dataGridView;
    private bool _loading;

    public FormColumnsConfig(DataGridViewEx dataGridView)
    {
      InitializeComponent();
      _dataGridView = dataGridView;
    }

    private void FormColumnsConfig_Load(object sender, EventArgs e)
    {
      _loading = true;
      //*** Add dumies items
      for (int i = 0; i <= _dataGridView.Columns.Count - 1; i++)
      {
        lstColumns.Items.Add("", false);
      }
      //*** Fill in the real items at the right place according to displayIndex propertie
      for (int i = 0; i <= _dataGridView.Columns.Count - 1; i++)
      {
        ItemData item = new ItemData(_dataGridView.Columns[i].Name, _dataGridView.Columns[i].HeaderText);
        lstColumns.Items[_dataGridView.Columns[i].DisplayIndex] = item;
        lstColumns.SetItemChecked(i, _dataGridView.Columns[i].Visible);
      }
      if (lstColumns.Items.Count > 0)
        lstColumns.SelectedIndex = 0;
      _loading = false;

    }

    private void btnUp_Click(object sender, EventArgs e)
    {
      if (lstColumns.SelectedIndex == 0)
        return;

      int index = lstColumns.SelectedIndex;
      ItemData item = (ItemData)lstColumns.SelectedItem;
      lstColumns.Items.Insert(index - 1, lstColumns.SelectedItem);
      lstColumns.SetItemChecked(index - 1, lstColumns.GetItemChecked(index + 1));
      lstColumns.Items.RemoveAt(index + 1);
      lstColumns.SelectedIndex = index - 1;
      _dataGridView.Columns[item.ColumnName].DisplayIndex = lstColumns.SelectedIndex;

    }

    private void btnDown_Click(object sender, EventArgs e)
    {
      if (lstColumns.SelectedIndex != -1 & lstColumns.SelectedIndex == lstColumns.Items.Count - 1)
        return;

      int index = lstColumns.SelectedIndex;
      ItemData item = (ItemData)lstColumns.SelectedItem;
      lstColumns.Items.Insert(index + 2, lstColumns.SelectedItem);
      lstColumns.SetItemChecked(index + 2, lstColumns.GetItemChecked(index));
      lstColumns.Items.RemoveAt(index);
      lstColumns.SelectedIndex = index + 1;
      _dataGridView.Columns[item.ColumnName].DisplayIndex = lstColumns.SelectedIndex;

    }

    private void btnRestore_Click(object sender, EventArgs e)
    {
      lstColumns.Items.Clear();
      //*** Fill in the items
      for (int i = 0; i <= _dataGridView.Columns.Count - 1; i++)
      {
        ItemData item = new ItemData(_dataGridView.Columns[i].Name, _dataGridView.Columns[i].HeaderText);
        lstColumns.Items.Add(item);
        lstColumns.SetItemChecked(i, true);
        _dataGridView.Columns[item.ColumnName].DisplayIndex = _dataGridView.Columns[item.ColumnName].Index;
        _dataGridView.Columns[item.ColumnName].Visible = true;
      }
      if (lstColumns.Items.Count > 0)
        lstColumns.SelectedIndex = 0;

    }

    #region CheckBox click methods
    //*** https://stackoverflow.com/questions/1877800/how-do-i-checkonclick-in-a-checkedlistbox-but-only-when-over-the-checkbox
    private bool MyCheckInCheckbox = false;
    /// <summary>
    /// Only change the checkbox value when clicking on the box
    /// </summary>
    /// <value></value>
    /// <returns></returns>
    /// <remarks></remarks>
    public bool CheckInCheckbox
    {
      get { return MyCheckInCheckbox; }
      set { MyCheckInCheckbox = value; }
    }

    private void MyCheckedListBox_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      int border = 1;
      int index = lstColumns.IndexFromPoint(e.Location);
      int idealCheckSize = 0;
      if (CheckInCheckbox)
      {
        if (index != ListBox.NoMatches)
        {
          Rectangle bounds = lstColumns.GetItemRectangle(index);
          if (Application.RenderWithVisualStyles)
          {
            System.Windows.Forms.VisualStyles.CheckBoxState cbState;
            switch (lstColumns.GetItemCheckState(index))
            {
              case CheckState.Checked:
                cbState = System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal;
                break;
              case CheckState.Indeterminate:
                cbState = System.Windows.Forms.VisualStyles.CheckBoxState.MixedNormal;
                break;
              case CheckState.Unchecked:
                cbState = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
                break;
              default:
                throw new Exception("Unexpected Case");
            }
            Graphics g = lstColumns.CreateGraphics();
            idealCheckSize = CheckBoxRenderer.GetGlyphSize(g, cbState).Width;
            g.Dispose();
          }
          int centeringFactor = Math.Max((bounds.Height - idealCheckSize) / 2, 0);
          if (centeringFactor + idealCheckSize > bounds.Height)
          {
            centeringFactor = bounds.Height - idealCheckSize;
          }
          Rectangle box = new Rectangle(bounds.X + border, bounds.Y + centeringFactor, idealCheckSize, idealCheckSize);
          if (RightToLeft == System.Windows.Forms.RightToLeft.Yes)
          {
            box.X = bounds.X + bounds.Width - idealCheckSize - border;
          }
          if (!box.Contains(e.Location))
          {
            lstColumns.SelectedIndex = index;
            lstColumns.SetItemChecked(index, !lstColumns.GetItemChecked(index));
          }
        }
      }
    }
    #endregion
  }
}
