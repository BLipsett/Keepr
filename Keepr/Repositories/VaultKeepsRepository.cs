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

    internal List<VaultKeepViewModel> GetVaultKeeps(int id)
    {
      string sql = @"
      SELECT
        k.*,
        v.*,
     a.*,
     vk.id AS VaultKeepId
      FROM vaultKeeps vk
      JOIN keeps k ON k.id = vk.keepId
      JOIN vaults v ON v.id = vk.vaultId
      JOIN accounts a ON v.creatorId = a.id
      WHERE vk.vaultId = @id;
      ";
      return _db.Query<VaultKeepViewModel, Vault, Profile, VaultKeepViewModel>(sql, (k, v, p) =>
      {
        k.Vault = v;
        k.Creator = p;
        v.Creator = p;
        return k;
      }, new { id }, splitOn: "id").ToList();
    }
  }
}