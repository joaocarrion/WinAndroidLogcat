namespace AndroidLogcat
{
  using System.Diagnostics;
  using System.Globalization;

  public class ADBLogReader
  {
    public class LogLine
    {
      public enum Type
      {
        Debug,
        Info,
        Warning,
        Error,
        Other,
      }

      public DateTime date;
      public int pid;
      public Type type;
      public string tag = "";
      public string message = "";
      public bool isValid = true;

      public LogLine(string line)
      {
        var date = line[..18];
        if (DateTime.TryParseExact(date, "MM-dd HH:mm:ss.fff", null, DateTimeStyles.None, out this.date)) {
          this.date = DateTime.Now;
        } else {
          this.isValid = false;
          return;
        }

        var split = line[18..].Split(" ", StringSplitOptions.RemoveEmptyEntries);
        pid = int.Parse(split[0]);
        type = split[2] switch {
          "D" => Type.Debug,
          "I" => Type.Info,
          "E" => Type.Error,
          "W" => Type.Warning,
          _ => Type.Other
        };

        tag = split[3].Replace(":", "");
        if (split.Length > 4) {
          if (split[4] == ":") {
            string[] array = new string[split.Length - 5];
            Array.Copy(split, 5, array, 0, split.Length - 5);
            message = string.Join(' ', array);
          } else {
            string[] array = new string[split.Length - 4];
            Array.Copy(split, 4, array, 0, split.Length - 4);
            message = string.Join(' ', array);
          }
        }
      }
    }

    private readonly StreamReader standardOutput;
    private readonly ADBProcess process;

    public ADBLogReader(StreamReader standardOutput, ADBProcess process)
    {
      this.standardOutput = standardOutput;
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

        //if (!standardOutput.BaseStream.CanRead)
        //  return null;
        line = await standardOutput.ReadLineAsync();
        //if (line != null && line == "") {
        //  Debug.WriteLine("Empty line");
        //}
      }

      return null;
    }
  }
}
