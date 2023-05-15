namespace AndroidLogcat
{
  using System.Diagnostics;
  using System.Text;

  public class ADBException : Exception
  {
    public enum Errors
    {
      ADBProgramNotSet,
      InvalidLogLine,
    }

    private static Dictionary<Errors, string> errorValues = new Dictionary<Errors, string>() {
      { Errors.ADBProgramNotSet, "ADB program path not set" },
      { Errors.InvalidLogLine, "Invalid log line" },
    };

    public ADBException(Errors error) : base(errorValues[error]) { }
  }

  public class ADB
  {
    private static ADB? adbInstance;

    // TODO: could be multiplataformed...
    private const string adbExecutable = "adb.exe";
    private string? adbProgram = null;

    public static ADB Instance => adbInstance ??= new ADB();

    private ADB()
    {
      Refresh();
    }

    private Process ADB_Process(string arguments)
    {
      Process p = new Process();
      p.StartInfo.FileName = adbProgram;
      p.StartInfo.Arguments = arguments;
      p.StartInfo.CreateNoWindow = true;
      p.StartInfo.UseShellExecute = false;
      p.StartInfo.RedirectStandardOutput = true;
      p.StartInfo.RedirectStandardError = true;
      p.StartInfo.StandardOutputEncoding = Encoding.UTF8;

      return p;
    }

    public List<string> PS(string filter)
    {
      Refresh();

      List<string> processes = new List<string>();
      var p = ADB_Process("shell ps");
      if (!p.Start()) {
        MessageBox.Show("Failed to run ADB");
        return processes;
      }

      var output = p.StandardOutput;
      string? line = output.ReadLine();
      while (line != null) {
        Debug.WriteLine(line);
        if (line.StartsWith("--"))
          continue;
        
        if (line.Contains(filter))
          processes.Add(line);

        line = output.ReadLine();
      }

      return processes;
    }

    public void Refresh()
    {
      string? path = AndroidLogcat.Settings.Instance.GetProperty("adb_path") as string;
      if (path != null) {
        var adb = $"{path}/{adbExecutable}";
        if (File.Exists(adb)) {
          if (!string.IsNullOrEmpty(adbProgram) || adbProgram != adb) {
            adbProgram = adb;
          }
        }
      }

      if (string.IsNullOrEmpty(adbProgram))
        throw new ADBException(ADBException.Errors.ADBProgramNotSet);
    }

    public ADBLogReader? LogProcess(ADBProcess process)
    {
      var exec = ADB_Process("logcat");
      if (exec.Start()) {
        return new ADBLogReader(exec, process);
      } else {
        return null;
      }
    }
  }
}
