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
      if (vault.IsPrivate == true)
      {
        throw new Exception("That vault is private!");
      }
      if (vault != null)
      {
        return vault;
      }
      throw new Exception("Object Id does not exist");
    }

    public Vault GetCreatorsVault(int id, string userId)
    {
      Vault vault = _vr.GetOne(id);
      if (vault.CreatorId == userId)
      {
        return vault;
      }
      throw new Exception("Access denied");
    }

    public Vault Create(Vault newVault)
    {
      int id = _vr.Create(newVault);
      newVault.Id = id;
      return newVault;
    }

    internal Vault UpdateVault(Vault vaultData, string id)
    {
      Vault OgVault = _vr.GetOne(vaultData.Id);
      if (OgVault == null)
      {
        throw new Exception("Invalid Id");
      }
      if (vaultData.CreatorId != OgVault.CreatorId)
      {
        throw new Exception("Only the creator can edit this vault");
      }
      OgVault.Name = vaultData.Name ?? OgVault.Name;
      OgVault.Description = vaultData.Description ?? OgVault.Description;
      if (_vr.Update(vaultData) > 0)
      {
        return vaultData;
      }
      throw new Exception("update failed");
      ;

    }

    public void Delete(int id, string userId)
    {
      Vault vault = _vr.GetOne(id);
      if (vault.CreatorId != userId)
      {
        throw new Exception("Invalid user");
      }
      _vr.Delete(id);
    }
  }
}