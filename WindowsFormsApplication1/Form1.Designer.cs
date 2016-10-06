namespace WindowsFormsApplication1
{
  partial class Form1
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      this.dataGridViewEx1 = new DataGridViewEx.DataGridViewEx(this.components);
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewButtonColumn();
      this.Column3 = new DataGridViewEx.DataGridViewCalendarColumn();
      this.Column4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.Column5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
      this.Column6 = new System.Windows.Forms.DataGridViewImageColumn();
      this.Column7 = new System.Windows.Forms.DataGridViewLinkColumn();
      this.Column8 = new DataGridViewEx.DataGridViewPasswordTextBoxColumn();
      this.Column9 = new DataGridViewEx.DataGridViewProgressColumn();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).BeginInit();
      this.SuspendLayout();
      // 
      // dataGridViewEx1
      // 
      this.dataGridViewEx1.AllowUserToAddRows = false;
      this.dataGridViewEx1.AllowUserToDeleteRows = false;
      this.dataGridViewEx1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridViewEx1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9});
      this.dataGridViewEx1.Configuration = resources.GetString("dataGridViewEx1.Configuration");
      this.dataGridViewEx1.ContextMenuStripMergeMode = DataGridViewEx.DataGridViewEx.ContextMenuStripMergingEnum.InsideMenuOnly;
      this.dataGridViewEx1.CSVFilename = null;
      this.dataGridViewEx1.HTMLFilename = null;
      this.dataGridViewEx1.Location = new System.Drawing.Point(23, 13);
      this.dataGridViewEx1.Name = "dataGridViewEx1";
      this.dataGridViewEx1.PrintDate = false;
      this.dataGridViewEx1.PrintTitle = null;
      this.dataGridViewEx1.RowTemplate.Height = 24;
      this.dataGridViewEx1.Size = new System.Drawing.Size(240, 150);
      this.dataGridViewEx1.TabIndex = 0;
      this.dataGridViewEx1.XLSXFilename = null;
      // 
      // Column1
      // 
      this.Column1.HeaderText = "Column1";
      this.Column1.Name = "Column1";
      // 
      // Column2
      // 
      this.Column2.HeaderText = "Column2";
      this.Column2.Name = "Column2";
      // 
      // Column3
      // 
      dataGridViewCellStyle1.Format = "dd/MM/yyyy";
      this.Column3.DefaultCellStyle = dataGridViewCellStyle1;
      this.Column3.HeaderText = "Column3";
      this.Column3.Name = "Column3";
      // 
      // Column4
      // 
      this.Column4.HeaderText = "Column4";
      this.Column4.Name = "Column4";
      // 
      // Column5
      // 
      this.Column5.HeaderText = "Column5";
      this.Column5.Name = "Column5";
      // 
      // Column6
      // 
      this.Column6.HeaderText = "Column6";
      this.Column6.Name = "Column6";
      // 
      // Column7
      // 
      this.Column7.HeaderText = "Column7";
      this.Column7.Name = "Column7";
      // 
      // Column8
      // 
      this.Column8.HeaderText = "Column8";
      this.Column8.Name = "Column8";
      this.Column8.PasswordChar = '\0';
      this.Column8.UseSystemPasswordChar = false;
      // 
      // Column9
      // 
      this.Column9.HeaderText = "Column9";
      this.Column9.Name = "Column9";
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(284, 261);
      this.Controls.Add(this.dataGridViewEx1);
      this.Name = "Form1";
      this.Text = "Form1";
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private DataGridViewEx.DataGridViewEx dataGridViewEx1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewButtonColumn Column2;
    private DataGridViewEx.DataGridViewCalendarColumn Column3;
    private System.Windows.Forms.DataGridViewCheckBoxColumn Column4;
    private System.Windows.Forms.DataGridViewComboBoxColumn Column5;
    private System.Windows.Forms.DataGridViewImageColumn Column6;
    private System.Windows.Forms.DataGridViewLinkColumn Column7;
    private DataGridViewEx.DataGridViewPasswordTextBoxColumn Column8;
    private DataGridViewEx.DataGridViewProgressColumn Column9;
  }
}

