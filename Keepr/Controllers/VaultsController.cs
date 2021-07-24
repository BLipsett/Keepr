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

  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;
    private readonly AccountService _acs;
    private readonly VaultKeepsService _vks;

    public VaultsController(VaultsService vs, AccountService acs, VaultKeepsService vks)
    {
      _vs = vs;
      _acs = acs;
      _vks = vks;
    }

    [HttpGet("{id}")]
    public ActionResult<Vault> GetOne(int id)
    {
      try
      {
        return Ok(_vs.GetVault(id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public ActionResult<List<VaultKeep>> GetKeepsByVault(int id)
    {
      try
      {
        return Ok(_vks.GetVaultKeepsById(id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
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

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<Vault>> Update(int id, [FromBody] Vault vaultData)
    {

      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vaultData.CreatorId = userInfo.Id;
        vaultData.Id = id;
        var v = _vs.UpdateVault(vaultData, userInfo.Id);
        return Ok(v);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Vault>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _vs.Delete(id, userInfo.Id);
        return Ok("removed successfully");
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}