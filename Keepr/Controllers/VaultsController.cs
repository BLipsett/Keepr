using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{

  [ApiController]
  [Route("api/[controller]")]

  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;
    private readonly AccountService _acs;

    public VaultsController(VaultsService vs, AccountService acs)
    {
      _vs = vs;
      _acs = acs;
    }

    [Authorize]
    [HttpGet("{id}")]
    public ActionResult<Vault> GetOne(int id)
    {
      try
      {
        return Ok(_vs.GetVault(id));
      }
      catch (System.Exception)
      {
        throw;
      }

    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault vault)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vault.CreatorId = userInfo.Id;
        vault.Creator = userInfo;
        return Ok(_vs.Create(vault));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}