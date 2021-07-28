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
    private readonly VaultsService _vs;

    public VaultKeepsService(VaultKeepsRepository vkr, VaultsRepository vr, AccountService acs, VaultsService vs)
    {
      _vkr = vkr;
      _vr = vr;
      _acs = acs;
      _vs = vs;
    }

    public VaultKeep Create(VaultKeep vaultKeep, string userId)
    {

      int id = _vkr.Create(vaultKeep);
      vaultKeep.Id = id;
      Vault vault = _vr.GetOne(vaultKeep.VaultId);
      if (vault.CreatorId != userId)
      {
        throw new Exception("only vault owner can create relationship");
      }
      return vaultKeep;
    }

    internal List<VaultKeepViewModel> GetVaultKeepsById(int id)
    {
      Vault vault = _vr.GetOne(id);
      if (vault.IsPrivate)
      {
        {
          throw new Exception("Only the owner can see this vault");
        }
      }
      var vkeeps = _vkr.GetVaultKeeps(id);
      return vkeeps;
    }



    internal void Delete(int id, string userId)
    {
      VaultKeep vKeep = _vkr.GetOneVk(id);
      if (vKeep.CreatorId != userId)
      {
        throw new Exception("Only creator can delete");
      }
      _vkr.Delete(id);
    }
  }
}