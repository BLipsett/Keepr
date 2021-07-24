using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal int Create(VaultKeep vaultKeep)
    {
      string sql = @"
      INSERT INTO vaultKeeps
      (vaultId, keepId, creatorId)
      VALUES 
      (@VaultId, @KeepId, @CreatorId);
     SELECT LAST_INSERT_ID();
      ";
      return _db.ExecuteScalar<int>(sql, vaultKeep);
    }

    internal List<VaultKeep> GetVaultKeeps(int id)
    {
      string sql = @"
      SELECT
        vk.*,
     v.*,
     k.*
      FROM vaultKeeps vk
      JOIN vaults v ON v.id = vk.vaultId
      JOIN keeps k ON k.id = vk.keepId
      WHERE v.id = @id;
      ";
      return _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vk, p) =>
      {
        vk.Creator = p;
        return vk;
      }, new { id }).ToList();
    }
  }
}