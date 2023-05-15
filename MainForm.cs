namespace AndroidLogcat
{
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.Diagnostics.Contracts;
  using System.Runtime.CompilerServices;

  public partial class MainForm : Form
  {
    private const string currentProcessProperty = "current_process";
    private const string tagProperty = "tag";
    private SettingsForm? settingsForm;

    private bool paused = false;
    private readonly SemaphoreSlim pauseSem = new SemaphoreSlim(0);
    private readonly SemaphoreSlim tagSelectionChanged = new SemaphoreSlim(0);
    private readonly SemaphoreSlim filterTextChanged = new SemaphoreSlim(0);
    private readonly SemaphoreSlim processTerminated = new SemaphoreSlim(0);

    private List<ADBLogReader.LogLine> lines = new();
    private HashSet<string> tags = new();
    private ADBLogReader.LogLine? selectedLine;
    private ADBLogReader? logReader;

    private SettingsForm SettingsForm
    {
      get {
        return settingsForm ??= new SettingsForm();
      }
    }

    public MainForm()
    {
      InitializeComponent();
    }

    private void Settings_Click(object sender, EventArgs e)
    {
      SettingsForm.ShowDialog();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
      var processes = ADBProcess.GetProcesses();
      cbProcesses.Items.AddRange(processes.Cast<object>().ToArray());

      var tag = Settings.Instance.GetProperty(tagProperty, "") as string;
      cbTag.Text = tag;

      var currentProcess = Settings.Instance.GetProperty(currentProcessProperty, "") as string;
      if (!string.IsNullOrEmpty(currentProcess)) {
        var pl = processes.FindIndex(proc => proc.name == currentProcess);
        if (pl >= 0)
          cbProcesses.SelectedIndex = pl;
      }
    }

    private void cbProcesses_SelectedIndexChanged(object sender, EventArgs e)
    {
      var p = cbProcesses.SelectedItem as ADBProcess;
      if (p != null) {
        Settings.Instance.SetProperty(currentProcessProperty, p.name);
        TerminateSession();
        LogProcess(p);
      }
    }

    private void LogProcess(ADBProcess process)
    {
      logReader = ADB.Instance.LogProcess(process);
      if (logReader != null) {
        _ = Task.Run(() => ProcessLog(logReader));
      }
    }

    private async void ProcessLog(ADBLogReader reader)
    {
      lines = new List<ADBLogReader.LogLine>();
      tags = new HashSet<string>();
      string tagFilter = Invoke(() => cbTag.Text);
      string textFilter = Invoke(() => filterText.Text);

      ADBLogReader.LogLine? line = await reader.Next();
      while (line != null) {
        lines.Add(line);
        if (tags.Add(line.tag!)) {
          var tagArray = tags.ToArray();
          Array.Sort(tagArray);

          Invoke(() => {
            cbTag.Items.Clear();
            cbTag.Items.AddRange(tagArray);
          });
        }

        if (tagSelectionChanged.CurrentCount > 0) {
          tagSelectionChanged.Wait();

          tagFilter = Invoke(() => {
            if (cbTag.SelectedItem != null) {
              var str = cbTag.SelectedItem as string;
              if (!string.IsNullOrEmpty(str))
                return str;
            }

            return "";
          });
        }

        if (filterTextChanged.CurrentCount > 0) {
          filterTextChanged.Wait();
          textFilter = Invoke(() => filterText.Text);
        }

        if (tagFilter == "" || tagFilter == line.tag) {
          if (string.IsNullOrEmpty(textFilter) || line.message!.IndexOf(textFilter, StringComparison.OrdinalIgnoreCase) >= 0) {
            var item = new ListViewItem(new string[] {
            line.date.ToString("yyyy-MM-dd hh:mm:ss.fff"),
            line.pid.ToString(),
            line.type.ToString(),
            line.tag!,
            line.message!,
          });
            item.ForeColor = LogColors.Instance[line.type];
            item.Tag = line;

            Invoke(() => {
              listView.Items.Add(item).EnsureVisible();
            });
          }
        }

        if (paused) {
          pauseSem.Wait();
        }

        line = await reader.Next();
      }

      processTerminated.Release();
    }

    private void btPause_Click(object sender, EventArgs e)
    {
      if (!paused) {
        paused = true;
        btPause.Text = "Resume";
      } else {
        paused = false;
        pauseSem.Release();
        btPause.Text = "Pause";
      }
    }

    private async void cbTag_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (!paused)
        btPause_Click(sender, e);

      await Task.Delay(200);

      listView.Items.Clear();
      AddFilteredLines();

      tagSelectionChanged.Release();
      btPause_Click(sender, e);
    }

    private void AddFilteredLines()
    {
      string tag = cbTag.Text;
      string text = filterText.Text;

      var lines = this.lines.Where(line => (tag == "" || line.tag == tag) && (text == "" || line.message.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0));

      AddLines(lines);
    }

    private void AddLines(IEnumerable<ADBLogReader.LogLine> lines)
    {
      foreach (var line in lines) {
        var item = new ListViewItem(new string[] {
            line.date.ToString("yyyy-MM-dd hh:mm:ss.fff"),
            line.pid.ToString(),
            line.type.ToString(),
            line.tag!,
            line.message!,
          });
        item.ForeColor = LogColors.Instance[line.type];
        item.Tag = line;

        listView.Items.Add(item).EnsureVisible();
      }
    }

    private void listView_MouseClick(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right) {
        if (listView.FocusedItem != null) {
          selectedLine = (listView.FocusedItem.Tag as ADBLogReader.LogLine)!;
          listContextMenu.Show(Cursor.Position);
        }
      }
    }

    private void copyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (selectedLine != null) {
        Clipboard.SetText(selectedLine.message!);
      }
    }

    private void filter_TextOutOfFocus(object sender, EventArgs e)
    {
      if (!paused)
        btPause_Click(sender, e);

      listView.Items.Clear();
      AddFilteredLines();

      filterTextChanged.Release();
      btPause_Click(sender, e);
    }

    private void filterText_KeyUp(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == '\r') {
        filter_TextOutOfFocus(sender, e);
        e.Handled = true;
      }
    }

    private void btRefresh_Click(object sender, EventArgs e)
    {
      TerminateSession();

      lines.Clear();
      listView.Items.Clear();

      RefreshProcesses();
    }

    private void TerminateSession()
    {
      if (logReader != null) {
        logReader.Close();
        logReader = null;

        processTerminated.Wait();
      }
    }

    private void RefreshProcesses()
    {
      var processes = ADBProcess.GetProcesses();
      var current = cbProcesses.SelectedItem as ADBProcess;

      cbProcesses.Items.Clear();
      cbProcesses.Items.AddRange(processes.Cast<object>().ToArray());
      cbProcesses.SelectedIndex = -1;

      if (current != null) {
        var pl = processes.FindIndex(proc => proc.name == current.name);
        if (pl >= 0)
          cbProcesses.SelectedIndex = pl;
      } else {
        var pl = processes.FindIndex(proc => proc.name == Settings.Instance.GetProperty(currentProcessProperty) as string);
        if (pl >= 0)
          cbProcesses.SelectedIndex = pl;
      }
    }
  }
}