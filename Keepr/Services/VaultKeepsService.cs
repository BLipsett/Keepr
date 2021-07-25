using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Repositories;
using Microsoft.AspNetCore.Http;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vkr;

    private readonly VaultsRepository _vr;
    private readonly AccountService _acs;

    public VaultKeepsService(VaultKeepsRepository vkr, VaultsRepository vr, AccountService acs)
    {
      _vkr = vkr;
      _vr = vr;
      _acs = acs;
    }

    public VaultKeep Create(VaultKeep vaultKeep)
    {
      int id = _vkr.Create(vaultKeep);
      vaultKeep.Id = id;
      return vaultKeep;
    }

    internal async Task<List<VaultKeepView>> GetVaultKeepsById(int id)
    {
      Vault vault = _vr.GetOne(id);
      if (vault.IsPrivate)
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        if (userInfo.Id != vault.CreatorId)
        {
          throw new Exception("Only the owner can see this vault");
        }
      }
      var vkeeps = _vkr.GetVaultKeeps(id);
      return vkeeps;
    }
  }
}