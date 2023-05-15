namespace AndroidLogcat
{
  using Newtonsoft.Json;
  using System.IO.Compression;
  using System.Text;
  using System.Windows.Forms;

  public class Settings
  {
    private static Settings? instance = null;
    private Dictionary<string, object?> properties = new();
    private string path => Application.UserAppDataPath + "/settings.dat";

    public static Settings Instance => instance ??= new Settings();

    private Settings()
    {
      try { 
        using var reader = new JsonTextReader(new StreamReader(new GZipStream(
          new FileStream(path, FileMode.Open, FileAccess.Read), CompressionMode.Decompress)));
        var serializer = JsonSerializer.CreateDefault();
        var data = serializer.Deserialize<Dictionary<string, object?>>(reader);
        if (data != null)
          properties = data;
      }
      catch (Exception) {
        //MessageBox.Show(exc.Message);
      }
    }

    public void SetProperty(string name, object? data)
    {
      properties[name] = data;
      Save();
    }

    public object? GetProperty(string name, object? defaultValue = null)
    {
      if (properties.TryGetValue(name, out var value))
        return value;
      else
        return defaultValue;
    }

    private void Save()
    {
      try {
        //C:\Program Files\Unity\Hub\Editor\2021.3.18f1\Editor\Data\PlaybackEngines\AndroidPlayer\SDK\platform-tools
        using var writer = new JsonTextWriter(new StreamWriter(
          new GZipStream(new FileStream(path, FileMode.Create, FileAccess.Write), CompressionMode.Compress), Encoding.UTF8));

        var serializer = JsonSerializer.CreateDefault();
        serializer.Serialize(writer, properties);
      } catch (Exception exc) {
        MessageBox.Show(exc.Message);
      }
    }
  }
}
