using System;
using System.Collections.Generic;
using System.Linq;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _pr;

    public ProfilesService(ProfilesRepository pr)
    {
      _pr = pr;
    }

    public Profile GetOne(string id)
    {
      Profile profile = _pr.GetOne(id);
      return profile;
    }

    public List<Keep> GetKeepsByProfile(string id)
    {
      return _pr.GetKeepsByProfile(id);
    }

    public List<Vault> GetVaultsByProfile(string id)
    {
      List<Vault> vaults = _pr.GetVaultsByProfile(id);
      List<Vault> creatorList = vaults.Where(v => v.CreatorId == id).ToList();
      return creatorList;

      // foreach (var v in vaults)
      // {
      //   if (v.IsPrivate && v.CreatorId != id)
      //   {
      //     throw new Exception("invalid user");
      //   }
      //   return vaults;
      // }
      // throw new Exception("bad");
    }

    public List<Vault> GetPubVaultsByProfile(string id)
    {
      List<Vault> vaults = _pr.GetVaultsByProfile(id);
      List<Vault> filteredList = vaults.Where(v => v.IsPrivate == false).ToList();
      return filteredList;
    }
  }
}