using System.Collections.Generic;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;

    public ProfilesController(ProfilesService ps)
    {
      _ps = ps;
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> GetOne(string id)
    {
      try
      {
        return Ok(_ps.GetOne(id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> GetKeepsByProfile(string id)
    {
      try
      {
        return Ok(_ps.GetKeepsByProfile(id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}