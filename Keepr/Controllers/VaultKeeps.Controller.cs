using System.Collections.Generic;
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

  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;
    private readonly AccountService _acs;

    public VaultKeepsController(VaultKeepsService vks, AccountService acs)
    {
      _vks = vks;
      _acs = acs;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vaultKeep)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vaultKeep.CreatorId = userInfo.Id;
        var vk = _vks.Create(vaultKeep);
        return Ok(vk);

      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }


  }
}