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
     vk.id AS VaultKeepId,
     a.*
      FROM vaultKeeps vk
      JOIN keeps k ON k.id = vk.keepId
      JOIN accounts a ON vk.creatorId = a.id
      WHERE vk.vaultId = @id;
      ";
      return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (k, p) =>
      {
        k.Creator = p;
        return k;
      }, new { id }, splitOn: "id").ToList();
    }

    internal VaultKeep GetOneVk(int id)
    {
      string sql = @"
      SELECT * FROM vaultKeeps
      WHERE vaultKeeps.id = @id;
      ";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });

    }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM vaultKeeps
      WHERE id =@id LIMIT 1;
      ";
      _db.Execute(sql, new { id });
    }
  }
}
