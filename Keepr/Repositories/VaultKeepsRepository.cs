using System;
using System.Data;
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
  }
}