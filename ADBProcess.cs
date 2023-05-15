namespace AndroidLogcat
{
  public class ADBProcess
  {
    public int pid { get; private set; }
    public string name { get; private set; }

    private ADBProcess(int id, string name)
    {
      this.pid = id;
      this.name = name;
    }

    public override string ToString() => $"proc: {pid} - {name}";

    public static List<ADBProcess> GetProcesses(string filter = "com.")
    {
      var processes = ADB.Instance.PS(filter);
      var list = new List<ADBProcess>();
      list.Sort((p1, p2) => p1.name.CompareTo(p2.name));

      foreach (var p in processes) {
        var s = p.Split(new char[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var pid = s[1];
        var name = s[8];

        list.Add(new ADBProcess(int.Parse(pid), name));
      }

      return list;
    }
  }
}
