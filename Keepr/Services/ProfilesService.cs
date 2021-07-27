using System;
using System.Collections.Generic;
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
  }
}