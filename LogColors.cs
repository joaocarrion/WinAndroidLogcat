namespace AndroidLogcat
{
  using System.Collections.Generic;

  public class LogColors
  {
    private static LogColors? instance;
    private Dictionary<ADBLogReader.LogLine.Type, Color> colors = new() {
        { ADBLogReader.LogLine.Type.Info, Color.Black },
        { ADBLogReader.LogLine.Type.Debug, Color.FromArgb(255, 20, 20, 20) },
        { ADBLogReader.LogLine.Type.Warning, Color.DarkGoldenrod},
        { ADBLogReader.LogLine.Type.Error, Color.Red },
        { ADBLogReader.LogLine.Type.Other, Color.DarkMagenta},
      };

    public static LogColors Instance => instance ??= new LogColors();

    private LogColors() { }

    public Color this[ADBLogReader.LogLine.Type type] => colors[type];
  }
}