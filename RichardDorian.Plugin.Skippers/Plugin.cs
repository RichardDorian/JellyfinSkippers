using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Serialization;
using RichardDorian.Plugin.Skippers.Data;

namespace RichardDorian.Plugin.Skippers;

public class Plugin : BasePlugin<PluginConfiguration>
{
  private readonly string SkippersPath;

  public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer) : base(applicationPaths, xmlSerializer)
  {
    Instance = this;

    SkippersPath = Path.Join(applicationPaths.PluginConfigurationsPath, "RichardDorian.Plugin.Skippers.xml");
    RestoreSkippersFromDisk();
  }

  public override string Name => "Skippers";
  public override Guid Id => Guid.Parse("b9650cbc-83f5-4e1f-bb54-4dffc66c2033");
  public Dictionary<Guid, List<Skip>> Skippers { get; set; } = new();

  public static Plugin? Instance { get; private set; }

  public void RestoreSkippersFromDisk()
  {
    if (File.Exists(SkippersPath))
    {
      var skipsList = (List<Skipper>)XmlSerializer.DeserializeFromFile(
        typeof(List<Skipper>),
        SkippersPath
      );

      foreach (var skips in skipsList)
      {
        Skippers[skips.Id] = skips.Skips;
      }
    }
    else
    {
      XmlSerializer.SerializeToFile(
        new List<Skipper>(),
        SkippersPath
      );
    }
  }
}