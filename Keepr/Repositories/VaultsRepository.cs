using System;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{

  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    public int Create(Vault newVault)
    {
      string sql = @"
      INSERT INTO vaults
      (name, description, creatorId, isPrivate)
      VALUES
      (@Name, @description, @CreatorId, @IsPrivate);
      SELECT LAST_INSERT_ID();
      ";
      return _db.ExecuteScalar<int>(sql, newVault);
    }

    public Vault GetOne(int id)
    {
      string sql = @"
      SELECT 
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON v.creatorId = a.id
      WHERE v.id = @id;
      ";
      return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
      {
        v.Creator = p;
        return v;
      }, new { id }).FirstOrDefault();
    }

    internal int Update(Vault vaultData)
    {
      string sql = @"
      UPDATE vaults
      SET
      name = @Name,
      description = @Description,
      isPrivate = @IsPrivate
      WHERE id = @Id;
      ";
      return _db.Execute(sql, vaultData);
    }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM vaults
      WHERE id =@id LIMIT 1;
      ";
      _db.Execute(sql, new { id });
    }
  }
}