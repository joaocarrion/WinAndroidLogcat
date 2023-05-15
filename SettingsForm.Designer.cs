namespace AndroidLogcat
{
  partial class SettingsForm
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
      if (disposing && (components != null)) {
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
      label1 = new Label();
      txtADBPath = new TextBox();
      btAdbPathBrowse = new Button();
      okButton = new Button();
      SuspendLayout();
      // 
      // label1
      // 
      label1.AutoSize = true;
      label1.Location = new Point(12, 9);
      label1.Name = "label1";
      label1.Size = new Size(57, 15);
      label1.TabIndex = 0;
      label1.Text = "ADB Path";
      // 
      // txtADBPath
      // 
      txtADBPath.Location = new Point(12, 27);
      txtADBPath.Name = "txtADBPath";
      txtADBPath.Size = new Size(432, 23);
      txtADBPath.TabIndex = 1;
      // 
      // btAdbPathBrowse
      // 
      btAdbPathBrowse.Location = new Point(450, 27);
      btAdbPathBrowse.Name = "btAdbPathBrowse";
      btAdbPathBrowse.Size = new Size(75, 23);
      btAdbPathBrowse.TabIndex = 2;
      btAdbPathBrowse.Text = "Browse...";
      btAdbPathBrowse.UseVisualStyleBackColor = true;
      btAdbPathBrowse.Click += PathBrowse_Click;
      // 
      // okButton
      // 
      okButton.Location = new Point(450, 301);
      okButton.Name = "okButton";
      okButton.Size = new Size(75, 23);
      okButton.TabIndex = 3;
      okButton.Text = "OK";
      okButton.UseVisualStyleBackColor = true;
      okButton.Click += okButton_Click;
      // 
      // SettingsForm
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(537, 336);
      Controls.Add(okButton);
      Controls.Add(btAdbPathBrowse);
      Controls.Add(txtADBPath);
      Controls.Add(label1);
      FormBorderStyle = FormBorderStyle.FixedDialog;
      Name = "SettingsForm";
      Text = "Settings";
      Load += SettingsForm_Load;
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private Label label1;
    private TextBox txtADBPath;
    private Button btAdbPathBrowse;
    private Button okButton;
  }
}