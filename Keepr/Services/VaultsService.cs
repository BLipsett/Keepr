using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vr;

    public VaultsService(VaultsRepository vr)
    {
      _vr = vr;
    }

    public Vault GetVault(int id)
    {
      Vault vault = _vr.GetOne(id);
      if (vault != null)
      {
        return vault;
      }
      throw new Exception("Object Id does not exist");
    }

    public Vault Create(Vault newVault)
    {
      int id = _vr.Create(newVault);
      newVault.Id = id;
      return newVault;
    }
  }
}