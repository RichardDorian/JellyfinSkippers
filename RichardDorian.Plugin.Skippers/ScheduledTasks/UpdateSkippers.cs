using MediaBrowser.Model.Tasks;

namespace RichardDorian.Plugin.Skippers.ScheduledTasks;

public class UpdateSkippersTask : IScheduledTask
{
  public string Name => "Update Skippers";
  public string Category => "Skippers";
  public string Description => "Update skippers from disk";
  public string Key => "RichardDorianSkippersUpdate";

  public Task ExecuteAsync(
    IProgress<double> progress,
    CancellationToken cancellationToken
  )
  {
    Plugin.Instance!.RestoreSkippersFromDisk();
    return Task.CompletedTask;
  }

  public IEnumerable<TaskTriggerInfo> GetDefaultTriggers()
  {
    return Array.Empty<TaskTriggerInfo>();
  }
}