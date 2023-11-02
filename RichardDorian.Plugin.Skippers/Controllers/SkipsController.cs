using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RichardDorian.Plugin.Skippers.Data;

namespace RichardDorian.Plugin.Skippers.Controllers;

[Authorize]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public class SkipsController : ControllerBase
{
  public SkipsController()
  {
  }

  [HttpGet("Item/{id}/Skips")]
  public ActionResult<List<Skip>> GetSkips([FromRoute] Guid id)
  {
    var skips = GetSkipsFromGuid(id);

    if (skips is null)
    {
      return NotFound();
    }

    return skips;
  }

  private static List<Skip>? GetSkipsFromGuid(Guid id)
  {
    Plugin.Instance!.Skippers.TryGetValue(id, out var skips);
    return skips;
  }
}