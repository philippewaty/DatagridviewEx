namespace DataGridViewEx
{
  partial class FormColumnsConfig
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormColumnsConfig));
      this.btnRestore = new System.Windows.Forms.Button();
      this.btnClose = new System.Windows.Forms.Button();
      this.btnDown = new System.Windows.Forms.Button();
      this.btnUp = new System.Windows.Forms.Button();
      this.lstColumns = new System.Windows.Forms.CheckedListBox();
      this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.SuspendLayout();
      // 
      // btnRestore
      // 
      this.btnRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnRestore.Image = global::DataGridViewEx.Properties.Resources.recur;
      this.btnRestore.Location = new System.Drawing.Point(243, 78);
      this.btnRestore.Name = "btnRestore";
      this.btnRestore.Size = new System.Drawing.Size(27, 27);
      this.btnRestore.TabIndex = 3;
      this.ToolTip1.SetToolTip(this.btnRestore, "Restore order and visibility");
      this.btnRestore.UseVisualStyleBackColor = true;
      this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnClose.Image = global::DataGridViewEx.Properties.Resources.fileclose;
      this.btnClose.Location = new System.Drawing.Point(243, 199);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(27, 27);
      this.btnClose.TabIndex = 4;
      this.btnClose.UseVisualStyleBackColor = true;
      // 
      // btnDown
      // 
      this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnDown.Image = global::DataGridViewEx.Properties.Resources.down;
      this.btnDown.Location = new System.Drawing.Point(243, 45);
      this.btnDown.Name = "btnDown";
      this.btnDown.Size = new System.Drawing.Size(27, 27);
      this.btnDown.TabIndex = 2;
      this.ToolTip1.SetToolTip(this.btnDown, "Move down");
      this.btnDown.UseVisualStyleBackColor = true;
      this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
      // 
      // btnUp
      // 
      this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnUp.Image = global::DataGridViewEx.Properties.Resources.up;
      this.btnUp.Location = new System.Drawing.Point(243, 12);
      this.btnUp.Name = "btnUp";
      this.btnUp.Size = new System.Drawing.Size(27, 27);
      this.btnUp.TabIndex = 1;
      this.ToolTip1.SetToolTip(this.btnUp, "Move up");
      this.btnUp.UseVisualStyleBackColor = true;
      this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
      // 
      // lstColumns
      // 
      this.lstColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lstColumns.CheckOnClick = true;
      this.lstColumns.FormattingEnabled = true;
      this.lstColumns.Location = new System.Drawing.Point(12, 12);
      this.lstColumns.Name = "lstColumns";
      this.lstColumns.Size = new System.Drawing.Size(225, 214);
      this.lstColumns.TabIndex = 0;
      this.lstColumns.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MyCheckedListBox_MouseClick);
      // 
      // FormColumnsConfig
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.CancelButton = this.btnClose;
      this.ClientSize = new System.Drawing.Size(282, 232);
      this.Controls.Add(this.btnRestore);
      this.Controls.Add(this.btnClose);
      this.Controls.Add(this.btnDown);
      this.Controls.Add(this.btnUp);
      this.Controls.Add(this.lstColumns);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FormColumnsConfig";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Columns configuration";
      this.Load += new System.EventHandler(this.FormColumnsConfig_Load);
      this.ResumeLayout(false);

    }

    #endregion

    internal System.Windows.Forms.Button btnRestore;
    internal System.Windows.Forms.ToolTip ToolTip1;
    internal System.Windows.Forms.Button btnClose;
    internal System.Windows.Forms.Button btnDown;
    internal System.Windows.Forms.Button btnUp;
    internal System.Windows.Forms.CheckedListBox lstColumns;
  }
}