namespace RichardDorian.Plugin.Skippers.Data;

public class Skipper
{
  public Guid Id;
  public List<Skip> Skips;

  public Skipper(Guid id, List<Skip> skips)
  {
    Id = id;
    Skips = skips;
  }

  internal Skipper()
  {
    Id = Guid.Empty;
    Skips = new();
  }
}