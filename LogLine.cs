namespace AndroidLogcat
{
  using System.Globalization;

  public partial class ADBLogReader
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
  }
}
