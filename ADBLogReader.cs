namespace AndroidLogcat
{
  using System.Diagnostics;

  public partial class ADBLogReader
  {
    private readonly Process systemProcess;
    private readonly StreamReader standardOutput;
    private readonly ADBProcess process;

    public ADBLogReader(Process systemProcess, ADBProcess process)
    {
      this.systemProcess = systemProcess;
      this.standardOutput = systemProcess.StandardOutput;
      this.process = process;
    }

    public async Task<LogLine?> Next()
    {
      string? line = await standardOutput.ReadLineAsync();
      while (line != null) {
        if (line.Length > 0 && !line.StartsWith("--")) {
          var logLine = new LogLine(line);
          if (logLine.isValid && logLine.pid == process.pid) {
            return logLine;
          }
        }

        line = await standardOutput.ReadLineAsync();
      }

      return null;
    }

    public void Close() => systemProcess.Kill();
  }
}
