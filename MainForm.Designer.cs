namespace AndroidLogcat
{
  partial class MainForm
  {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      components = new System.ComponentModel.Container();
      menuStrip1 = new MenuStrip();
      settingsToolStripMenuItem = new ToolStripMenuItem();
      aboutToolStripMenuItem = new ToolStripMenuItem();
      panel1 = new Panel();
      filterText = new TextBox();
      label3 = new Label();
      btRefresh = new Button();
      cbTag = new ComboBox();
      label2 = new Label();
      btPause = new Button();
      listView = new ListViewNF();
      date = new ColumnHeader();
      pid = new ColumnHeader();
      type = new ColumnHeader();
      tag = new ColumnHeader();
      message = new ColumnHeader();
      cbProcesses = new ComboBox();
      label1 = new Label();
      listContextMenu = new ContextMenuStrip(components);
      copyToolStripMenuItem = new ToolStripMenuItem();
      menuStrip1.SuspendLayout();
      panel1.SuspendLayout();
      listContextMenu.SuspendLayout();
      SuspendLayout();
      // 
      // menuStrip1
      // 
      menuStrip1.Items.AddRange(new ToolStripItem[] { settingsToolStripMenuItem, aboutToolStripMenuItem });
      menuStrip1.Location = new Point(0, 0);
      menuStrip1.Name = "menuStrip1";
      menuStrip1.Size = new Size(1173, 24);
      menuStrip1.TabIndex = 0;
      menuStrip1.Text = "menuStrip1";
      // 
      // settingsToolStripMenuItem
      // 
      settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
      settingsToolStripMenuItem.Size = new Size(61, 20);
      settingsToolStripMenuItem.Text = "&Settings";
      settingsToolStripMenuItem.Click += Settings_Click;
      // 
      // aboutToolStripMenuItem
      // 
      aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      aboutToolStripMenuItem.Size = new Size(52, 20);
      aboutToolStripMenuItem.Text = "&About";
      // 
      // panel1
      // 
      panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      panel1.BorderStyle = BorderStyle.FixedSingle;
      panel1.Controls.Add(filterText);
      panel1.Controls.Add(label3);
      panel1.Controls.Add(btRefresh);
      panel1.Controls.Add(cbTag);
      panel1.Controls.Add(label2);
      panel1.Controls.Add(btPause);
      panel1.Controls.Add(listView);
      panel1.Controls.Add(cbProcesses);
      panel1.Controls.Add(label1);
      panel1.Location = new Point(12, 27);
      panel1.Name = "panel1";
      panel1.Size = new Size(1149, 594);
      panel1.TabIndex = 1;
      // 
      // filterText
      // 
      filterText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      filterText.Location = new Point(808, 12);
      filterText.Name = "filterText";
      filterText.Size = new Size(227, 23);
      filterText.TabIndex = 8;
      filterText.KeyPress += filterText_KeyUp;
      filterText.Leave += filter_TextOutOfFocus;
      // 
      // label3
      // 
      label3.AutoSize = true;
      label3.Location = new Point(769, 15);
      label3.Name = "label3";
      label3.Size = new Size(33, 15);
      label3.TabIndex = 7;
      label3.Text = "Filter";
      // 
      // btRefresh
      // 
      btRefresh.Font = new Font("Wingdings 3", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
      btRefresh.Location = new Point(438, 6);
      btRefresh.Name = "btRefresh";
      btRefresh.Size = new Size(32, 32);
      btRefresh.TabIndex = 6;
      btRefresh.Text = "Q";
      btRefresh.UseVisualStyleBackColor = true;
      btRefresh.Click += btRefresh_Click;
      // 
      // cbTag
      // 
      cbTag.FormattingEnabled = true;
      cbTag.Location = new Point(508, 12);
      cbTag.Name = "cbTag";
      cbTag.Size = new Size(255, 23);
      cbTag.TabIndex = 5;
      cbTag.SelectedIndexChanged += cbTag_SelectedIndexChanged;
      // 
      // label2
      // 
      label2.AutoSize = true;
      label2.Location = new Point(477, 15);
      label2.Name = "label2";
      label2.Size = new Size(25, 15);
      label2.TabIndex = 4;
      label2.Text = "Tag";
      // 
      // btPause
      // 
      btPause.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      btPause.Location = new Point(1041, 6);
      btPause.Name = "btPause";
      btPause.Size = new Size(103, 32);
      btPause.TabIndex = 3;
      btPause.Text = "Pause";
      btPause.UseVisualStyleBackColor = true;
      btPause.Click += btPause_Click;
      // 
      // listView
      // 
      listView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      listView.Columns.AddRange(new ColumnHeader[] { date, pid, type, tag, message });
      listView.FullRowSelect = true;
      listView.Location = new Point(3, 41);
      listView.Name = "listView";
      listView.Size = new Size(1141, 548);
      listView.TabIndex = 2;
      listView.UseCompatibleStateImageBehavior = false;
      listView.View = View.Details;
      listView.MouseClick += listView_MouseClick;
      // 
      // date
      // 
      date.Text = "Date";
      date.Width = 140;
      // 
      // pid
      // 
      pid.Text = "PID";
      // 
      // type
      // 
      type.Text = "Type";
      type.Width = 90;
      // 
      // tag
      // 
      tag.Text = "Tag";
      tag.Width = 100;
      // 
      // message
      // 
      message.Text = "Message";
      message.Width = 420;
      // 
      // cbProcesses
      // 
      cbProcesses.DropDownStyle = ComboBoxStyle.DropDownList;
      cbProcesses.FormattingEnabled = true;
      cbProcesses.Location = new Point(67, 12);
      cbProcesses.Name = "cbProcesses";
      cbProcesses.Size = new Size(365, 23);
      cbProcesses.TabIndex = 1;
      cbProcesses.SelectedIndexChanged += cbProcesses_SelectedIndexChanged;
      // 
      // label1
      // 
      label1.AutoSize = true;
      label1.Location = new Point(3, 15);
      label1.Name = "label1";
      label1.Size = new Size(58, 15);
      label1.TabIndex = 0;
      label1.Text = "Processes";
      // 
      // listContextMenu
      // 
      listContextMenu.Items.AddRange(new ToolStripItem[] { copyToolStripMenuItem });
      listContextMenu.Name = "listContextMenu";
      listContextMenu.Size = new Size(103, 26);
      // 
      // copyToolStripMenuItem
      // 
      copyToolStripMenuItem.Name = "copyToolStripMenuItem";
      copyToolStripMenuItem.Size = new Size(102, 22);
      copyToolStripMenuItem.Text = "&Copy";
      copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;
      // 
      // MainForm
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(1173, 633);
      Controls.Add(panel1);
      Controls.Add(menuStrip1);
      MainMenuStrip = menuStrip1;
      Name = "MainForm";
      Text = "Android Logcat";
      Load += MainForm_Load;
      menuStrip1.ResumeLayout(false);
      menuStrip1.PerformLayout();
      panel1.ResumeLayout(false);
      panel1.PerformLayout();
      listContextMenu.ResumeLayout(false);
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private MenuStrip menuStrip1;
    private ToolStripMenuItem settingsToolStripMenuItem;
    private ToolStripMenuItem aboutToolStripMenuItem;
    private Panel panel1;
    private ComboBox cbProcesses;
    private Label label1;
    private ListViewNF listView;
    private ColumnHeader date;
    private ColumnHeader pid;
    private ColumnHeader type;
    private ColumnHeader tag;
    private ColumnHeader message;
    private Button btPause;
    private ComboBox cbTag;
    private Label label2;
    private ContextMenuStrip listContextMenu;
    private ToolStripMenuItem copyToolStripMenuItem;
    private TextBox filterText;
    private Label label3;
    private Button btRefresh;
  }
}