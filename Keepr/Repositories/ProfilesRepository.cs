using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Profile GetOne(string id)
    {
      string sql = @"
      SELECT 
      a.*
      FROM accounts a
      WHERE a.id = @id;
      ";
      return _db.QueryFirstOrDefault<Profile>(sql, new { id });
    }

    internal List<Keep> GetKeepsByProfile(string id)
    {
      string sql = @"
      SELECT k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id
      WHERE k.creatorId = @id;
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
      {
        k.Creator = p;
        return k;
      },
      new { id }, splitOn: "id").ToList();
    }
  }
}