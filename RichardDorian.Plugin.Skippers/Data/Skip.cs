namespace RichardDorian.Plugin.Skippers.Data;

public class Skip
{
  public SkipType Type { get; set; }
  public double Start { get; set; }
  public double End { get; set; }

  public Skip(SkipType type, double start, double end)
  {
    Type = type;
    Start = start;
    End = end;
  }

  internal Skip()
  {
  }

  public double ShowPromptAt
  {
    get => Start + .5f;
  }

  public double HidePromptAt
  {
    get => Math.Round(Start + (End - Start) / 3, 1);
  }
}