using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _kr;

    public KeepsService(KeepsRepository kr)
    {
      _kr = kr;
    }

    public Keep CreateKeep(Keep newKeep)
    {
      int id = _kr.CreateKeep(newKeep);
      newKeep.Id = id;
      return newKeep;
    }

    public List<Keep> GetAll()
    {
      return _kr.GetAll();
    }

    public Keep UpdateKeep(Keep keepData)
    {
      Keep OriginalKeep = _kr.GetOne(keepData.Id);
      if (OriginalKeep == null)
      {
        throw new Exception("Invalid Id");
      }
      if (OriginalKeep.CreatorId != keepData.CreatorId)
      {



        throw new Exception("You must be the creator to update");
      }
      OriginalKeep.Name = keepData.Name ?? OriginalKeep.Name;
      OriginalKeep.Description = keepData.Description ?? OriginalKeep.Description;
      OriginalKeep.Img = keepData.Img ?? OriginalKeep.Img;
      return _kr.UpdateKeep(keepData);
    }

    public Keep GetOne(int id)
    {

      Keep keep = _kr.GetOne(id);
      if (keep != null)
      {
        return keep;
      }
      throw new Exception("Object Id does not exist");

    }

    public void Delete(int id, string userId)
    {
      Keep keep = _kr.GetOne(id);
      if (keep.CreatorId != userId)
      {
        throw new Exception("Invalid user");
      }
      _kr.Delete(id);
    }
  }
}