using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vkr;

    public VaultKeepsService(VaultKeepsRepository vkr)
    {
      _vkr = vkr;
    }

    public VaultKeep Create(VaultKeep vaultKeep)
    {
      int id = _vkr.Create(vaultKeep);
      vaultKeep.Id = id;
      return vaultKeep;
    }

    internal List<VaultKeepView> GetVaultKeepsById(int id)
    {
      var vkeeps = _vkr.GetVaultKeeps(id);
      return vkeeps;
    }
  }
}