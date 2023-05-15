namespace AndroidLogcat
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Drawing;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using System.Windows.Forms;

  public partial class SettingsForm : Form
  {
    private FolderBrowserDialog? folderDialog;
    private string? AdbPath { get => Settings.Instance.GetProperty("adb_path") as string; set => Settings.Instance.SetProperty("adb_path", value); }

    public SettingsForm()
    {
      InitializeComponent();
    }

    private void PathBrowse_Click(object sender, EventArgs e)
    {
      if (folderDialog == null) {
        folderDialog = new FolderBrowserDialog();
        folderDialog.Description = "Select Path";
      }

      folderDialog.InitialDirectory = AdbPath ?? "C:\\Program Files";
      if (folderDialog.ShowDialog() == DialogResult.OK) {
        txtADBPath.Text = folderDialog.SelectedPath;
      }
    }

    private void SettingsForm_Load(object sender, EventArgs e)
    {
      txtADBPath.Text = AdbPath ?? "";
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      Settings.Instance.SetProperty("adb_path", txtADBPath.Text);
      Close();
    }
  }
}
